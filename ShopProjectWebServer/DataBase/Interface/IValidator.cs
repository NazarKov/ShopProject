namespace ShopProjectWebServer.DataBase.Interface
{
    public interface IValidator<T>
    {
        public bool Validate(T item , out List<string> errors);
    }
}
