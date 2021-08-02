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

namespace Hotel.Tests
{
    [TestClass]
    public class GuestControllerTest
    {
        HttpConfiguration httpConfiguration;
        HttpRequestMessage httpRequest;
        private IMapper mapper;
        public GuestControllerTest()
        {
           httpConfiguration = new HttpConfiguration();
           httpRequest = new System.Net.Http.HttpRequestMessage();
           httpRequest.Properties[System.Web.Http.Hosting.HttpPropertyKeys.HttpConfigurationKey] = httpConfiguration;
            
            mapper = new MapperConfiguration(cfg =>
            cfg.CreateMap<GuestDTO, GuestModel>()).CreateMapper();
        }
        
        [TestMethod]
        public void GetAllGuestsTest ()
        {
            var mock = new Mock<IGuestService>();
            mock.Setup(a => a.GetAllGuests()).Returns(new List<GuestDTO>());

            var expected = mapper.Map<IEnumerable<GuestDTO>, List<GuestModel>>(mock.Object.GetAllGuests());

            GuestController controller = new GuestController(mock.Object);
            var result = controller.Get();

            CollectionAssert.AreEqual(expected, result.ToList());
        }

        [TestMethod]

        public void GetGuestByIdTest()
        {
            int guestId = 5;
           
            var GuestMock = new Mock<IGuestService>();
            
            GuestMock.Setup(a => a.Get(guestId)).Returns(new GuestDTO());
            
            GuestController controller = new GuestController(GuestMock.Object);

            var httpResponse = controller.Get(httpRequest, guestId);
            var result = httpResponse.Content.ReadAsAsync<GuestModel>();
            var expected = mapper.Map<GuestDTO, GuestModel>(GuestMock.Object.Get(guestId));

            Assert.AreEqual(expected, result.Result);
        }

        [TestMethod]

        public void GetGuestById_CheckStatusCode_Test()
        {
            int guestId = 2;

            var GuestMock = new Mock<IGuestService>();

            GuestMock.Setup(a => a.Get(guestId)).Returns(new GuestDTO());

            GuestController controller = new GuestController(GuestMock.Object);

            var httpResponse = controller.Get(httpRequest, guestId);
            var res = httpResponse.StatusCode;

            Assert.AreEqual(res, System.Net.HttpStatusCode.OK);
        }
        [TestMethod]
        public void CreateGuestTest()
        {

            GuestDTO new_guest = new GuestDTO()
            {
                Id = 9,
                Name = "Marina",
                Surname="Koptelova"

            };
            var GuestMock = new Mock<IGuestService>();

            GuestMock.Setup(a => a.Create(new_guest));

            GuestController controller = new GuestController(GuestMock.Object);

            var httpResponse = controller.Post(httpRequest, mapper.Map<GuestDTO, GuestModel>(new_guest));
            var res = httpResponse.StatusCode;

            Assert.IsNotNull(httpResponse);

            Assert.AreEqual(res, System.Net.HttpStatusCode.Created);
        }
        [TestMethod]
        public void DeleteGuestByIdTest()
        {
            int guestId = 3;

            var GuestMock = new Mock<IGuestService>();

            GuestMock.Setup(a => a.Delete(guestId));

            GuestController controller = new GuestController(GuestMock.Object);

            var httpResponse = controller.Delete(httpRequest, guestId);
            var res = httpResponse.StatusCode;

            Assert.IsNotNull(httpResponse);
            Assert.AreEqual(res, System.Net.HttpStatusCode.NoContent);
        }
    }
}
