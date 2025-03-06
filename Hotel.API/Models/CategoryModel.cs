using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hotel.API.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Number_Of_Places { get; set; }

        public override bool Equals(object obj)
        {
            if(obj is CategoryModel)
            {
                var objCM = obj as CategoryModel;
                return this.Id == objCM.Id
                    && this.Name == objCM.Name
                    && this.Number_Of_Places == objCM.Number_Of_Places;
            }
            else
            {
                return base.Equals(obj);
            }
        }
    }
}