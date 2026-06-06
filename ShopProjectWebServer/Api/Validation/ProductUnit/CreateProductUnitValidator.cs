using ShopProjectWebServer.Api.DtoModels.ProductCodeUKTZED;
using ShopProjectWebServer.Api.DtoModels.ProductUnit;
using ShopProjectWebServer.Api.Validation.Helper;
using ShopProjectWebServer.Api.Validation.Interface;

namespace ShopProjectWebServer.Api.Validation.ProductUnitValidation
{
    public class CreateProductUnitValidator : IValidator<CreateProductUnitDto>
    {
        public ValidationResult Validation(CreateProductUnitDto model)
        {
            var error = new List<string>();

            if (string.IsNullOrWhiteSpace(model.NameUnit))
            {
                error.Add("Ведіть назву товарної одиниці");
            }

            if (model.Number == 0)
            {
                error.Add("Ведіть номер товарної одиниці");
            }

            if (string.IsNullOrWhiteSpace(model.ShortNameUnit))
            {
                error.Add("Ведіть скорочену назву товарної одиниці");
            }

            return new ValidationResult(error);
        }
    }
}
