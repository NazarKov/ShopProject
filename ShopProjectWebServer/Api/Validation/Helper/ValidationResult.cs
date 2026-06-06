namespace ShopProjectWebServer.Api.Validation.Helper
{
    public class ValidationResult
    {
        public bool isValid => Errors.Count() == 0;
        public List<string> Errors { get;} = new List<string>();

        public ValidationResult(List<string> errors)
        {
            Errors = errors;
        }
    }
}
