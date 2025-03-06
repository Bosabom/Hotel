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
    public class PriceCategoryController : ApiController
    {
        private IPriceCategoryService service;
        private IMapper mapper;

        public PriceCategoryController(IPriceCategoryService service)
        {
            this.service = service;
            mapper = new MapperConfiguration(cfg =>
               cfg.CreateMap<PriceCategoryDTO, PriceCategoryModel>()).CreateMapper();
        }

        public IEnumerable<PriceCategoryModel> Get()
        {
            var data = service.GetAllPriceCategories();
            var pricecategories = mapper.Map<IEnumerable<PriceCategoryDTO>, List<PriceCategoryModel>>(data);
            return pricecategories;
        }

        [ResponseType(typeof(PriceCategoryModel))]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            try
            {
                PriceCategoryDTO data = service.Get(id);
                var pricecategory = new PriceCategoryModel();
                if (data != null)
                {
                    pricecategory = mapper.Map<PriceCategoryDTO, PriceCategoryModel>(data);
                    return request.CreateResponse(HttpStatusCode.OK, pricecategory);
                }
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (NullReferenceException ex)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        public HttpResponseMessage Post(HttpRequestMessage request, [FromBody] PriceCategoryModel value)
        {
            var mapper = new MapperConfiguration(cfg =>
              cfg.CreateMap<PriceCategoryModel, PriceCategoryDTO>()).CreateMapper();
            try
            {
                service.Create(mapper.Map<PriceCategoryModel, PriceCategoryDTO>(value));
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