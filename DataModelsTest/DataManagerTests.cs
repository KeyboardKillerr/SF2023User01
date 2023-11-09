using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataModel;
using System;

namespace DataModel.Tests;

[TestClass()]
public class DataManagerTests
{
    [TestMethod()]
    public void GetTest()
    {
        var data = DataManager.Get(DataProvidersList.SqlServer);

        Assert.IsFalse(false);
    }
}