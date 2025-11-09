using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaEmpresarial.API.AuthJwtService.Dtos;
using SistemaEmpresarial.API.AuthJwtService.Entities;

namespace SistemaEmpresarial.API.AuthJwtService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InicioDeSesionesController : ControllerBase
    {
        private readonly TokenService _tokenService;

        public InicioDeSesionesController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(InicioDeSesionDto inicioDeSesionDto)
        {
            //Esto debería estar en la capa de reglas de negocio.
            // Implementar la lógica de inicio de sesión aquí
            //return Ok("Inicio de sesión exitoso");
            UsuarioEntidad usuario = await ObtenerAsync(inicioDeSesionDto.Correo);
            string token;

            if(usuario == null)
            {
                //return NotFound("Usuario no encontrado");
                token = string.Empty;
            }
            if (EsValido(usuario.Contrasena, inicioDeSesionDto.Contrasena))
            {
                //return Ok("Inicio de sesión exitoso");
                DateTime fechaExpiracion = DateTime.Now.AddMinutes(20);
                token = _tokenService.ObtenerToken(usuario.Nombre, usuario.Role, "1", usuario.Correo, fechaExpiracion);
            }
            else
            {
                //return Unauthorized("Credenciales inválidas");
                token = string.Empty;
            }
            // fin de la capa de reglas de negocio
            if (token == null)
            {
                return NotFound("Credenciales no validas");
            }
            else
            {
                return Ok(new { Token = token });
            }


        }

        private bool EsValido(string contrasenia1, string contrasena2)
        {
            if(contrasenia1 == contrasena2)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Aqui debería ir la llamada a la capa de repositorio para obtener el usuario por correo
        /// </summary>
        /// <param name="correo"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private async Task<UsuarioEntidad> ObtenerAsync(string correo)
        {
            return new UsuarioEntidad
            {
                Contrasena = "123456",
                Correo = "juan@hernandez",
                Nombre = "Juan Hernandez",
                Role = "Cliente"
            };
        }
    }
}
