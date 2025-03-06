using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hotel.API.Models
{
    public class RoomModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Active { get; set; }
        
        public int CategoryId { get; set; }

        public override bool Equals(object obj)
        {
            if(obj is RoomModel)
            {
                var objRM = obj as RoomModel;
                return this.Id == objRM.Id
                        && this.Name == objRM.Name
                        && this.Active == objRM.Active
                        &&this.CategoryId==objRM.CategoryId;
            }
            else
            {
                return base.Equals(obj);
            }
        }
    }
}