﻿namespace CustomERP.Domain.Exceptions
{

	[Serializable]
	public class BusinessRuleViolationException : Exception
	{
		public BusinessRuleViolationException() { }
		public BusinessRuleViolationException(string message) : base(message) { }
		public BusinessRuleViolationException(string message, Exception inner) : base(message, inner) { }
		protected BusinessRuleViolationException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
