using ShopProjectWebServer.Api.DtoModels.ProductCodeUKTZED;
using ShopProjectWebServer.Api.Validation.Helper;
using ShopProjectWebServer.Api.Validation.Interface;

namespace ShopProjectWebServer.Api.Validation.ProductCodeUKTZEDValidation
{
    public class CreateProductCodeUTKZEDValidator : IValidator<CreateProductUKTZEDDto>
    {
        public ValidationResult Validation(CreateProductUKTZEDDto model)
        {
            var error = new List<string>();

            if (string.IsNullOrWhiteSpace(model.NameCode))
            {
                error.Add("Ведіть назву товарного коду");
            }

            if (string.IsNullOrWhiteSpace(model.Code))
            {
                error.Add("Ведіть товарий код");
            }  
            return new ValidationResult(error);
        }
    }
}
