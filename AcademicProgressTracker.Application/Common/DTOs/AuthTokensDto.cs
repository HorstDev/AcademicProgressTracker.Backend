﻿using AcademicProgressTracker.Application.Auth;

namespace AcademicProgressTracker.Application.Common.DTOs
{
    public class AuthTokensDto
    {
        public AccessToken AccessToken { get; set; }
        public RefreshToken RefreshToken { get; set; }

        public AuthTokensDto(AccessToken accessToken, RefreshToken refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
