using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hotel.API.Models
{
    public class GuestModel
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Passport { get; set; }
        public DateTime Birthday { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is GuestModel)
            {
                var objGM = obj as GuestModel;
                return this.Id == objGM.Id
                    && this.Name == objGM.Name
                    && this.Surname == objGM.Surname
                    && this.Passport == objGM.Passport;
            }
            else
                return base.Equals(obj);
        }
    }
}