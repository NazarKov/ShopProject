using ShopProjectWebServer.Api.Validation.Helper;

namespace ShopProjectWebServer.Api.Validation.Interface
{
    public interface IValidator<T>
    {
        ValidationResult Validation(T model);
    }
}
