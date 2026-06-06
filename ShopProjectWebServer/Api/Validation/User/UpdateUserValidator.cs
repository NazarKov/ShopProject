using ShopProjectWebServer.Api.DtoModels.User;
using ShopProjectWebServer.Api.Validation.Helper;
using ShopProjectWebServer.Api.Validation.Interface;

namespace ShopProjectWebServer.Api.Validation.User
{
    public class UpdateUserValidator : IValidator<UpdateUserDto>
    {
        public ValidationResult Validation(UpdateUserDto model)
        {
            var error = new List<string>();

            if (string.IsNullOrWhiteSpace(model.FullName))
            {
                error.Add("Ведіть назву користувача");
            }

            if (string.IsNullOrWhiteSpace(model.Password))
            {
                error.Add("Ведіть пароль");
            }

            if (string.IsNullOrWhiteSpace(model.Login))
            {
                error.Add("Ведіть логін");
            }

            return new ValidationResult(error);
        }
    }
}
