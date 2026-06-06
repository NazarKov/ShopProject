using ShopProjectWebServer.Api.DtoModels.Product;
using ShopProjectWebServer.Api.Validation.Helper; 

namespace ShopProjectWebServer.Api.Validation.ProductValidation
{
    public class CreateProductValidator : ShopProjectWebServer.Api.Validation.Interface.IValidator<CreateProductDto>
    {
        public ValidationResult Validation(CreateProductDto model)
        {
            var error = new List<string>();

            if (string.IsNullOrWhiteSpace(model.NameProduct))
            {
                error.Add("Ведіть назву товару"); 
            }

            if (string.IsNullOrWhiteSpace(model.Code))
            {
                error.Add("Ведіть штрихкод товару"); 
            }  

            if (model.Price == 0)
            {
                error.Add("Ведіть ціну товару"); 
            }

            if (model.Count == 0)
            {
                error.Add("Ведіть кількість товару"); 
            }

            if (string.IsNullOrWhiteSpace(model.Articule))
            {
                error.Add("Ведіть артикуль товару"); 
            }

            return new ValidationResult(error);
        }
    }
}
