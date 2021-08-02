using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.BLL.DTO;

namespace Hotel.BLL.Interfaces
{
    public interface IPriceCategoryService
    {
        IEnumerable<PriceCategoryDTO> GetAllPriceCategories();
        PriceCategoryDTO Get(int id);

        void Create(PriceCategoryDTO PriceCategoryDTO);

        void Delete(int id);
    }
}
