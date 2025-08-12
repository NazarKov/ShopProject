using ShopProjectSQLDataBase.Context;
using ShopProjectSQLDataBase.Entities; 
using ShopProjectSQLDataBase.Helper;
using ShopProjectWebServer.DataBase.Helpers;
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
                    context.ElectronicSignatureKeys.Load();
                    if (item != null)
                    {

                        var user = context.Users.FirstOrDefault(i=>i.Login == item.Login);

                        if (user != null)
                        {
                            throw new Exception("Користувач існує");
                        }

                        item.UserRole = context.UserRoles.Find(item.UserRole.ID);
                        if (item.SignatureKey != null)
                        {
                            context.ElectronicSignatureKeys.Add(item.SignatureKey);
                        }
                        context.Users.Add(item);
                    }
                }
                context.SaveChanges();
            }
        }

        public void Delete(UserEntity item)
        {
            using (ContextDataBase context = new ContextDataBase(_connectionString))
            {
                if (context != null)
                {
                    context.UserTokens.Load();
                    context.UserRoles.Load();
                    context.ElectronicSignatureKeys.Load();
                    context.Users.Load();

                    if(context.Users != null)
                    {
                        var user = context.Users.Find(item.ID);
                        context.Users.Remove(user);
                    }
                    context.SaveChanges();
                }
            }
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

        public PaginatorData<UserEntity> GetAllPageColumn(double page, double countColumn, TypeStatusUser statusUser)
        {
            using (ContextDataBase context = new ContextDataBase(_connectionString))
            {
                if (context != null)
                {
                    context.UserRoles.Load();
                    context.UserTokens.Load(); 
                    context.ElectronicSignatureKeys.Load();
                    context.Users.Load();

                    if (context.Users!=null && context.Users.Count() != 0)
                    {
                        double pages;

                        int countEnd = (int)(page * countColumn);
                        int countStart = (int)(countEnd - countColumn);

                        PaginatorData<UserEntity> result = new PaginatorData<UserEntity>();

                        if (statusUser == TypeStatusUser.Unknown)
                        {
                            result.Page = (int)page;
                            result.Data = context.Users.OrderBy(i => i.ID)
                                                          .Skip(countStart)
                                                          .Take((int)countColumn).ToList();

                            pages = context.Users.Count() / countColumn;
                        }
                        else
                        {
                            result.Page = (int)page;
                            var users = context.Users.Where(item => item.Status == statusUser).ToList();
                            result.Data = users.OrderBy(i => i.ID)
                                                  .Skip(countStart)
                                                  .Take((int)countColumn).ToList();

                            pages = users.Count() / countColumn;
                        }

                        int pagesCount = 0;

                        if (!(pages % 2 == 0))
                        {
                            pagesCount = (int)pages;
                            pagesCount++;
                        }
                        result.Pages = pagesCount;

                        return result;
                    }
                    else
                    {
                        return new PaginatorData<UserEntity>();
                    }
                }
                return new PaginatorData<UserEntity>();
            }
        }

        public PaginatorData<UserEntity> GetUsersByNamePageColumn(string name, double page, double countColumn, TypeStatusUser statusUser)
        {
            using (ContextDataBase context = new ContextDataBase(_connectionString))
            {
                if (context != null)
                {
                    context.UserRoles.Load();
                    context.UserTokens.Load();
                    context.ElectronicSignatureKeys.Load();
                    context.Users.Load();

                    if (context.Users != null && context.Users.Count() != 0)
                    {
                        double pages;

                        int countEnd = (int)(page * countColumn);
                        int countStart = (int)(countEnd - countColumn);

                        PaginatorData<UserEntity> result = new PaginatorData<UserEntity>();

                        if (statusUser == TypeStatusUser.Unknown)
                        {
                            result.Page = (int)page;
                            var users = context.Users.Where(i => i.FullName.Contains(name)).ToList();
                            result.Data = users.OrderBy(i => i.ID)
                                                  .Skip(countStart)
                                                  .Take((int)countColumn).ToList();

                            pages = users.Count() / countColumn;
                        }
                        else
                        {
                            result.Page = (int)page;
                            var users = context.Users.Where(t => t.Status == statusUser)
                                                           .Where(i => i.FullName.Contains(name)).ToList();
                            result.Data = users.OrderBy(i => i.ID)
                                                  .Skip(countStart)
                                                  .Take((int)countColumn).ToList();

                            pages = users.Count() / countColumn;
                        }

                        int pagesCount = 0;

                        if (!(pages % 2 == 0))
                        {
                            pagesCount = (int)pages;
                            pagesCount++;
                        }
                        result.Pages = pagesCount;

                        return result;

                    }
                    else
                    {
                        throw new Exception("Неможливий пошук оскільки немає товарів");
                    }

                }
                return new PaginatorData<UserEntity>();
            }
        }

        public void Update(UserEntity item)
        {
            using (ContextDataBase context = new ContextDataBase(_connectionString))
            {
                if (context != null)
                {
                    context.UserTokens.Load();
                    context.UserRoles.Load();
                    context.ElectronicSignatureKeys.Load();
                    context.Users.Load();
                    UserEntity user;
                    if (context.Users!=null)
                    {
                        user = context.Users.Find(item.ID);

                        if (user != null)
                        {
                            user.Login = item.Login;
                            user.Password = item.Password;
                            user.Email = item.Email;
                            user.FullName = item.FullName;
                            user.TIN = item.TIN;
                            user.Status = item.Status;
                            

                            if(item.SignatureKey != null)
                            {
                                context.ElectronicSignatureKeys.Add(item.SignatureKey);
                                user.SignatureKey = item.SignatureKey;
                            }

                            if (context.UserRoles != null)
                            {
                                var role = context.UserRoles.FirstOrDefault(r => r.NameRole == item.UserRole.NameRole);
                                if(role != null)
                                {
                                    user.UserRole = role;
                                }
                            }
                            
                        }
                        else
                        {
                            throw new Exception("Користувача не знайдено");
                        }

                    }
                    context.SaveChanges();

                }
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
