using DataAbstractions.Dapper;
using DemoLibrary;
using Moq;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Xunit;
using static DemoLibrary.DemoAsyncClass;

namespace DemoLibraryTests
{
    public class DemoAsyncClassTests
	{
		[Fact]
		public async Task InvokeFunction_Test()
		{
            Mock<IConnectionFactory> connectionFactory = new Mock<IConnectionFactory>();
            Mock<IDataAccessor> dataAccessor = new Mock<IDataAccessor>();
            connectionFactory.Setup(c => c.GetDataAccessor()).Returns(dataAccessor.Object);
            dataAccessor.Setup(c => c.QueryAsync<Job>(It.IsAny<string>(), It.IsAny<object>(), null, null, CommandType.StoredProcedure))
                                .ReturnsAsync(new List<Job>() { new Job() });
            DemoAsyncClass demoClass = new DemoAsyncClass(connectionFactory.Object);

			var results = await demoClass.InvokeFunction();

            dataAccessor.VerifyAll();
            Assert.NotEmpty(results);
		}

		[Fact]
		public async Task InvokeAnonymous_Test()
		{
            Mock<IConnectionFactory> connectionFactory = new Mock<IConnectionFactory>();
            Mock<IDataAccessor> dataAccessor = new Mock<IDataAccessor>();
            connectionFactory.Setup(c => c.GetDataAccessor()).Returns(dataAccessor.Object);
            dataAccessor.Setup(c => c.QueryAsync<Job>(It.IsAny<string>(), It.IsAny<object>(), null, null, CommandType.StoredProcedure))
                                            .ReturnsAsync(new List<Job>() { new Job() });
            DemoAsyncClass demoClass = new DemoAsyncClass(connectionFactory.Object);

            var results = await demoClass.InvokeAnonymous();

            dataAccessor.VerifyAll();
            Assert.NotEmpty(results);
        }
	}
}
