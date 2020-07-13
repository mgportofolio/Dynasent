using Dynasent.ViewModels;
using Entities.Entities.PostGre;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dynasent.Services
{
    public class DriverService
    {
        private readonly PostGreDbContext _PostGreDbContext;

        public DriverService(PostGreDbContext postGreDbContext)
        {
            this._PostGreDbContext = postGreDbContext;
        }

        public async Task<List<DriverViewModel>> GetDataDriver()
        {
            var getAllData =
                 await _PostGreDbContext.Drivers
                .Select(Q => new DriverViewModel
                {
                    DriversId = Q.DriversId,
                    DriverName = Q.DriversName,
                    DriverPhoneNumber = Q.DriversPhoneNumber
                }).ToListAsync();

            return getAllData;
        }

        public async Task<bool> InsertDriver(DriverViewModel model)
        {
            var addData = new Entities.Entities.PostGre.Drivers()
            {
                DriversName = model.DriverName,
                DriversPhoneNumber = model.DriverPhoneNumber

            };
            this._PostGreDbContext.Drivers.Add(addData);
            await this._PostGreDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetDriverId(string driverName, string driverPhone)
        {
            var getAllData = await GetDataDriver();
            var driverId = getAllData.Where(Q => (Q.DriverName == driverName)
            && (Q.DriverPhoneNumber == driverPhone))
                .Select(Q => Q.DriversId).FirstOrDefault();

            return driverId;
        }

        public async Task<bool> UpdateDriverAsync(int driverId, DriverViewModel modelUpdated)
        {
            var driver = await this._PostGreDbContext.Drivers
                .Where(Q => Q.DriversId == driverId)
                .Select(Q => Q).FirstOrDefaultAsync();
            if (driver == null)
            {
                return false;
            }
            driver.DriversName = modelUpdated.DriverName;
            driver.DriversPhoneNumber = modelUpdated.DriverPhoneNumber;
            this._PostGreDbContext.Drivers.Update(driver);
            await this._PostGreDbContext.SaveChangesAsync();
            return true;
        }
    }
}
