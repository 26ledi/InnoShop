namespace InnoShop.Exceptions.Shared.Exceptions
{
    public class AlreadyExistException : Exception
    {
        public AlreadyExistException() { }
        public AlreadyExistException(string message) : base(message) { }
    }
}
