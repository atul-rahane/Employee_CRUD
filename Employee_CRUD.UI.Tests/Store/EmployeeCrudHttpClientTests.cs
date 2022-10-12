using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Employee_CRUD.Domain.HttpClients;
using Employee_CRUD.Domain.Models;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Employee_CRUD.UI.Tests.Store
{
    public class EmployeeCrudHttpClientTests
    {
        private Mock<CRUDAPIKey> _mockApiKey;
        private EmployeeCrudHttpClient _employeeCrudHttpClient;
        private Mock<HttpMessageHandler>? _msgHandler;

        [SetUp]
        public void Setup()
        {
            _msgHandler = new Mock<HttpMessageHandler>();
            _mockApiKey = new Mock<CRUDAPIKey>("testKey");

            // use real http client with mocked handler here
            var httpClient = new HttpClient(_msgHandler.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };
            _employeeCrudHttpClient = new EmployeeCrudHttpClient(httpClient, _mockApiKey.Object);
        }

        [Test]
        public async Task EmployeeCrudHttpClient_GetAsync_Returns_List_From_JsonResponse()
        {
            Employee expectedEmployee = new Employee(1, "test", "test@test.com", "female", "active");
            List<Employee> list = new List<Employee>();
            list.Add(expectedEmployee);

            var mockedProtected = _msgHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(list))
                });

            var result = await _employeeCrudHttpClient.GetAsync<IEnumerable<Employee>>("");

            Assert.AreEqual(result.ToList()[0].Id, list[0].Id);
            Assert.AreEqual(result.ToList()[0].Name, list[0].Name);
            Assert.AreEqual(result.ToList()[0].Email, list[0].Email);
            Assert.AreEqual(result.ToList()[0].Gender, list[0].Gender);
            Assert.AreEqual(result.ToList()[0].Status, list[0].Status);
        }

        [Test]
        public async Task EmployeeCrudHttpClient_PostAsync_Returns_Created_Entity_From_JsonResponse()
        {
            Employee expectedEmployee = new Employee(1, "test", "test@test.com", "female", "active");
            
            var mockedProtected = _msgHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(expectedEmployee))
                });

            var result = await _employeeCrudHttpClient.PostAsync<Employee>("", expectedEmployee);

            Assert.AreEqual(result.Id, expectedEmployee.Id);
            Assert.AreEqual(result.Name, expectedEmployee.Name);
            Assert.AreEqual(result.Email, expectedEmployee.Email);
            Assert.AreEqual(result.Gender, expectedEmployee.Gender);
            Assert.AreEqual(result.Status, expectedEmployee.Status);
        }

        [Test]
        public async Task EmployeeCrudHttpClient_PutAsync_Returns_Updated_Entity_From_JsonResponse()
        {
            Employee expectedEmployee = new Employee(1, "test", "test@test.com", "female", "active");
            
            var mockedProtected = _msgHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(expectedEmployee))
                });

            var result = await _employeeCrudHttpClient.PutAsync<Employee>("", expectedEmployee);

            Assert.AreEqual(result.Id, expectedEmployee.Id);
            Assert.AreEqual(result.Name, expectedEmployee.Name);
            Assert.AreEqual(result.Email, expectedEmployee.Email);
            Assert.AreEqual(result.Gender, expectedEmployee.Gender);
            Assert.AreEqual(result.Status, expectedEmployee.Status);
        }

        [Test]
        public async Task EmployeeCrudHttpClient_DeleteAsync_Returns_Status_From_JsonResponse()
        {
            var mockedProtected = _msgHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK
                });

            var result = await _employeeCrudHttpClient.DeleteAsync(1);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task EmployeeCrudHttpClient_DeleteAsync_Returns_Failed_Status_From_JsonResponse()
        {
            var mockedProtected = _msgHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError
                });

            var result = await _employeeCrudHttpClient.DeleteAsync(1);

            Assert.IsFalse(result);
        }
    }
}