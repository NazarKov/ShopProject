namespace ShopProjectWebServer.Api.Common
{
    public enum ErrorType
    {
        None,
        Validation,
        NotFound,
        Unauthorized,
        Conflict,
        Server,
        ObjectExists
    }
}
