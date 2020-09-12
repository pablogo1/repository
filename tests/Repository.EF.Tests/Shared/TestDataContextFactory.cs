namespace Repository.EF.Tests.Shared
{
    public class TestDataContextFactory
    {
        private readonly InMemoryDbContextFactory dbContextFactory;

        public TestDataContextFactory()
        {
            dbContextFactory = new InMemoryDbContextFactory();
        }

        public TestDataContext CreateTestDataContext()
        {
            var dbContext = dbContextFactory.CreateDbContext("test");

            return new TestDataContext(dbContext);
        }
    }
}
