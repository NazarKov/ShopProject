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
            var user = _contextDataBase.Users.Find(item.ID);

            if (user == null) return; 
            _contextDataBase.Users.Remove(user);
            _contextDataBase.SaveChanges();
        }

        public IEnumerable<UserEntity> GetAll()
        { 
            return _contextDataBase.Users.Include(r=>r.UserRole).Include(t=>t.Tokens).AsNoTracking();
        }

        public UserEntity GetById(Guid id)
        {
            return _contextDataBase.Users.FirstOrDefault(t => t.ID == id);
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
            var userToken = _contextDataBase.UserTokens.Include(u=>u.User).Include(r=>r.User.UserRole).FirstOrDefault(t => t.Token == token);

            if (userToken != null)
            {
                return _contextDataBase.Users.FirstOrDefault(u => u.ID == userToken.User.ID);
            }
            return null;
        } 
        public void Update(UserEntity item)
        { 
            UserEntity user;
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
                _contextDataBase.SaveChanges();
            }
            else
            {
                throw new Exception("Користувача не знайдено");
            } 
        }

        public void UpdateParameter(Guid id, string nameParameter, object valueParameter)
        { 
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
