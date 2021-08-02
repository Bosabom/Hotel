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
    public class GuestController : ApiController
    {
        private IGuestService service;
        private IMapper mapper;
        public GuestController(IGuestService service)
        {
            this.service = service;
            mapper = new MapperConfiguration(cfg =>
               cfg.CreateMap<GuestDTO, GuestModel>()).CreateMapper();
        }
        public IEnumerable<GuestModel> Get()
        {
            var data = service.GetAllGuests();

            var guests = mapper.Map<IEnumerable<GuestDTO>, List<GuestModel>>(data);
            return guests;
        }

        [ResponseType(typeof(GuestModel))]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            try
            {
                GuestDTO data = service.Get(id);
                var guest = new GuestModel();

                if (data != null)
                {
                    guest = mapper.Map<GuestDTO, GuestModel>(data);
                    return request.CreateResponse(HttpStatusCode.OK, guest);

                }

                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (NullReferenceException ex)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        public HttpResponseMessage Post(HttpRequestMessage request,[FromBody]GuestModel value)
        {
            var mapper = new MapperConfiguration(cfg =>
              cfg.CreateMap<GuestModel, GuestDTO>()).CreateMapper();
            try
            {
                service.Create(mapper.Map<GuestModel, GuestDTO>(value));
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
