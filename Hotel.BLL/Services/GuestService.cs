using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;
using AutoMapper;

namespace Hotel.BLL.Services
{
    public class GuestService : IGuestService
    {
        private IWorkUnit Database { get; set; }
        IMapper mapper;
        IMapper mapper_reverse;
        public GuestService(IWorkUnit database)
        {
            this.Database = database;
            mapper = new MapperConfiguration(cfg =>
                 cfg.CreateMap<Guest, GuestDTO>()).CreateMapper();

            mapper_reverse = new MapperConfiguration(cfg =>
           cfg.CreateMap<GuestDTO, Guest>()).CreateMapper();

        }
        public IEnumerable<GuestDTO> GetAllGuests()
        {
            return mapper.Map<IEnumerable<Guest>, List<GuestDTO>>(Database.Guests.GetAll());
        }

        public GuestDTO Get(int id)
        {
            return mapper.Map<Guest, GuestDTO>(Database.Guests.Get(id));
        }

        public void Create(GuestDTO newGuest)
        {
            //проверка,а существует ли уже такой постоялец
            var allGuests = GetAllGuests();

            var data = allGuests.Where(g => g.Passport == newGuest.Passport).FirstOrDefault();
            if (data != null)
            {
                throw new Exception();
            }

           Database.Guests.Create(mapper_reverse.Map<GuestDTO, Guest>(newGuest));
           Database.Save();
           
        }
        public void Delete(int id)
        {
            //есть ли постоялец с таким id?
            var guestWithThisId = Database.Guests.Get(id);
            if (guestWithThisId != null)
            {
                Database.Guests.Delete(id);
                Database.Save();
            }
            else
                throw new Exception();
        }

    }
}
