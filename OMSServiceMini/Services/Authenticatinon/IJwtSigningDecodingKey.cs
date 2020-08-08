using Microsoft.IdentityModel.Tokens;

namespace OMSServiceMini.Services.Authenticatinon
{
    public interface IJwtSigningDecodingKey
    {
        SecurityKey GetKey();
    }
}