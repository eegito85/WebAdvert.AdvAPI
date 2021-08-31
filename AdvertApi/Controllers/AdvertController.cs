using AdvertApi.Models;
using AdvertApi.Models.Models;
using AdvertApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertApi.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class AdvertController : Controller
    {
        private readonly IAdvertStorageService _advertStorageService;

        public AdvertController(IAdvertStorageService advertStorageService)
        {
            _advertStorageService = advertStorageService;
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(400)]
        [ProducesResponseType(201, Type=typeof(CreateAdvertResponse))]
        public async Task<IActionResult> Create(AdvertModel advertModel)
        {
            string recordId;
            try
            {
                recordId = await _advertStorageService.Add(advertModel);
            }
            catch(KeyNotFoundException)
            {
                return new NotFoundResult();
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }

            return StatusCode(201, new CreateAdvertResponse { Id = recordId });
        }

        [HttpPut]
        [Route("Confirm")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Confirm(ConfirmAdvertModel confirmAdvertModel)
        {
            try
            {
                await _advertStorageService.Confirm(confirmAdvertModel);

            }
            catch(KeyNotFoundException)
            {
                return new NotFoundResult();
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
            return new OkResult();
        }

    }
}
