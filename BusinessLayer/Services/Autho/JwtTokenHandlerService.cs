﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessLayer.Autho
{
	public class JwtTokenHandlerService : BaseTokenService, IJwtTokenHandlerService
	{

        public JwtTokenHandlerService(IConfiguration config) : base(config) { }
    
        /// <summary>
        /// Generate jwt token
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userRole"></param>
        /// <returns></returns>
        public string GenerateJwtToken(string userId, string userRole)
		{
		
			var claims = GenerateClaimsForJWT(userId, userRole);
			var codedKey = GenerateKey();

			var token = new JwtSecurityToken(
							issuer: config["Jwt:Issuer"],
							audience: config["Jwt:Audience"],
							claims: claims,
							notBefore: DateTime.UtcNow,
							expires: DateTime.UtcNow.AddHours(1),
							signingCredentials: codedKey);


			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		/// <summary>
		/// Validate jwt token 
		/// </summary>
		/// <param name="token">jwt token</param>
		/// <returns></returns>
		public ClaimsPrincipal? ValidateJwtToken(string token)
		{
			ClaimsPrincipal? jwtValidResult = null;
			try
			{
				var validationParameters = GetTokenValidationParameters();

				jwtValidResult = new JwtSecurityTokenHandler()
					.ValidateToken(token, validationParameters, out _);

			}
			catch (SecurityTokenException ex)
			{
				Console.WriteLine($"Token validation failed: {ex.Message}");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Problem not cooming from jwt validation process. Exception message : {ex.Message}");
			}

			return jwtValidResult;
		}

		public TokenValidationParameters GetTokenValidationParameters()
		{
			var key = Encoding.UTF8.GetBytes(config["Jwt:Key"]);
			return new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(key),
				ValidateIssuer = true,
				ValidIssuer = config["Jwt:Issuer"],
				ValidateAudience = true,
				ValidAudience = config["Jwt:Audience"],
				ValidateLifetime = true,
				ClockSkew = TimeSpan.Zero
			};
		}


		/// <summary>
		/// Jwt generats with UserId and user role 
		/// </summary>
		/// <returns></returns>
		private IEnumerable<Claim> GenerateClaimsForJWT(string userId, string userRole)
		{
			return new List<Claim>() {
				  new Claim(JwtRegisteredClaimNames.Sub,userId),
				  new Claim(ClaimTypes.Role, userRole)
			};
		}

        public int GetUserIdFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(token, GetTokenValidationParameters(), out SecurityToken validatedToken);

                var userId = Convert.ToInt32(principal.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub ||
                                                                                  c.Type == ClaimTypes.NameIdentifier).Value);
                return userId;
            }
            catch (Exception)
            {
                return 0; // Return null if token is invalid or expired
            }
        }
    }
}
