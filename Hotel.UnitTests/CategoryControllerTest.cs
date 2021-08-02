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
    public class CategoryControllerTest
    {
        HttpConfiguration httpConfiguration;
        HttpRequestMessage httpRequest;
        private IMapper mapper;
        public CategoryControllerTest()
        {
            httpConfiguration = new HttpConfiguration();
            httpRequest = new System.Net.Http.HttpRequestMessage();
            httpRequest.Properties[System.Web.Http.Hosting.HttpPropertyKeys.HttpConfigurationKey] = httpConfiguration;
            mapper = new MapperConfiguration(cfg =>
            cfg.CreateMap<CategoryDTO, CategoryModel>()).CreateMapper();
        }

        [TestMethod]
        public void GetAllCategoriesTest()
        {
            var mock = new Mock<ICategoryService>();
            mock.Setup(a => a.GetAllCategories()).Returns(new List<CategoryDTO>());

            var expected = mapper.Map<IEnumerable<CategoryDTO>, List<CategoryModel>>(mock.Object.GetAllCategories());

            CategoryController controller = new CategoryController(mock.Object);
            var result = controller.Get();

            CollectionAssert.AreEqual(expected, result.ToList());
        }

        [TestMethod]

        public void GetCategoryByIdTest()
        {
            int CategoryId = 2;
           

            var CategoryMock = new Mock<ICategoryService>();


            CategoryMock.Setup(a => a.Get(CategoryId)).Returns(new CategoryDTO());

            CategoryController controller = new CategoryController(CategoryMock.Object);

            var httpResponse = controller.Get(httpRequest, CategoryId);
            var result = httpResponse.Content.ReadAsAsync<CategoryModel>();
            var expected = mapper.Map<CategoryDTO, CategoryModel>(CategoryMock.Object.Get(CategoryId));

            Assert.AreEqual(expected, result.Result);
        }
        [TestMethod]

        public void GetCategoryById_CheckStatusCode_Test()
        {
            int CategoryId = 1;
            var CategoryMock = new Mock<ICategoryService>();


            CategoryMock.Setup(a => a.Get(CategoryId)).Returns(new CategoryDTO());

            CategoryController controller = new CategoryController(CategoryMock.Object);

            var httpResponse = controller.Get(httpRequest, CategoryId);
            var res = httpResponse.StatusCode;

            Assert.AreEqual(res, System.Net.HttpStatusCode.OK);
        }
        [TestMethod]
        public void CreateCategoryTest()
        {

            CategoryDTO new_category= new CategoryDTO()
            {
               
                Name = "President",
                Number_Of_Places=1
                

            };
            var CategoryMock = new Mock<ICategoryService>();

            CategoryMock.Setup(a => a.Create(new_category));

            CategoryController controller = new CategoryController(CategoryMock.Object);

            var httpResponse = controller.Post(httpRequest, mapper.Map<CategoryDTO, CategoryModel>(new_category));
            var res = httpResponse.StatusCode;

            Assert.IsNotNull(httpResponse);

            Assert.AreEqual(res, System.Net.HttpStatusCode.Created);
        }
        [TestMethod]
        public void DeleteCategoryByIDTest()
        {
            int CategoryId = 4;

            var CategoryMock = new Mock<ICategoryService>();

          CategoryMock.Setup(a => a.Delete(CategoryId));

            CategoryController controller = new CategoryController(CategoryMock.Object);

            var httpResponse = controller.Delete(httpRequest, CategoryId);
            var res = httpResponse.StatusCode;

            Assert.IsNotNull(httpResponse);

            Assert.AreEqual(res, System.Net.HttpStatusCode.NoContent);
        }
    }
}
