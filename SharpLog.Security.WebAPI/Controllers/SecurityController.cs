﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharpLog.Framework.WebAPI.Controllers;
using SharpLog.Security.Core.Models;
using SharpLog.Security.Core.Requests;
using SharpLog.Security.WebAPI.Models;
using System.Threading.Tasks;

namespace SharpLog.Security.WebAPI.Controllers
{
    [ApiController]
    [Route("api/")]
    [Authorize]
    public class SecurityController : BaseApiController
    {
        [HttpGet]
        [Route("security/access-token")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAccessTokenAsync([FromQuery] GetAccessCodeViewModel getAccessCodeViewModel)
        {
            var result = await RequestLoader
                .LoadRequest<GetAccessCodeViewModel, GetAccessTokenHandler, FetchAccessTokens, AccessTokens>(getAccessCodeViewModel)
                .WithDataMappedTo<FetchAccessTokens>()
                .WithResponseMappedTo<AccessTokensViewModel>()
                .InvokeAsync();

            return Ok(result);
        }

        [HttpGet]
        [Route("login/")]
        public async Task<IActionResult> Login()
        {
            var result = await RequestLoader
                .LoadPing<GetLoggedInUserHandler, UserIdentity>()
                .WithResponseMappedTo<UserIdentityViewModel>()
                .InvokeAsync();

            return Ok(result);
        }
    }
}