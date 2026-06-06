namespace ShopProjectWebServer.Services.Common.Enum
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
