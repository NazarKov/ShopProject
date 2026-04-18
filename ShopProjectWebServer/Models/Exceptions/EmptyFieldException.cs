namespace ShopProjectWebServer.Models.Exceptions
{
    public class EmptyFieldException : Exception
    {
        public EmptyFieldException() { }
        public EmptyFieldException(string message) : base(message) { }
    }
}
