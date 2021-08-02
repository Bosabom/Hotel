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
    public class BookingController : ApiController
    {
        private IBookingService service;
        private IMapper mapper;
        private IMapper bookingmapper_reverse;

        public BookingController(IBookingService service)
        {
            this.service = service;
            mapper = new MapperConfiguration(cfg =>
               cfg.CreateMap<BookingDTO, BookingModel>()).CreateMapper();

            bookingmapper_reverse = new MapperConfiguration(cfg =>
            cfg.CreateMap<BookingModel, BookingDTO>()).CreateMapper();
        }
       
        public IEnumerable<BookingModel> Get()
        {
            var data = service.GetAllBookings();

            var bookings = mapper.Map<IEnumerable<BookingDTO>, List<BookingModel>>(data);
            return bookings;
        }

        [ResponseType(typeof(BookingModel))]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            try
            {
                BookingDTO data = service.Get(id);
                var booking = new BookingModel();

                if (data != null)
                {
                    booking = mapper.Map<BookingDTO, BookingModel>(data);
                    return request.CreateResponse(HttpStatusCode.OK, booking);

                }

                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (NullReferenceException ex)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
        }
        [Route("api/GetProfitForMonth/{date}")]
        public HttpResponseMessage GetProfitForMonth(HttpRequestMessage request, [FromUri] DateTime date)
        {

            if (date != null)
            {
                var profit = service.GetProfitForMonth(date);
                return request.CreateResponse(HttpStatusCode.OK, profit);
            }
            return request.CreateResponse(HttpStatusCode.NotFound);

        }


        [ResponseType(typeof(List<RoomModel>))]
        [Route("api/GetFreeRoomsOnDate/{date_from}/{date_to}")]
        public HttpResponseMessage GetFreeRoomsOnDate(HttpRequestMessage request, [FromUri] DateTime date_from, [FromUri] DateTime date_to)
        {
            var rooms = service.GetFreeRoomsOnPeriod(date_from, date_to);
            if (!rooms.Any())
            {
                return request.CreateResponse(HttpStatusCode.NotFound, mapper.Map<IEnumerable<RoomDTO>, List<RoomModel>>(rooms));
            }
            return request.CreateResponse(HttpStatusCode.OK, mapper.Map<IEnumerable<RoomDTO>, List<RoomModel>>(rooms));
        }


        public HttpResponseMessage Post(HttpRequestMessage request, [FromBody] BookingModel value)
        {
            
            try
            {
                service.Create(bookingmapper_reverse.Map<BookingModel, BookingDTO>(value));
                return request.CreateResponse(HttpStatusCode.Created);

            }
            catch (Exception ex)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }

        }
        public HttpResponseMessage Put(HttpRequestMessage request, int id, [FromBody] BookingModel value)
        {
            try
            {
                service.Update(id, bookingmapper_reverse.Map<BookingModel, BookingDTO>(value));
                return request.CreateResponse(HttpStatusCode.OK);
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
