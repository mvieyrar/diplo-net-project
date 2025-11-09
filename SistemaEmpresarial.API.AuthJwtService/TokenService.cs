using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SistemaEmpresarial.API.AuthJwtService
{
    public class TokenService
    {
        private readonly string _llaveJwt;
        public TokenService(IConfiguration configuration)
        {
            _llaveJwt = configuration["LlaveJwt"];
        }
        public string ObtenerToken(string nombre, string role, string clienteId, string correo, DateTime fechaDeExpiracion)
        {
            // Lógica para generar y devolver un token JWT
            //return "token_generado";
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_llaveJwt));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            //var expirationTimeStamp = new DateTime.Now.AddMinutes(20);
            
            var claims = new List<Claim>
            {
                new Claim("Nombre", nombre),
                new Claim("Role", role),
                new Claim("ClienteId", clienteId),
                new Claim("Email", correo)
            };
            
            var tokenOptions = new JwtSecurityToken(
                //issuer: "https://localhost:5002",
                claims: claims,
                expires: fechaDeExpiracion,
                signingCredentials: signingCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return tokenString;
        }
    }
}
