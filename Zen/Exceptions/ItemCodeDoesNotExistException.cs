using System;
using System.Runtime.Serialization;

namespace Zen.Exceptions
{
	[Serializable]
	public class ItemCodeDoesNotExistException : Exception
	{
		public ItemCodeDoesNotExistException()
		{
		}

		public ItemCodeDoesNotExistException(string message) : base(message)
		{
		}

		public ItemCodeDoesNotExistException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected ItemCodeDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
