using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Authorization;
using UserService.Controllers;
using UserService.Models.DTO;

namespace UserServiceTest
{
    public class RestrictedControllerTest
    {
        private RestrictedController _controller;
        private Guid testGuid = Guid.NewGuid();

        [SetUp]
        public void Setup()
        {
            var mock = new Mock<IControllerUserSource>();
            mock.SetReturnsDefault(new UserDto() { Guid = testGuid });
            _controller = new RestrictedController(mock.Object);
        }


        [Test]
        public void GetIdFromTokenTest()
        {
            var result = (ObjectResult)_controller.GetIdFromToken();
            Assert.Multiple(() =>
            {
                Assert.That(result.StatusCode, Is.EqualTo(200));
                Assert.That(result.Value, Is.EqualTo(testGuid));
            });
        }
    }
}
