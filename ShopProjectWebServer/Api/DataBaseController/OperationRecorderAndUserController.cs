﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopProjectDataBase.DataBase.Entities;
using ShopProjectWebServer.Api.Helpers;
using ShopProjectWebServer.DataBase;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Api.DataBaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationRecorderAndUserController : ControllerBase
    {
        [HttpPost("AddOperationRecordersAndUser")]
        public async Task<IActionResult> AddOperationRecordersAndUser(string token, IEnumerable<OperationsRecorderUserEntity> operationsrecorderuserentity)
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                {

                    DataBaseMainController.DataBaseAccess.OperationRecorederUserTable.AddRange(operationsrecorderuserentity);

                    return Ok(new Message()
                    {
                        MessageBody = JsonSerializer.Serialize<bool>(true),
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

        [HttpGet("GetOperationRecordersAndUser")]
        public async Task<IActionResult> GetOperationRecordersAndUser(string token)
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                {

                    var options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        WriteIndented = true
                    };


                    var operationRecordersUsers = DataBaseMainController.DataBaseAccess.OperationRecorederUserTable.GetAll();

                    return Ok(new Message()
                    {
                        MessageBody = JsonSerializer.Serialize<IEnumerable<OperationsRecorderUserEntity>>(operationRecordersUsers,options),
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
