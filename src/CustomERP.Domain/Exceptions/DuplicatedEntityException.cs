namespace CustomERP.Domain.Exceptions
{
    [Serializable]
    public class DuplicatedEntityException : Exception
    {
        public DuplicatedEntityException() { }
        public DuplicatedEntityException(string message) : base(message) { }
        public DuplicatedEntityException(string message, Exception inner) : base(message, inner) { }
        protected DuplicatedEntityException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
