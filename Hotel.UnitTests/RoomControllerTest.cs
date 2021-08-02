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
    public class RoomControllerTest
    {
        HttpConfiguration httpConfiguration;
        HttpRequestMessage httpRequest;
        private IMapper mapper;
        public RoomControllerTest()
        {
            httpConfiguration = new HttpConfiguration();
            httpRequest = new System.Net.Http.HttpRequestMessage();
            httpRequest.Properties[System.Web.Http.Hosting.HttpPropertyKeys.HttpConfigurationKey] = httpConfiguration;

           
            mapper = new MapperConfiguration(cfg =>
            cfg.CreateMap<RoomDTO, RoomModel>()).CreateMapper();
        }

        [TestMethod]
        public void GetAllRoomsTest()
        {
            var RoomMock = new Mock<IRoomService>();
            

            RoomMock.Setup(a => a.GetAllRooms()).Returns(new List<RoomDTO>());

            var expected = mapper.Map<IEnumerable<RoomDTO>, List<RoomModel>>(RoomMock.Object.GetAllRooms());

            RoomController controller = new RoomController(RoomMock.Object);
            var result = controller.Get();

            CollectionAssert.AreEqual(expected, result.ToList());
        }

        [TestMethod]

        public void GetRoomByIdTest()
        {
            int RoomId = 6;

            var RoomMock = new Mock<IRoomService>();


            RoomMock.Setup(a => a.Get(RoomId)).Returns(new RoomDTO());

            RoomController controller = new RoomController(RoomMock.Object);

            var httpResponse = controller.Get(httpRequest, RoomId);
            var result = httpResponse.Content.ReadAsAsync<RoomModel>();
            var expected = mapper.Map<RoomDTO, RoomModel>(RoomMock.Object.Get(RoomId));

            Assert.AreEqual(expected, result.Result);
        }


        [TestMethod]

        public void GetRoomById_CheckStatusCode_Test()
        {
            int RoomId = 2;

            var RoomMock = new Mock<IRoomService>();


            RoomMock.Setup(a => a.Get(RoomId)).Returns(new RoomDTO());

            RoomController controller = new RoomController(RoomMock.Object);

            var httpResponse = controller.Get(httpRequest, RoomId);
            var res = httpResponse.StatusCode;

            Assert.AreEqual(res, System.Net.HttpStatusCode.OK);
        }

        [TestMethod]
        public void CreateRoomTest()
        {
           
            RoomDTO new_room = new RoomDTO()
            {
                Id=9,
                Name="500a",
                Active = false

            };
            var RoomMock = new Mock<IRoomService>();

            RoomMock.Setup(a => a.Create(new_room));

            RoomController controller = new RoomController(RoomMock.Object);

            var httpResponse = controller.Post(httpRequest,mapper.Map<RoomDTO, RoomModel>(new_room));
            var res = httpResponse.StatusCode;

            Assert.IsNotNull(httpResponse);

            Assert.IsNotNull(httpResponse);

            Assert.AreEqual(res, System.Net.HttpStatusCode.Created);
        }

        [TestMethod]
        public void DeleteRoomByIDTest()
        {
            int RoomId = 7;

            var RoomMock = new Mock<IRoomService>();

            RoomMock.Setup(a => a.Delete(RoomId));

            RoomController controller = new RoomController(RoomMock.Object);

            var httpResponse = controller.Delete(httpRequest, RoomId);
            var res = httpResponse.StatusCode;

            Assert.IsNotNull(httpResponse);

            Assert.AreEqual(res, System.Net.HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void UpdateRoomTest()
        {
            int id = 3;
            RoomDTO room_with_updates = new RoomDTO()
            {
                Active = false
            };
            var RoomMock = new Mock<IRoomService>();

            RoomMock.Setup(a => a.Update(id,room_with_updates));
            
            RoomController controller = new RoomController(RoomMock.Object);

            var httpResponse = controller.Put(httpRequest, id,mapper.Map<RoomDTO,RoomModel>(room_with_updates));
            var res = httpResponse.StatusCode;

            Assert.IsNotNull(httpResponse);

            Assert.AreEqual(res, System.Net.HttpStatusCode.OK);
        }


    }
}
