using System;
using System.Runtime.Serialization;

namespace Zen.Exceptions
{
	[Serializable]
	public class ItemCodeNotRecognisedException : Exception
	{
		public ItemCodeNotRecognisedException()
		{
		}

		public ItemCodeNotRecognisedException(string message) : base(message)
		{
		}

		public ItemCodeNotRecognisedException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected ItemCodeNotRecognisedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
