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
    public class PriceCategoryControllerTest
    {
        HttpConfiguration httpConfiguration;
        HttpRequestMessage httpRequest;
        private IMapper mapper;
        public PriceCategoryControllerTest()
        {
            httpConfiguration = new HttpConfiguration();
            httpRequest = new System.Net.Http.HttpRequestMessage();
            httpRequest.Properties[System.Web.Http.Hosting.HttpPropertyKeys.HttpConfigurationKey] = httpConfiguration;
            mapper = new MapperConfiguration(cfg =>
           cfg.CreateMap<PriceCategoryDTO, PriceCategoryModel>()).CreateMapper();
        }

        [TestMethod]
        public void GetAllPriceCategoriesTest()
        {
            var PriceCategoryMock = new Mock<IPriceCategoryService>();
            PriceCategoryMock.Setup(a => a.GetAllPriceCategories()).Returns(new List<PriceCategoryDTO>());

            var expected = mapper.Map<IEnumerable<PriceCategoryDTO>, List<PriceCategoryModel>>(PriceCategoryMock.Object.GetAllPriceCategories());

            PriceCategoryController controller = new PriceCategoryController(PriceCategoryMock.Object);
            var result = controller.Get();

            CollectionAssert.AreEqual(expected, result.ToList());
        }
        [TestMethod]

        public void GetPriceCategoryByIdTest()
        {
            int PriceCategoryId = 5;
            var PriceCategoryMock = new Mock<IPriceCategoryService>();

            PriceCategoryMock.Setup(a => a.Get(PriceCategoryId)).Returns(new PriceCategoryDTO());

            PriceCategoryController controller = new PriceCategoryController(PriceCategoryMock.Object);

            var httpResponse = controller.Get(httpRequest, PriceCategoryId);
            var result = httpResponse.Content.ReadAsAsync<PriceCategoryModel>();
            var expected = mapper.Map<PriceCategoryDTO, PriceCategoryModel>(PriceCategoryMock.Object.Get(PriceCategoryId));

            Assert.AreEqual(expected, result.Result);
        }

        [TestMethod]

        public void GetPriceCategoryById_CheckStatusCode_Test()
        {
            int PriceCategoryId = 3;
            var PriceCategoryMock = new Mock<IPriceCategoryService>();

            PriceCategoryMock.Setup(a => a.Get(PriceCategoryId)).Returns(new PriceCategoryDTO());

            PriceCategoryController controller = new PriceCategoryController(PriceCategoryMock.Object);

            var httpResponse = controller.Get(httpRequest, PriceCategoryId);
            var res = httpResponse.StatusCode;
            Assert.AreEqual(res, System.Net.HttpStatusCode.OK);
        }
        [TestMethod]
        public void CreatePriceCategoryTest()
        {

            PriceCategoryDTO new_Pricecategory = new PriceCategoryDTO()
            {

                Price = 500.5,
                StartDate = new DateTime(2022, 01, 01),
                EndDate = new DateTime(2022, 07, 01)


            };
            var PriceCategoryMock = new Mock<IPriceCategoryService>();

            PriceCategoryMock.Setup(a => a.Create(new_Pricecategory));

            PriceCategoryController controller = new PriceCategoryController(PriceCategoryMock.Object);

            var httpResponse = controller.Post(httpRequest, mapper.Map<PriceCategoryDTO, PriceCategoryModel>(new_Pricecategory));
            var res = httpResponse.StatusCode;

            Assert.IsNotNull(httpResponse);

            Assert.AreEqual(res, System.Net.HttpStatusCode.Created);
        }
        [TestMethod]
        public void DeletePriceCategoryByIDTest()
        {
            int PriceCategoryId = 4;

            var PriceCategoryMock = new Mock<IPriceCategoryService>();

            PriceCategoryMock.Setup(a => a.Delete(PriceCategoryId));

            PriceCategoryController controller = new PriceCategoryController(PriceCategoryMock.Object);

            var httpResponse = controller.Delete(httpRequest, PriceCategoryId);
            var res = httpResponse.StatusCode;

            Assert.IsNotNull(httpResponse);

            Assert.AreEqual(res, System.Net.HttpStatusCode.NoContent);
        }
    }
}
