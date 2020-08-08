using Microsoft.IdentityModel.Tokens;

namespace OMSServiceMini.Services.Authenticatinon
{
    public interface IJwtSigningEncodingKey
    {
        string SigningAlgorithm { get; }
        SecurityKey GetKey();
    }
}