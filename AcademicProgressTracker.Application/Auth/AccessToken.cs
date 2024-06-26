﻿using AcademicProgressTracker.Application.Common.Interfaces;
using AcademicProgressTracker.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AcademicProgressTracker.Application.Auth
{
    public class AccessToken : IToken
    {
        public string Token { get; set; }

        public AccessToken(User user, SymmetricSecurityKey secretKey, int lifeTimeInMinutes)
        {
            Token = Create(user, secretKey, lifeTimeInMinutes);
        }

        private string Create(User user, SymmetricSecurityKey secretKey, int lifeTimeInMinutes)
        {
            // в claims будет лежать вся информация о пользователе, которая нам нужна в токене
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
            };

            foreach(var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var creds = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(lifeTimeInMinutes),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
