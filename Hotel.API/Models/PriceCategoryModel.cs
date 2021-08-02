using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
namespace Hotel.API.Models
{
    public class PriceCategoryModel
    {
        public int Id { get; set; }

        public double Price { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        //public CategoryModel Category { get; set; }

        public int CategoryId {get;set;}
        public override bool Equals(object obj)
        {
            if (obj is PriceCategoryModel)
            {
                var objPCM = obj as PriceCategoryModel;
                return this.Id == objPCM.Id
                    && this.Price == objPCM.Price
                    && this.StartDate == objPCM.StartDate
                    && this.EndDate == objPCM.EndDate
                    &&this.CategoryId==objPCM.CategoryId;
            }
            else
                return base.Equals(obj);
        }
    }
}