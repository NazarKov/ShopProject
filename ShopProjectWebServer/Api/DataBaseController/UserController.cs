using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopProjectDataBase.DataBase.Model;
using ShopProjectSQLDataBase.Entities;
using ShopProjectWebServer.Api.Helpers;
using ShopProjectWebServer.DataBase;
using ShopProjectWebServer.Helpers;
using System.Text.Json;

namespace ShopProjectWebServer.Api.DataBaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(string token, UserEntity user)
        {
            try
            {
                var tokens = DataBaseMainController.DataBaseAccess.TokenTable.GetAll();

                if (tokens != null)
                {
                    var userToken = tokens.Where(t => t.Token == token).FirstOrDefault();
                    if (userToken != null)
                    {

                        var users = DataBaseMainController.DataBaseAccess.UserTable.GetAll();
                        if (users.Count() > 0)
                        {
                            var item = users.Where(u => u.Login == user.Login).FirstOrDefault();

                            if (item != null)
                            {
                                throw new Exception("Користувач існує");
                            }
                        }
                        DataBaseMainController.DataBaseAccess.UserTable.Add(user);


                        return Ok(new Message()
                        {
                            MessageBody = JsonSerializer.Serialize<bool>(true),
                            Type = TypeMessage.Message
                        }.ToString());
                    }
                }
                throw new Exception("Невірний токен авторизації");

            }
            catch (Exception ex)
            {
                return BadRequest(new Message()
                {
                    MessageBody = ex.ToString(),
                    Type = TypeMessage.Error,

                }.ToString());
            }
        }


        [HttpGet("Authorization")]
        public async Task<IActionResult> Authorization(string login, string password, string devise)
        {
            try
            {
                var users = DataBaseMainController.DataBaseAccess.UserTable.GetAll();

                if (users != null)
                {
                    var user = users.Where(u => u.Login == login).FirstOrDefault();
                    if (user != null)
                    {
                        if (user.Password == password)
                        {
                            var tokenbody = GenerationToken.Generate(16);

                            var token = new TokenEntity()
                            {
                                Device = devise,
                                Token = tokenbody,
                                User = user,
                                CreateAt = DateTime.Now,
                            };
                            DataBaseMainController.DataBaseAccess.TokenTable.Add(token);

                            return Ok(new Message()
                            {
                                MessageBody = JsonSerializer.Serialize<TokenEntity>(token),
                                Type = TypeMessage.Message
                            }.ToString());
                        }
                        else
                        {
                            throw new Exception("Невірний пароль");
                        }
                    }
                    else
                    {
                        throw new Exception("Користувача не знайдено");
                    }
                }
                else
                {
                    throw new Exception("Немає користувачів");
                }


            }
            catch (Exception ex)
            {
                return BadRequest(new Message()
                {
                    MessageBody = ex.ToString(),
                    Type = TypeMessage.Error,

                }.ToString());
            }
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers(string token)
        {
            try
            {
                var tokens = DataBaseMainController.DataBaseAccess.TokenTable.GetAll();

                if (tokens != null)
                {
                    var userToken = tokens.Where(t => t.Token == token).FirstOrDefault();
                    if (userToken != null)
                    {
                        var users = DataBaseMainController.DataBaseAccess.UserTable.GetAll();

                        return Ok(new Message()
                        {
                            MessageBody = JsonSerializer.Serialize(users),
                            Type = TypeMessage.Message
                        }.ToString());
                    }
                }
                throw new Exception("Невірний токен авторизації");

            }
            catch (Exception ex)
            {
                return BadRequest(new Message()
                {
                    MessageBody = ex.ToString(),
                    Type = TypeMessage.Error,

                }.ToString());
            }
        }

        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById(string token,string id)
        {
            try
            {
                var tokens = DataBaseMainController.DataBaseAccess.TokenTable.GetAll();

                if (tokens != null)
                {
                    var userToken = tokens.Where(t => t.Token == token).FirstOrDefault();
                    if (userToken != null)
                    {
                        var users = DataBaseMainController.DataBaseAccess.UserTable.GetAll();

                        if (users.Count() > 0)
                        {
                            var user = users.Where(U => U.ID == Guid.Parse(id)).First();
                            return Ok(new Message()

                            {
                                MessageBody = JsonSerializer.Serialize(user),
                                Type = TypeMessage.Message
                            }.ToString());
                        }
                        else
                        {
                            throw new Exception("Немає користувачів для виконнаня операції");
                        }
                    }
                }
                throw new Exception("Невірний токен авторизації");

            }
            catch (Exception ex)
            {
                return BadRequest(new Message()
                {
                    MessageBody = ex.ToString(),
                    Type = TypeMessage.Error,

                }.ToString());
            }
        }



        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser(string token)
        {
            try
            {
                var userTokens = DataBaseMainController.DataBaseAccess.TokenTable.GetAll();

                if (userTokens != null)
                {
                    var userToken = userTokens.Where(t => t.Token == token).FirstOrDefault();
                    if (userToken != null)
                    {
                        var users = DataBaseMainController.DataBaseAccess.UserTable.GetAll();
                        if (users != null)
                        {
                            var user = users.Where(u => u.ID == userToken.User.ID).FirstOrDefault();
                            
                            return Ok(new Message()
                            {
                                MessageBody = JsonSerializer.Serialize<UserEntity>(user),
                                Type = TypeMessage.Message
                            }.ToString());
                        }
                        else
                        {
                            throw new Exception("Користувача не знайдено");
                        }
                    }
                    else
                    {
                        throw new Exception("Користувача не знайдено");
                    }
                }
                else
                {
                    throw new Exception("Немає користувачів");
                }


            }
            catch (Exception ex)
            {
                return BadRequest(new Message()
                {
                    MessageBody = ex.ToString(),
                    Type = TypeMessage.Error,

                }.ToString());
            }
        }



    }
}
