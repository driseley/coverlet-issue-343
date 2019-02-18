using DemoLibrary;
using System;
using System.IO;
using Xunit;

namespace DemoLibraryTests
{
	public class DemoClassTests
	{
		[Fact]
		public void InvokeFunction_Test()
		{
			DemoClass demoClass = new DemoClass();
			bool result = demoClass.InvokeFunction();
			Assert.False(result);
		}

		[Fact]
		public void InvokeAnonymous_Test()
		{
			DemoClass demoClass = new DemoClass();
			bool result = demoClass.InvokeAnonymous();
			Assert.False(result);
		}
	}
}
