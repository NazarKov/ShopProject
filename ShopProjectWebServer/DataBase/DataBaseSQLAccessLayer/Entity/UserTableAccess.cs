using Microsoft.EntityFrameworkCore;
using ShopProjectDataBase.Context;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper; 
using ShopProjectWebServer.DataBase.Interface.EntityInterface;
using System.Threading.Tasks;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity
{
    public class UserTableAccess : IUserTableAccess
    {
        private readonly ContextDataBase _contextDataBase;
        public UserTableAccess(ContextDataBase contextDataBase)
        {
            _contextDataBase = contextDataBase;
        }
        public async Task<UserEntity> AddAsync(UserEntity item)
        {
            if (item.UserRole != null)
            {
                item.UserRole = _contextDataBase.UserRoles.Find(item.UserRole.ID);
            }
            if (item.SignatureKey != null)
            {
                _contextDataBase.ElectronicSignatureKeys.Add(item.SignatureKey);
            }
            await _contextDataBase.Users.AddAsync(item);
            await _contextDataBase.SaveChangesAsync();
            return item;
        }

        public async Task UpdateAsync(UserEntity item)
        {
            var user = _contextDataBase.Users.Find(item.ID);
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
                    await _contextDataBase.ElectronicSignatureKeys.AddAsync(item.SignatureKey);
                    user.SignatureKey = item.SignatureKey;
                }
                else if(item.SignatureKey == null)
                {
                    user.SignatureKey = null;
                }

                if (_contextDataBase.UserRoles != null)
                {
                    var role = _contextDataBase.UserRoles.FirstOrDefault(r => r.NameRole == item.UserRole.NameRole);
                    if (role != null)
                    {
                        user.UserRole = role;
                    }
                }
                await _contextDataBase.SaveChangesAsync();
            }  
        }

        public async Task UpdateParameterAsync(Guid id, string nameParameter, object valueParameter)
        {
            var user = _contextDataBase.Users.Find(id);
            if (user != null)
            {
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
            await _contextDataBase.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = _contextDataBase.Users.Find(id);

            if (user == null) return;
            _contextDataBase.Users.Remove(user);
            await _contextDataBase.SaveChangesAsync();
        }



        public UserEntity? GetByLogin(string login)
        {
            IQueryable<UserEntity> query = _contextDataBase.Users.Include(u => u.UserRole).Include(s => s.SignatureKey).AsNoTracking();

            var user = query.FirstOrDefault(item => item.Login == login);

            if (user != null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }
         
        public IEnumerable<UserEntity> GetAll()
        { 
            return _contextDataBase.Users.Include(r=>r.UserRole).Include(t=>t.Tokens).AsNoTracking();
        }

        public UserEntity? GetById(Guid id)
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
                var items = query.Where(o => o.FullName.Contains(name));
                if (items.Any()) 
                {
                    query = items;
                }
                else
                {
                    query = query.Where(o => o.Login.Contains(name));
                }
            }
             

            var result = query.ToList();
            return result;
        }

        public UserEntity? GetUser(string token)
        {
            var userToken = _contextDataBase.UserTokens.Include(u=>u.User).Include(r=>r.User.UserRole).FirstOrDefault(t => t.Token == token);

            if (userToken != null)
            {
                return _contextDataBase.Users.Include(u=>u.SignatureKey).FirstOrDefault(u => u.ID == userToken.User.ID);
            }
            return null;
        }  

        public async Task<bool> ExistsByLogin(string login)
        {
            return await _contextDataBase.Users.AnyAsync(p => p.Login == login);
        }
    }
}
