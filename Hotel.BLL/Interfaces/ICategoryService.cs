using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.BLL.DTO;

namespace Hotel.BLL.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDTO> GetAllCategories();

        CategoryDTO Get(int id);

        void Create(CategoryDTO CategoryDTO);

        void Delete(int id);
    }
}