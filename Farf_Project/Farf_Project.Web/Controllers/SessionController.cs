using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Farf_Project.Core;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Farf_Project.Web
{
    public class SessionController : Controller
    {

        #region Private Readonly Session
        
        private readonly IUsersService usersService;
        private readonly ITokenManager tokenManager;

        #endregion Private Readonly Session

        #region Constructor

        public SessionController(IUsersService sessionService, ITokenManager tokenManager)
        {
            this.usersService = sessionService;
            this.tokenManager = tokenManager;
        }

        #endregion Constructor

        #region Public Methods

        [AllowAnonymous]
        [HttpPost("api/session/login")]
        public async Task<IActionResult> AuthenticationLogin([FromBody] AuthenticationDataResource authenticationMetadataResource)
        {
            // verify if the credentials are valid
            try
            {
                await this.usersService.ValidateCredentialsAsync(authenticationMetadataResource.Username, authenticationMetadataResource.Password);
            }
            catch (UserException e) when (e.Type == UserExceptionType.UserNotFound)
            {
                throw new UnauthorizedException("User not found");
            }
            catch (UserException e) when (e.Type == UserExceptionType.InvalidPassword)
            {
                throw new UnauthorizedException("Invalid password");
            }
            catch (UserException e) when (e.Type == UserExceptionType.UserNotActive)
            {
                throw new UnauthorizedException("User not active");
            }

            // get the user data
            var user = await this.usersService.GetUserByUsernameAsync(authenticationMetadataResource.Username);

            var claims = new List<Claim>();

            // add user name claim
            claims.Add(new Claim(ClaimTypes.Name, authenticationMetadataResource.Username));

            // add scope claim
            claims.Add(new Claim("scope", user.Role.ToString()));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("hdhgsdfghseifhgsldfhgksdfogsdf523452345dsfgsdfg"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokenExpirationDate = DateTime.Now.AddDays(1);

            // create the token for the user
            var token = new JwtSecurityToken(
                issuer: "localhost",
                audience: "localhost",
                claims: claims,
                expires: tokenExpirationDate,
                signingCredentials: creds);


            // create the token string
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenString = jwtSecurityTokenHandler.WriteToken(token);

            // store token
            await this.tokenManager.StoreTokenAsync(tokenString, tokenExpirationDate, user);

            // returns the token and the user data
            return this.Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
            });
        }

        [AllowAnonymous]
        [HttpPost("api/session/logout")]
        public async Task<IActionResult> AuthenticationLogoutAsync()
        {
            await this.tokenManager.InvalidateCurrentTokenAsync();

            return this.Ok();
        }

        #endregion Public Methods
    }
}
