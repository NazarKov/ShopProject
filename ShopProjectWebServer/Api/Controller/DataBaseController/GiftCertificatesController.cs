using Microsoft.AspNetCore.Mvc;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.Discount;
using ShopProjectWebServer.Api.DtoModels.GiftCertificate;
using ShopProjectWebServer.Api.Interface.Services;

namespace ShopProjectWebServer.Api.Controller.DataBaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftCertificatesController : ControllerBase
    {
        private IGiftCertificatesServise _servise;
        public GiftCertificatesController(IGiftCertificatesServise servise)
        {
            _servise = servise;
        }


        [HttpPost("UpdateParameterGiftCertificates")]
        public async Task<IActionResult> UpdateParameterGiftCertificates(string token, [FromQuery] string parameter, [FromQuery] string value, UpdateGiftCertificateDto item)
        {
            try
            {
                _servise.UpdateParameterGiftCertificate(token, parameter, value, item);
                return Ok(ApiResponse<bool>.Ok(true));

            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }

        [HttpPost("AddGiftCertificate")]
        public IActionResult AddGiftCertificate([FromQuery] string token, CreateGiftCertificateDto item)
        {
            try
            {
                _servise.Add(token, item);
                return Ok(ApiResponse<bool>.Ok(true, "Обєкт збережено"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }

        [HttpPost("UpdateGiftCertificate")]
        public IActionResult UpdateGiftCertificate([FromQuery] string token, UpdateGiftCertificateDto item)
        {
            try
            {
                _servise.Update(token, item);
                return Ok(ApiResponse<bool>.Ok(true, "Обєкт збережено"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }

        [HttpGet("GetGiftCertificateByBarCode")]
        public IActionResult GetGiftCertificateByBarCode(string token, string barCode, TypeStatusGiftCertificate status = TypeStatusGiftCertificate.Unknown)
        {
            try
            {
                var result = _servise.GetGiftCertificateByBarCode(token, barCode, status);
                return Ok(ApiResponse<GiftCertificateDto>.Ok(result));
            }
            catch (InvalidOperationException invalidOperationException)
            {
                if (invalidOperationException.Message == "Sequence contains no elements")
                {
                    return Ok(ApiResponse<GiftCertificateDto>.Ok(new GiftCertificateDto()));
                }
                else
                {
                    return BadRequest(ApiResponse<string>.Fail(invalidOperationException.Message));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }

        [HttpGet("GetGiftCertificatesByNamePageColumn")]
        public IActionResult GetGiftCertificatesByNamePageColumn(string token, string name, int page, int countColumn, TypeStatusGiftCertificate status)
        {
            try
            {
                var result = _servise.GetGiftCertificatesByNamePageColumn(token, name, page, countColumn, status);
                return Ok(ApiResponse<PaginatorDto<GiftCertificateDto>>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }

        [HttpGet("GetGiftCertificatesPageColumn")]
        public IActionResult GetGiftCertificatesPageColumn(string token, int page, int countColumn, TypeStatusGiftCertificate status)
        {
            try
            {
                var result = _servise.GetGiftCertificatesPageColumn(token, page, countColumn, status);
                return Ok(ApiResponse<PaginatorDto<GiftCertificateDto>>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }
    }
}
