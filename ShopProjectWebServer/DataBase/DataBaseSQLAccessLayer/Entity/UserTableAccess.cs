using Microsoft.EntityFrameworkCore;
using ShopProjectDataBase.Context;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.DataBase.Helpers;
using ShopProjectWebServer.DataBase.Interface.EntityInterface;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity
{
    public class UserTableAccess : IUserTableAccess
    {
        private DbContextOptions<ContextDataBase> _option;
        public UserTableAccess(DbContextOptions<ContextDataBase> option)
        {
            _option = option;
        }
        public void Add(UserEntity item)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
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

        public UserEntity Authorization(string login, string password)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    IQueryable<UserEntity> query = context.Users.Include(u => u.UserRole).Include(s => s.SignatureKey).AsNoTracking();

                    var user = query.FirstOrDefault(item => item.Login == login);

                    if(user != null)
                    {
                        return user;
                    }
                    else
                    { 
                        throw new Exception("Користувача не занайдено");
                    }  
                }
                return null;
            }
        }

        public void Delete(UserEntity item)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
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
            using (ContextDataBase context = new ContextDataBase(_option))
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

        public UserEntity GetById(Guid id)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.Users.Load();
                    context.ElectronicSignatureKeys.Load();
                    context.UserRoles.Load(); 

                    if (context.Users != null && context.Users.Count() > 0)
                    {
                        var user = context.Users.FirstOrDefault(t => t.ID == id);
                        return user;
                    }
                }
                return new UserEntity();
            }
        }

        public IEnumerable<UserEntity> GetByNameAndStatus(string name, TypeStatusUser status)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            { 
                IQueryable<UserEntity> query = context.Users.Include(u=>u.UserRole).Include(s=>s.SignatureKey).AsNoTracking();

                if (status != TypeStatusUser.Unknown)
                {
                    query = query.Where(o => o.Status == status);
                }

                if (!(name == string.Empty))
                {
                    query = query.Where(o => o.FullName.Contains(name));
                }

                var result = query.ToList();
                return result;
            }
        }

        public UserEntity GetUser(string token)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.Users.Load();
                    context.ElectronicSignatureKeys.Load();
                    context.UserRoles.Load();
                    context.UserTokens.Load();
                    context.ObjectOwners.Load();
                    context.OperationsRecorders.Load();


                    if(context.Users!=null && context.Users.Count() > 0)
                    {
                        var userToken = context.UserTokens.FirstOrDefault(t => t.Token == token);

                        if (userToken != null)
                        {
                            return context.Users.FirstOrDefault(u => u.ID == userToken.User.ID); 
                        }
                    } 
                }
                return new UserEntity();
            }
        } 
        public void Update(UserEntity item)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
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
            using (ContextDataBase context = new ContextDataBase(_option))
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
