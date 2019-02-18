using System;
using System.IO;
using System.Text;

namespace DemoLibrary
{
	public class DemoClass
	{
		protected T WriteToStream<T>(Func<Stream, bool, T> getResultFunction)
		{
			using (var stream = new MemoryStream())
			{
				var result = getResultFunction(stream, false);
				stream.Close();
				Console.WriteLine(Encoding.Default.GetString(stream.ToArray()));
				return result;
			}
		}

		public bool WriteHelloWorldToStream(Stream stream , bool condition)
		{
			if (condition) stream.Write(Encoding.Default.GetBytes("Hello World"));
			else stream.Write(Encoding.Default.GetBytes("Goodbye World"));        
			return condition;
		}

		public bool InvokeFunction()
		{
			return WriteToStream((stream,condition) => WriteHelloWorldToStream(stream, condition));
		}

		public bool InvokeAnonymous()
		{
			return WriteToStream((stream, condition) =>
			{
				if (condition) stream.Write(Encoding.Default.GetBytes("Hello World"));
				else stream.Write(Encoding.Default.GetBytes("Goodbye World"));               
				return condition;
			}
			);
		}
	}
}