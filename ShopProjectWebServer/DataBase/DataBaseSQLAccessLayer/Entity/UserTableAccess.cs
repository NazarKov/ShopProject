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
        private readonly ContextDataBase _contextDataBase;
        public UserTableAccess(ContextDataBase contextDataBase)
        {
            _contextDataBase = contextDataBase;
        }
        public void Add(UserEntity item)
        {
            _contextDataBase.Users.Load();
            _contextDataBase.UserRoles.Load();
            _contextDataBase.ElectronicSignatureKeys.Load();
            if (item != null)
            {

                var user = _contextDataBase.Users.FirstOrDefault(i => i.Login == item.Login);

                if (user != null)
                {
                    throw new Exception("Користувач існує");
                }

                item.UserRole = _contextDataBase.UserRoles.Find(item.UserRole.ID);
                if (item.SignatureKey != null)
                {
                    _contextDataBase.ElectronicSignatureKeys.Add(item.SignatureKey);
                }
                _contextDataBase.Users.Add(item);
            }
            _contextDataBase.SaveChanges(); 
        }

        public UserEntity Authorization(string login, string password)
        {
            IQueryable<UserEntity> query = _contextDataBase.Users.Include(u => u.UserRole).Include(s => s.SignatureKey).AsNoTracking();

            var user = query.FirstOrDefault(item => item.Login == login);

            if (user != null)
            {
                return user;
            }
            else
            {
                throw new Exception("Користувача не занайдено");
            }
        }

        public void Delete(UserEntity item)
        {
            _contextDataBase.UserTokens.Load();
            _contextDataBase.UserRoles.Load();
            _contextDataBase.ElectronicSignatureKeys.Load();
            _contextDataBase.Users.Load();

            if (_contextDataBase.Users != null)
            {
                var user = _contextDataBase.Users.Find(item.ID);
                _contextDataBase.Users.Remove(user);
            }
            _contextDataBase.SaveChanges();
        }

        public IEnumerable<UserEntity> GetAll()
        {
            _contextDataBase.Users.Load();
            _contextDataBase.UserRoles.Load();
            _contextDataBase.UserTokens.Load();
            _contextDataBase.ObjectOwners.Load();
            _contextDataBase.OperationsRecorders.Load();

            if (_contextDataBase.Users.Count() != 0)
            {
                return _contextDataBase.Users.ToList();
            }
            else
            {
                return null;
            }
        }

        public UserEntity GetById(Guid id)
        {
            _contextDataBase.Users.Load();
            _contextDataBase.ElectronicSignatureKeys.Load();
            _contextDataBase.UserRoles.Load();

            if (_contextDataBase.Users != null && _contextDataBase.Users.Count() > 0)
            {
                var user = _contextDataBase.Users.FirstOrDefault(t => t.ID == id);
                return user;
            }
            return null;
        }

        public IEnumerable<UserEntity> GetByNameAndStatus(string name, TypeStatusUser status)
        {
            IQueryable<UserEntity> query = _contextDataBase.Users.Include(u => u.UserRole).Include(s => s.SignatureKey).AsNoTracking();

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

        public UserEntity GetUser(string token)
        {
            _contextDataBase.Users.Load();
            _contextDataBase.ElectronicSignatureKeys.Load();
            _contextDataBase.UserRoles.Load();
            _contextDataBase.UserTokens.Load();
            _contextDataBase.ObjectOwners.Load();
            _contextDataBase.OperationsRecorders.Load();


            if (_contextDataBase.Users != null && _contextDataBase.Users.Count() > 0)
            {
                var userToken = _contextDataBase.UserTokens.FirstOrDefault(t => t.Token == token);

                if (userToken != null)
                {
                    return _contextDataBase.Users.FirstOrDefault(u => u.ID == userToken.User.ID);
                }
            }
            return null;
        } 
        public void Update(UserEntity item)
        {
            _contextDataBase.UserTokens.Load();
            _contextDataBase.UserRoles.Load();
            _contextDataBase.ElectronicSignatureKeys.Load();
            _contextDataBase.Users.Load();
            UserEntity user;
            if (_contextDataBase.Users != null)
            {
                user = _contextDataBase.Users.Find(item.ID);

                if (user != null)
                {
                    user.Login = item.Login;
                    user.Password = item.Password;
                    user.Email = item.Email;
                    user.FullName = item.FullName;
                    user.TIN = item.TIN;
                    user.Status = item.Status;


                    if (item.SignatureKey != null)
                    {
                        _contextDataBase.ElectronicSignatureKeys.Add(item.SignatureKey);
                        user.SignatureKey = item.SignatureKey;
                    }

                    if (_contextDataBase.UserRoles != null)
                    {
                        var role = _contextDataBase.UserRoles.FirstOrDefault(r => r.NameRole == item.UserRole.NameRole);
                        if (role != null)
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
            _contextDataBase.SaveChanges();
        }

        public void UpdateParameter(Guid id, string nameParameter, object valueParameter)
        {
            _contextDataBase.Users.Load();
            _contextDataBase.OperationsRecorders.Load();
            var user = _contextDataBase.Users.Find(id);
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
            _contextDataBase.SaveChanges(); 
        }
    }
}
