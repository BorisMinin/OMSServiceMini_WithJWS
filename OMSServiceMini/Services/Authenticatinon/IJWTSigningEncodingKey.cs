﻿using Microsoft.IdentityModel.Tokens;

namespace OMSServiceMini.Services.Authenticatinon
{
    public interface IJWTSigningEncodingKey
    {
        string SigningAlgorithm { get; }
        SecurityKey GetKey();
    }
}