using Microsoft.AspNetCore.Mvc;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.ObjectOwner;
using ShopProjectWebServer.Api.DtoModels.SignatureKey;
using ShopProjectWebServer.Api.Interface.Services;

namespace ShopProjectWebServer.Api.Controller.DataBaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElectronicSignatureKeyController : ControllerBase
    {
        private readonly IElectronicSignatureKeyServise _servise;
        public ElectronicSignatureKeyController(IElectronicSignatureKeyServise signatureKeyServise)
        {
            _servise = signatureKeyServise;
        }

        [HttpGet("GetElectronicSignatureKey")]
        public IActionResult GetObjectsOwnersByNamePageColumn(string token)
        {
            try
            {
                var result = _servise.GetSignatureKeyByUser(token);
                return Ok(ApiResponse<SignatureKeyDto>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }
    }
}
