using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AutoMapper;
using Hotel.API.Models;
using Hotel.API.Controllers;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;
using Hotel.BLL.Services;
using Moq;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using Hotel.DAL.Repositories;
using System.Web.Http;
using Newtonsoft.Json;
namespace Hotel.Tests
{
    [TestClass]
    public class BookingControllerTest
    {
        HttpConfiguration httpConfiguration;
        HttpRequestMessage httpRequest;
        private IMapper mapper;
        public BookingControllerTest()
        {
            httpConfiguration = new HttpConfiguration();
            httpRequest = new System.Net.Http.HttpRequestMessage();
            httpRequest.Properties[System.Web.Http.Hosting.HttpPropertyKeys.HttpConfigurationKey] = httpConfiguration;
           
            mapper = new MapperConfiguration(cfg =>
           cfg.CreateMap<BookingDTO, BookingModel>()).CreateMapper();

        }

        [TestMethod]
        public void GetAllBookingsTest()
        {
            var mock = new Mock<IBookingService>();
           
            mock.Setup(a => a.GetAllBookings()).Returns(new List<BookingDTO>());

            var expected = mapper.Map<IEnumerable<BookingDTO>, List<BookingModel>>(mock.Object.GetAllBookings());

            BookingController controller = new BookingController(mock.Object);
            var result = controller.Get();

            CollectionAssert.AreEqual(expected, result.ToList());
        }

        [TestMethod]

        public void GetBookingByIdTest()
        {
            int BookingId = 1;

            var BookingMock = new Mock<IBookingService>();

            BookingMock.Setup(a => a.Get(BookingId)).Returns(new BookingDTO());

            BookingController controller = new BookingController(BookingMock.Object);

            var httpResponse = controller.Get(httpRequest, BookingId);
            var result = httpResponse.Content.ReadAsAsync<BookingModel>();
            var expected = mapper.Map<BookingDTO, BookingModel>(BookingMock.Object.Get(BookingId));

            Assert.AreEqual(expected, result.Result);
        }

        [TestMethod]

        public void GetBookingById_CheckStatusCode_Test()
        {
            int BookingId = 3;

            var BookingMock = new Mock<IBookingService>();

            BookingMock.Setup(a => a.Get(BookingId)).Returns(new BookingDTO());

            BookingController controller = new BookingController(BookingMock.Object);

            var httpResponse = controller.Get(httpRequest, BookingId);
            var res = httpResponse.StatusCode;
            
            Assert.AreEqual(res,System.Net.HttpStatusCode.OK);
        }

        [TestMethod]
        public void GetFreeRoomsOnDateTest()
        {

            DateTime date_from = new DateTime(2021, 05, 11);
            DateTime date_to = new DateTime(2021, 08, 30);
            var mock = new Mock<IBookingService>();


            mock.Setup(a => a.GetFreeRoomsOnPeriod(date_from, date_to)).Returns(new List<RoomDTO>());

            BookingController controller = new BookingController(mock.Object);

            var httpResponse = controller.GetFreeRoomsOnDate(httpRequest, date_from, date_to);
            var result = httpResponse.Content.ReadAsAsync<List<RoomModel>>();
            var expected = mapper.Map<IEnumerable<RoomDTO>, List<RoomModel>>(mock.Object.GetFreeRoomsOnPeriod(date_from, date_to));

            Assert.AreEqual(expected.Count, result.Result.Count);
        }

        [TestMethod]

        public void GetProfitForMonthTest()
        {
            DateTime date = new DateTime(2021,08,01);
            var BookingMock = new Mock<IBookingService>();

            BookingMock.Setup(a => a.GetProfitForMonth(date));

            BookingController controller = new BookingController(BookingMock.Object);

            var httpResponse = controller.GetProfitForMonth(httpRequest, date);
            var result = httpResponse.Content.ReadAsAsync<double>();
            var expected = BookingMock.Object.GetProfitForMonth(date);
            Assert.AreEqual(expected, result.Result);
            
        }

        [TestMethod]

        public void GetProfitForMonth_CheckingStatusCode_Test()
        {
            DateTime date = new DateTime(2021, 08, 01);
            var BookingMock = new Mock<IBookingService>();

            BookingMock.Setup(a => a.GetProfitForMonth(date));

            BookingController controller = new BookingController(BookingMock.Object);

            var httpResponse = controller.GetProfitForMonth(httpRequest, date);
            var res = httpResponse.StatusCode;
            
            Assert.AreEqual(res, System.Net.HttpStatusCode.OK);
        }

        [TestMethod]
        public void CreateBookingTest()
        {

            BookingDTO new_booking = new BookingDTO()
            {
                IsGuestSettledIn = false,
                GuestId = 4,
                RoomId = 8,
                EnterDate = new DateTime(2021, 12, 15),
                LeaveDate=new DateTime(2021,12,25)


            };
            var BookingMock = new Mock<IBookingService>();

            BookingMock.Setup(a => a.Create(new_booking));

            BookingController controller = new BookingController(BookingMock.Object);

            var httpResponse = controller.Post(httpRequest, mapper.Map<BookingDTO, BookingModel>(new_booking));
            var res = httpResponse.StatusCode;

            Assert.IsNotNull(httpResponse);
            Assert.AreEqual(res, System.Net.HttpStatusCode.Created);
        }
        [TestMethod]
        public void DeleteBookingByIDTest()
        {
            int BookingId = 2;

            var BookingMock = new Mock<IBookingService>();

            BookingMock.Setup(a => a.Delete(BookingId));

            BookingController controller = new BookingController(BookingMock.Object);

            var httpResponse = controller.Delete(httpRequest, BookingId);
            var res = httpResponse.StatusCode;

            Assert.IsNotNull(httpResponse);
            Assert.AreEqual(res, System.Net.HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void UpdateBookingTest()
        {
            int id = 5;
            BookingDTO booking_with_updates = new BookingDTO()
            {
                IsGuestSettledIn = false
            };

            var BookingMock = new Mock<IBookingService>();

            BookingMock.Setup(a => a.Update(id, booking_with_updates));

            BookingController controller = new BookingController(BookingMock.Object);

            var httpResponse = controller.Put(httpRequest, id,mapper.Map<BookingDTO,BookingModel>(booking_with_updates));
            var res = httpResponse.StatusCode;

            Assert.IsNotNull(httpResponse);
            Assert.AreEqual(res, System.Net.HttpStatusCode.OK);

        }

        
    }
}
