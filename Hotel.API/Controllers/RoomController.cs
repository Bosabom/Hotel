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
    public class RoomController : ApiController
    {
        private IRoomService service;
        private IMapper mapper;

        public RoomController(IRoomService service)
        {
            this.service = service;
            mapper = new MapperConfiguration(cfg =>
               cfg.CreateMap<RoomDTO, RoomModel>()).CreateMapper();
        }
        public IEnumerable<RoomModel> Get()
        {
            var data = service.GetAllRooms();

            var rooms = mapper.Map<IEnumerable<RoomDTO>, List<RoomModel>>(data);
            return rooms;
        }

        [ResponseType(typeof(RoomModel))]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            try
            {
                RoomDTO data = service.Get(id);
                var room= new RoomModel();

                if (data != null)
                {
                    room = mapper.Map<RoomDTO, RoomModel>(data);
                    return request.CreateResponse(HttpStatusCode.OK, room);

                }
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (NullReferenceException ex)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [ResponseType(typeof(RoomModel))]

       
        public HttpResponseMessage Post(HttpRequestMessage request, [FromBody] RoomModel value)
        {
            var mapper = new MapperConfiguration(cfg =>
              cfg.CreateMap<RoomModel, RoomDTO>()).CreateMapper();
            try
            {
                service.Create(mapper.Map<RoomModel, RoomDTO>(value));
                return request.CreateResponse(HttpStatusCode.Created);

            }
            catch (Exception ex)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }

        }
        public HttpResponseMessage Put(HttpRequestMessage request, int id, [FromBody] RoomModel value)
        {
            try
            {
                var mapper = new MapperConfiguration(cfg =>
             cfg.CreateMap<RoomModel, RoomDTO>()).CreateMapper();

                service.Update(id, mapper.Map<RoomModel,RoomDTO>(value));
                return request.CreateResponse(HttpStatusCode.OK);
            }
            catch(Exception ex)
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
