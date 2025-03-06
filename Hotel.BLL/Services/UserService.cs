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
using Hotel.BLL.Infrastructure;

namespace Hotel.BLL.Services
{
    public class UserService : IUserService
    {
        private IWorkUnit Database { get; set; }
        IMapper mapper;
        IMapper mapper_reverse;

        public UserService(IWorkUnit database)
        {
            this.Database = database;
         
            mapper = new MapperConfiguration(cfg =>
               cfg.CreateMap<UserDTO, User>()).CreateMapper();
            mapper_reverse = new MapperConfiguration(cfg =>
               cfg.CreateMap<User, UserDTO>()).CreateMapper();
        }

        public UserDTO Get(UserDTO userDTO)
        {
            var user = Database.Users.Get(mapper.Map<UserDTO, User>(userDTO));
            if(user != null && Crypto.Hash(userDTO.Password) == user.HashedPassword)
            {
                return mapper_reverse.Map<User, UserDTO>(user);
            }
            return null;
        }
       
        public void Create(UserDTO new_userDTO)
        {
            var user = Database.Users.Get(mapper.Map<UserDTO, User>(new_userDTO));
            if (user != null)
            {
                throw new Exception();
            }

            Database.Users.Create(mapper.Map<UserDTO, User>(new_userDTO));
            Database.Save();
        }
    }
}