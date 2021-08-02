using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Hotel.API.Models;
using AutoMapper;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.BLL.Services;

namespace Hotel.API.Controllers
{
    public class CategoryController : ApiController
    {
        private ICategoryService service;
        private IMapper mapper;
        
        public CategoryController(ICategoryService service)
        {
            this.service = service;
            mapper = new MapperConfiguration(cfg =>
               cfg.CreateMap<CategoryDTO, CategoryModel>()).CreateMapper();
        }
        public IEnumerable<CategoryModel> Get()
        {
            var data = service.GetAllCategories();

            var categories = mapper.Map<IEnumerable<CategoryDTO>, List<CategoryModel>>(data);
            return categories;
        }

        [ResponseType(typeof(CategoryModel))]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            try
            {
                CategoryDTO data = service.Get(id);
                var category = new CategoryModel();

                if (data != null)
                {
                    category = mapper.Map<CategoryDTO, CategoryModel>(data);
                    return request.CreateResponse(HttpStatusCode.OK, category);

                }

                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (NullReferenceException ex)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        public HttpResponseMessage Post(HttpRequestMessage request, [FromBody] CategoryModel value)
        {
            var mapper = new MapperConfiguration(cfg =>
              cfg.CreateMap<CategoryModel, CategoryDTO>()).CreateMapper();
            try
            {
                service.Create(mapper.Map<CategoryModel, CategoryDTO>(value));
                return request.CreateResponse(HttpStatusCode.Created);

            }
            catch (Exception ex)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }

        }


        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            try
            {
                service.Delete(id);
                return request.CreateResponse(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
        }
    }
}
