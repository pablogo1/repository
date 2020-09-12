using Repository.EF.Tests.Model;
using System;
using System.Linq;

namespace Repository.EF.Tests.Shared
{
    public class TestDatabaseFixture
    {
        public TestDatabaseFixture()
        {
            DataContextFactory = new TestDataContextFactory();
        }

        public TestDataContextFactory DataContextFactory { get; }
    }
}
