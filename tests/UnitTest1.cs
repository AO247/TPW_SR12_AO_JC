using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace tests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1()
    {
        Assert.AreEqual(Model.addition(1,2),3);
    }
}