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
    public class LogService : ILogService
    {
        private IWorkUnit Database { get; set; }
        IMapper mapper;
        IMapper mapper_reverse;
        public LogService(IWorkUnit database)
        {
            this.Database = database;

            mapper = new MapperConfiguration(cfg =>
               cfg.CreateMap<LogDTO, Log>()).CreateMapper();
            mapper_reverse = new MapperConfiguration(cfg =>
               cfg.CreateMap<Log, LogDTO>()).CreateMapper();
        }
        public IEnumerable<LogDTO> GetAll()
        {
           return mapper_reverse.Map<IEnumerable<Log>, List<LogDTO>>(Database.Logs.GetAll());
        }

        public void Create(LogDTO new_logDTO)
        {
            Database.Logs.Create(mapper.Map<LogDTO, Log>(new_logDTO));
            Database.Save();
        }
    }
}
