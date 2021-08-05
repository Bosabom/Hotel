using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.DAL.EF;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;
namespace Hotel.DAL.Repositories
{
    class LogRepository : ILogRepository<Log>
    {
        private HotelModel db;
        public LogRepository(HotelModel db)
        {
            this.db = db;
        }
        public void Create(Log new_log)
        {
            db.Logs.Add(new_log);
        }
        public IEnumerable<Log> GetAll()
        {
            return db.Logs;
        }
    }
}
