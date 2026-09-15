namespace ShopProjectWebServer.Api.Common
{
    public enum ErrorType
    {
        None,
        Validation,
        NotFound,
        Authorized,
        Unauthorized,
        Conflict,
        Server,
        ObjectExists
    }
}
