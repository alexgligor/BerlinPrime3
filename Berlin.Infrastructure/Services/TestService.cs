using Berlin.Domain.Entities;

namespace Berlin.Infrastructure.Services
{
    public interface ITestService
    {
        Task GenerateData();
    }
    public class TestService : ITestService
    {
        private IGenericService<Service> _Service { get; set; }
        private IGenericService<Division> _Division{ get; set; }
        private IGenericService<ServiceType> _ServiceType { get; set; }
        private IGenericService<Device> _Device { get; set; }
        private IGenericService<Site> _Site { get; set; }
        private IGenericService<User> _User { get; set; }
        public TestService(IGenericService<Site> _Site, IGenericService<Device> _Device, 
            IGenericService<ServiceType> _ServiceType, IGenericService<Service> _Service,
            IGenericService<Division> _Division, IGenericService<User> _User)
        {
            this._Service = _Service;
            this._Division = _Division;
            this._ServiceType = _ServiceType;
            this._Site = _Site;
            this._Device = _Device;
            this._User = _User;
        }
        public async Task GenerateData()
        {
            

           
           
            #region Site

            var site1 = new Site() { Title = "Berlin", Description = "First location"  };

            var site2 = new Site() { Title = "Viena", Description = "Second location"};

            await _Site.Add(site1);
            await _Site.Add(site2);

            #endregion

            #region Device
            var device1 = new Device() { Title = "PC1", Site = site1 };
            var device2 = new Device() { Title = "PC2", Site = site1 };
            var device3 = new Device() { Title = "Tablet1", Site = site2 };

            await _Device.Add(device1);
            await _Device.Add(device2);
            await _Device.Add(device3);
            #endregion

            #region User
            var user1 = new User() { Title = "Andrei Moisuc" };
            user1.Sites.Add(new SiteUser() { Site = site1 });
            user1.Sites.Add(new SiteUser() { Site = site2 });
            var user2 = new User() { Title = "Flavius Bohaciuc" };
            user2.Sites.Add(new SiteUser() { Site = site1 });

            await _User.Add(user1);
            await _User.Add(user2);

            #endregion

            #region Division

            var division1 = new Division() { Title = "Car Shop" , Sites = { new SiteDivision() {Site = site1 } } };
            var division2 = new Division() { Title = "Motor Shop", Sites = { new SiteDivision() { Site = site1 } } };

            await _Division.Add(division1);
            await _Division.Add(division2);

            #endregion

            #region ServiceType

            await _ServiceType.Add( new ServiceType()
            {
                Title = "Lights",
                Description = "kkk",
                DevisionId = division1.Id,
                Services = new List<Service>() {
             new Service(){Title="Oil Change",Price = 14 , Description = "Details for filling"  },
             new Service(){Title="Filter Change", Price = 14 , Description = "Details for filling" },
             new Service(){Title="O Rings Change", Price = 134 , Description = "Details for filling"  },
             new Service(){Title="Ignition Change", Price = 149 , Description = "Details for filling"}}
            } );
            await _ServiceType.Add(new ServiceType()
            {
                Title = "Additionals",
                DevisionId = division1.Id,
                Services = new Service[] {
                 new Service() { Title = "Oil Change", Price = 14, Description = "Details for filling" },
                new Service() { Title = "Filter Change", Price = 14, Description = "Details for filling" },
                new Service() { Title = "O Rings Change", Price = 134, Description = "Details for filling" },
                new Service() { Title = "Ignition Change", Price = 149, Description = "Details for filling" },
                new Service() { Title = "Power Change", Price = 14, Description = "Details for filling" }
            }
            });
            await _ServiceType.Add(new ServiceType() { Title = "Specials", DevisionId = division1.Id, Services= new List<Service>()});
            await _ServiceType.Add(new ServiceType() { Title = "Weels",  DevisionId = division1.Id, Services = new List<Service>() }); 

                 await _ServiceType.Add(new ServiceType() { Title = "Light",  DevisionId = division2.Id, Services = new List<Service>() });
            await _ServiceType.Add(new ServiceType() { Title = "Blue",  DevisionId = division2.Id, Services = new List<Service>() });
            await _ServiceType.Add(new ServiceType() { Title = "Green",  DevisionId = division2.Id, Services = new List<Service>() });
            await _ServiceType.Add(new ServiceType() { Title = "Fast",  DevisionId = division2.Id, Services = new List<Service>() });

            #endregion
        }
    }
}
