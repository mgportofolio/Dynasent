using Dynasent.ViewModels;
using Entities.Entities.PostGre;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ZXing.QrCode;

namespace Dynasent.Services
{
    public class PassangerService
    {
        public string Src { set; get; }
      
        public List<PassangerViewModel> Passangers { set; get; } = new List<PassangerViewModel>();
        public string Base64 { set; get; }

        private readonly PostGreDbContext _PostGreDbContext;

        public PassangerService(PostGreDbContext postGreDbContext)
        {
            this._PostGreDbContext = postGreDbContext;
        }

        //public async Task<List<QrViewModel>> GetQrAsync()
        //{
        //    if (Qrs == null)
        //    {
        //        return Qrs = new List<QrViewModel>();
        //    }

        //    return Qrs;
        //}

        //public async Task CreateQr(QrViewModel qrViewModel)
        //{
        //    var qrs = await GetQrAsync();
        //    if (qrs == null)
        //    {
        //        var listTemp = new List<QrViewModel>();
        //    }
        //    qrs.Add(qrViewModel);
        //}

        public async Task<string> GenerateQrCode(string content)
        {
            var width = 250; // width of the Qr Code 
            var height = 250; // height of the Qr Code 
            var margin = 0;

            var qrCodeWriter = new ZXing.BarcodeWriterPixelData
            {
                Format = ZXing.BarcodeFormat.QR_CODE,

                Options = new QrCodeEncodingOptions
                {
                    Height = height,
                    Width = width,
                    Margin = margin,
                    PureBarcode = true,
                    DisableECI = false,
                    CharacterSet = "UTF-8",
                    GS1Format = false

                }
            };

            var tempString = content.Replace("&quot;", '"'.ToString());
            var pixelData = qrCodeWriter.Write(tempString);

            // creating a bitmap from the raw pixel data; if only black and white colors are used it makes no difference 
            // that the pixel data ist BGRA oriented and the bitmap is initialized with RGB 
            using (var bitmap = new Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
            using (var ms = new MemoryStream())
            {

                var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height),
                   System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                try
                {
                    // we assume that the row stride of the bitmap is aligned to 4 byte multiplied by the width of the image 
                    System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0,
                       pixelData.Pixels.Length);
                }
                finally
                {
                    bitmap.UnlockBits(bitmapData);
                }
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                return String.Format("data:image/png;base64,{0}", Convert.ToBase64String(ms.ToArray()));
            }
        }

        public async Task<List<PassangerViewModel>> GetPassangerAsync()
        {
            if (Passangers == null)
            {
                return Passangers = await GetDataPassanger();
            }

            return Passangers;
        }

        //public async Task CreatePassanger(PassangerViewModel passangerViewModel)
        //{
        //    var qrs = await GetPassangerAsync();
        //    if (qrs == null)
        //    {
        //        var listTemp = new List<QrViewModel>();
        //    }
        //    qrs.Add(passangerViewModel);
        //}

        public async Task<bool> InsertPassanger(PassangerViewModel model)
        {
            var addData = new Entities.Entities.PostGre.Passanger()
            {
                PassangerName = model.PassangerName,
                PassangerContact = model.PassangerContact,
                BusId = model.BusId,
                QrCodeSrc = model.QrCodeSrc
            };
            this._PostGreDbContext.Passanger.Add(addData);
            await this._PostGreDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<PassangerViewModel>> GetDataPassanger()
        {
            var getAllData =
                 await _PostGreDbContext.Passanger
                .Select(Q => new PassangerViewModel
                {
                    PassangerId = Q.PassangerId,
                    PassangerName = Q.PassangerName,
                    PassangerContact = Q.PassangerContact,
                    BusId = Q.BusId,
                    InOrOut = Q.InOrOut,
                    TimeStamp = Q.TimeStamp,
                    QrCodeSrc = Q.QrCodeSrc
                }).ToListAsync();

            return getAllData;
        }

        public async Task<int> GetPassangerId(string passangerName)
        {
            var getAllData = await GetDataPassanger();
            var passangerId = getAllData.Where(Q => Q.PassangerName == passangerName)
                .Select(Q => Q.PassangerId).FirstOrDefault();

            return passangerId;
        }

        public async Task<List<PassangerViewModel>> GetPassanger(int passangerId)
        {
            var getAllData = await GetDataPassanger();
            var passanger = getAllData.Where(Q => Q.PassangerId == passangerId)
                .Select(Q => Q).ToList();

            return passanger;
        }

        public async Task<PassangerViewModel> GetPassangerById(int passangerId)
        {
            var getAllData = await GetDataPassanger();
            var passanger = getAllData.Where(Q => Q.PassangerId == passangerId)
                .Select(Q => Q).FirstOrDefault();

            return passanger;
        }

        public async Task<bool> InOrOutPassanger(int passangerId, int busId, string passangerName)
        {
            var passanger = await this._PostGreDbContext.Passanger
                .Where(Q => Q.PassangerId == passangerId)
                .Select(Q=>Q)
                .FirstOrDefaultAsync();
            if (passanger == null)
            {
                return false;
            }
            if(passanger.PassangerName != passangerName)
            {
                return false;
            }
            if (passanger.BusId != busId)
            {
                return false;
            }
            

            switch(passanger.InOrOut)
            {
                case true:
                    passanger.InOrOut = false;
                    break;

                case false:
                    passanger.InOrOut = true;
                    break;
            }

            passanger.TimeStamp = DateTime.UtcNow;
            this._PostGreDbContext.Passanger.Update(passanger);
            await this._PostGreDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> InsertQrSrc(int passangerId, string qrCodeSrc)
        {
            var passanger = await this._PostGreDbContext.Passanger
                .Where(Q => Q.PassangerId == passangerId).FirstOrDefaultAsync();
            if (passanger == null)
            {
                return false;
            }
            if (passanger.QrCodeSrc.ToUpper() != "DEFAULT".ToUpper())
            {
                return false;
            }

            passanger.QrCodeSrc = qrCodeSrc;
            this._PostGreDbContext.Passanger.Update(passanger);
            await this._PostGreDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePassanger(int passangerId)
        {
            var passanger = await this._PostGreDbContext.Passanger
                .Where(Q => Q.PassangerId == passangerId).FirstOrDefaultAsync();
            this._PostGreDbContext.Passanger.Remove(passanger);
            await this._PostGreDbContext.SaveChangesAsync();
            return true;
        }

    }
}
