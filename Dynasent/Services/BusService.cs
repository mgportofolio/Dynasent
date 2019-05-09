using Dynasent.ViewModels;
using Entities.Entities.PostGre;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dynasent.Services
{
    public class BusService
    {
        private readonly PostGreDbContext _PostGreDbContext;
        private readonly DriverService _DriverMan;

        public BusService(PostGreDbContext postGreDbContext, DriverService driverService)
        {
            this._PostGreDbContext = postGreDbContext;
            this._DriverMan = driverService;
        }

        public List<DriverViewModel> Drivers { set; get; }
        public List<BusViewModel> Bus { get; private set; }

        public async Task<bool> InsertBus(BusViewModel model)
        {
            var addData = new Entities.Entities.PostGre.Bus()
            {
                BusName = model.BusName,
                BusNumber = model.BusNumber,
                BusType = model.BusType,
                DriversId = model.DriversId
            };
            this._PostGreDbContext.Bus.Add(addData);
            await this._PostGreDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<BusViewModel>> GetDataBus()
        {
            var getAllData =
                 await _PostGreDbContext.Bus
                .Select(Q => new BusViewModel
                {
                    BusId = Q.BusId,
                    BusName = Q.BusName,
                    BusType = Q.BusType,
                    BusNumber = Q.BusNumber,
                    DriversId = Q.DriversId,
                    StartTimeStamp = Q.StartTimeStamp,
                    IsEnd = Q.IsEnd,
                    EndTimeStamp = Q.EndTimeStamp
                }).ToListAsync();

            return getAllData;
        }

        public async Task<List<BusViewModel>> GetBus(int busId)
        {
            var getAllData = await GetDataBus();
            var bus = getAllData.Where(Q => Q.BusId == busId)
                .Select(Q => Q).ToList();

            return bus;
        }

        public async Task<bool> EndBus(int busId)
        {
            var bus = await this._PostGreDbContext.Bus
                .Where(Q => Q.BusId == busId).FirstOrDefaultAsync();
            if(bus.IsEnd == true)
            {
                return false;
            }
            bus.IsEnd = true;
            this._PostGreDbContext.Bus.Update(bus);
            await this._PostGreDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetDriverId(int busId)
        {
            Bus = await GetDataBus();
            var driverId = Bus.Where(Q => Q.BusId == busId)
                .Select(Q => Q.DriversId).FirstOrDefault();
            return driverId;
        }

        
        public async Task<DriverViewModel> GetDriverViewModels(int busId)
        {
            Drivers = await _DriverMan.GetDataDriver();
            var driverId = await GetDriverId(busId);
            var driver = Drivers.Where(Q => Q.DriversId == driverId)
                .Select(Q => Q).FirstOrDefault();
            

            return driver;
        }

        public async Task<BusViewModel> GetBusViewModels(int busId)
        {
            Bus = await GetDataBus();
            var BusInfo = Bus.Where(Q => Q.BusId == busId)
                .Select(Q => Q).FirstOrDefault();
            return BusInfo;
        }
    }
}
