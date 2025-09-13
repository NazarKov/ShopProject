using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Api.Helpers;
using ShopProjectWebServer.DataBase;
using ShopProjectWebServer.Helpers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Api.Controller.DataBaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("GetUserByNamePageColumn")]
        public IActionResult GetUserByNamePageColumn(string token, string name, int page, int countColumn, TypeStatusUser status)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true
                };

                if (AuthorizationApi.LoginToken(token))
                {
                    var users = DataBaseMainController.DataBaseAccess.UserTable.GetUsersByNamePageColumn(name, page, countColumn, status);

                    return Ok(new Message()
                    {
                        MessageBody = JsonSerializer.Serialize(users,options),
                        Type = TypeMessage.Message
                    }.ToString());
                }
                throw new Exception("Невдалося отримати товари");

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

        [HttpGet("GetUsersPageColumn")]
        public IActionResult GetUsersPageColumn(string token, int page, int countColumn, TypeStatusUser status)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true
                };
                if (AuthorizationApi.LoginToken(token))
                {
                    var users = DataBaseMainController.DataBaseAccess.UserTable.GetAllPageColumn(page, countColumn, status);

                    return Ok(new Message()
                    {
                        MessageBody = JsonSerializer.Serialize(users, options),
                        Type = TypeMessage.Message
                    }.ToString());
                }
                throw new Exception("Невдалося отримати товари");

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

        [HttpPost("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromQuery] string token, UserEntity user)
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                {

                    
                    DataBaseMainController.DataBaseAccess.UserTable.Delete(user);
                     
                    return Ok(new Message()
                    {
                        MessageBody = JsonSerializer.Serialize(true),
                        Type = TypeMessage.Message
                    }.ToString());
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


        [HttpPost("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromQuery] string token, UserEntity user)
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                { 
                   
                    DataBaseMainController.DataBaseAccess.UserTable.Update(user);


                    return Ok(new Message()
                    {
                        MessageBody = JsonSerializer.Serialize(true),
                        Type = TypeMessage.Message
                    }.ToString());
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


        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromQuery]string token, UserEntity user)
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                {  
                    DataBaseMainController.DataBaseAccess.UserTable.Add(user);


                    return Ok(new Message()
                    {
                        MessageBody = JsonSerializer.Serialize(true),
                        Type = TypeMessage.Message
                    }.ToString()); 
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

                            AuthorizationApi.AddToken(tokenbody);

                            return Ok(new Message()
                            {
                                MessageBody = JsonSerializer.Serialize(token),
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
                if (AuthorizationApi.LoginToken(token))
                { 
                    var users = DataBaseMainController.DataBaseAccess.UserTable.GetAll();

                        return Ok(new Message()
                        {
                            MessageBody = JsonSerializer.Serialize(users),
                            Type = TypeMessage.Message
                        }.ToString()); 
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
                if (AuthorizationApi.LoginToken(token))
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
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true
                };
                if (AuthorizationApi.LoginToken(token))
                {
                    var user = DataBaseMainController.DataBaseAccess.UserTable.GetUser(token);

                    return Ok(new Message()
                    {
                        MessageBody = JsonSerializer.Serialize(user,options),
                        Type = TypeMessage.Message
                    }.ToString());
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



    }
}
