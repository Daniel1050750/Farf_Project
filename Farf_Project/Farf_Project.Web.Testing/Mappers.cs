using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Farf_Project.Web.Testing
{
    [TestClass]
    public class Mappers
    {
        [TestMethod]
        public void UserResource_To_User()
        {
            var resources = new Farf_Project.Web.UserResource
            {
                Id = Guid.NewGuid().ToString(),
                Username = "DSilva",
                State = "Active",
                Role = "Administrator"
            };

            var entity = UserResource.Map(resources);

            // Test Resource and Entity base properties
            Assert.IsNotNull(resources.Username, "The value should not be null");
            Assert.AreEqual(resources.Username, entity.Username, "The value should be the same");

            Assert.IsNotNull(resources.State, "The value should not be null");

            Assert.IsNotNull(resources.Role, "The value should not be null");
        }

        [TestMethod]
        public void RouteResource_To_Route()
        {
            var resource = new Farf_Project.Web.RouteResource
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Route A",
                PointStart = Guid.NewGuid().ToString(),
                PointEnd = Guid.NewGuid().ToString(),
                RoutePrice = 123,
                RouteTime = 234,
                State = "Active"
            };

            var entity = RouteResource.Map(resource);

            // Test Resource and Entity base properties
            Assert.AreEqual(resource.Name, entity.Name, "The value should be the same");
            Assert.IsNotNull(resource.Name, "The value should not be null");
            
            Assert.IsNotNull(resource.PointStart, "The value should not be null");
            
            Assert.IsNotNull(resource.PointEnd, "The value should not be null");

            Assert.AreEqual(resource.RoutePrice, entity.RoutePrice, "The value should be the same");
            Assert.IsNotNull(resource.RoutePrice, "The value should not be null");

            Assert.AreEqual(resource.RouteTime, entity.RouteTime, "The value should be the same");
            Assert.IsNotNull(resource.RouteTime, "The value should not be null");
            
            Assert.IsNotNull(resource.State, "The value should not be null");
        }

        [TestMethod]
        public void PointResource_To_Point()
        {
            var resource = new Farf_Project.Web.PointResource
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Point A",
                Address = "China",
                State = "Active"
            };

            var entity = PointResource.Map(resource);

            // Test Resource and Entity base properties
            Assert.AreEqual(resource.Name, entity.Name, "The value should be the same");
            Assert.IsNotNull(resource.Name, "The value should not be null");

            Assert.AreEqual(resource.Address, entity.Address, "The value should be the same");
            Assert.IsNotNull(resource.Address, "The value should not be null");
            
            Assert.IsNotNull(resource.State, "The value should not be null");
        }
    }
}
