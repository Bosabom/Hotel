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
    class PriceCategoryRepository : IRepository<PriceCategory>
    {
        private HotelModel db;
        public PriceCategoryRepository(HotelModel db)
        {
            this.db = db;
        }

        public IEnumerable<PriceCategory> GetAll()
        {
            return db.PriceCategories;
        }

        public PriceCategory Get(int id)
        {
            return db.PriceCategories.Find(id);
        }

        public void Create(PriceCategory pricecategory)
        {
            db.PriceCategories.Add(pricecategory);
        }

        public void Update(int id,PriceCategory priceCategory) { }
        public void Delete(int id)
        {
            PriceCategory pricecategory = Get(id);
            if (pricecategory != null) 
            {
                db.PriceCategories.Remove(pricecategory);     
            }
           
        }
    }
}
