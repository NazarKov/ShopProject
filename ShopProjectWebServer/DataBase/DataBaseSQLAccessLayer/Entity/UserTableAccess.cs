using ShopProjectDataBase.DataBase.Context;
using ShopProjectDataBase.DataBase.Entities;
using ShopProjectDataBase.DataBase.Model;
using ShopProjectWebServer.DataBase.Interface.EntityInterface;
using System.Data.Entity;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity
{
    public class UserTableAccess : IUserTableAccess 
    {
        private string _connectionString;
        public UserTableAccess(string ConnectionString)
        {
            _connectionString = ConnectionString;
        }

        public void Add(UserEntity item)
        {
            using (ContextDataBase context = new ContextDataBase(_connectionString))
            {
                if (context != null)
                {
                    context.Users.Load();
                    context.UserRoles.Load();
                    if (item != null)
                    {
                        item.UserRole = context.UserRoles.Find(item.UserRole.ID);
                        context.Users.Add(item);
                    }
                }
                context.SaveChanges();
            }
        }

        public void Delete(UserEntity item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserEntity> GetAll()
        {
            using (ContextDataBase context = new ContextDataBase(_connectionString))
            {
                if (context != null)
                {
                    context.Users.Load();
                    context.UserRoles.Load();
                    context.UserTokens.Load();
                    context.ObjectOwners.Load();
                    context.OperationsRecorders.Load();

                    if (context.Users.Count() != 0)
                    { 
                        return context.Users.ToList();
                    }
                    else
                    {
                        return null;
                    }
                }
                return null;
            }
        }

        public void UpdateParameter(Guid id, string nameParameter, object valueParameter)
        {
            using (ContextDataBase context = new ContextDataBase(_connectionString))
            {
                if (context != null)
                {
                    context.Users.Load();
                    context.OperationsRecorders.Load();
                    var user = context.Users.Find(id);
                    switch (nameParameter)
                    {
                        case nameof(user.Password):
                            {
                                user.Password = valueParameter.ToString();
                                break;
                            }
                        case nameof(user.Login):
                            {
                                user.Login = valueParameter.ToString();
                                break;
                            }
                        case nameof(user.Email):
                            {
                                user.Email = valueParameter.ToString();
                                break;
                            }
                    }

                }
                context.SaveChanges();
            }
        }
    }
}
