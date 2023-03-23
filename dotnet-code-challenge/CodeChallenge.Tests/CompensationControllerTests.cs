using CodeChallenge.Models;
using CodeCodeChallenge.Tests.Integration.Extensions;
using CodeCodeChallenge.Tests.Integration.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenge.Tests.Integration
{
    [TestClass]
    public class CompensationControllerTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        // Attribute ClassInitialize requires this signature
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer();
            _httpClient = _testServer.NewClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        [TestMethod]
        public void CreateCompensation_Returns_Created()
        {
            // Arrange
            var compensation = new Compensation()
            {
                CompensationId = "2",
                EffectiveDate = DateTime.Now,
                Salary = 10000
            };

            var requestContent = new JsonSerialization().ToJson(compensation);

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/compensation/16a596ae-edd3-4847-99fe-c4518e82c86f",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var newCompensation = response.DeserializeContent<Compensation>();
            Assert.IsNotNull(newCompensation.CompensationId);
            Assert.AreEqual(newCompensation.Salary, newCompensation.Salary);
            Assert.AreEqual(newCompensation.EffectiveDate, newCompensation.EffectiveDate);
        }

        [TestMethod]
        public async Task GetCompensationByEmployeeId_Returns_Ok()
        {
            // Execute
            var getRequestTask = await _httpClient.GetAsync("api/compensation/16a596ae-edd3-4847-99fe-c4518e82c86f");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, getRequestTask.StatusCode);

            var compensation = getRequestTask.DeserializeContent<List<Compensation>>();

            //Assert compensation returns an object
            Assert.AreEqual(1, compensation.Count);

            //Assert data is correct
            Assert.AreEqual(20000, compensation.ElementAt(0).Salary);
        }
    }
}
