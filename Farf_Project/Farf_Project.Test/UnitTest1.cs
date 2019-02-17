using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Farf_Project.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            //SequenceResource resource = new SequenceResource
            //{
            //    Id = Guid.NewGuid().ToString(),
            //    Name = "AAAA",
            //    Description = "BBBB"
            //};

            //Guid varOneId = Guid.NewGuid();
            //Guid varTwoId = Guid.NewGuid();

            //VariableResource[] variables = {
            //    new VariableResource()
            //    {
            //        Id = varOneId.ToString(),
            //        Name = "Var1",
            //        Type = "String"
            //    },
            //    new VariableResource()
            //    {
            //         Id = varTwoId.ToString(),
            //         Name = "Var2",
            //         Type = "Int"
            //    }
            //};

            //resource.Variables = variables;

            //// Sequence entity = SequenceResource.Map(resource);

            //// Test Resource and Entity base properties
            //Assert.AreEqual(resource.Id, entity.Id, "The value should be the same");
            //Assert.AreEqual(resource.Name, entity.Name, "The value should be the same");
            //Assert.AreEqual(resource.Description, entity.Description, "The value should be the same");
            //Assert.IsNotNull(resource.Variables, "The value should not be null");
            //Assert.IsNotNull(entity.Variables, "The value should not be null");
            //Assert.AreEqual(resource.Variables.Count(), 2, "The collection should have the size '2'");
            //Assert.AreEqual(entity.Variables.Count(), 2, "The collection should have the size '2'");
            //Assert.AreEqual(resource.Variables.Count(), entity.Variables.Count(), "The collections should have the same size");

            //// Test the resource and entity variables collection
            //VariableResource resourceVarOne = resource.Variables.FirstOrDefault(v => v.Id.Equals(varOneId));
            //Variable entityVarOne = entity.Variables.FirstOrDefault(v => v.Id.Equals(varOneId));

            //Assert.IsNotNull(resourceVarOne, "The value should not be null");
            //Assert.IsNotNull(entityVarOne, "The value should not be null");
            //Assert.AreEqual(resourceVarOne.Id, entityVarOne.Id, "The value should be the same");
            //Assert.AreEqual(resourceVarOne.Name, entityVarOne.Name, "The value should be the same");
            //Assert.AreEqual(resourceVarOne.Type, entityVarOne.Type, "The value should be the same");
        }
    }
}
