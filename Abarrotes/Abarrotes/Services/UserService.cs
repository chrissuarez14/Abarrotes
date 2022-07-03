using Abarrotes.Models;
using Abarrotes.Models.Common;
using Abarrotes.Models.Request;
using Abarrotes.Models.Response;
using Abarrotes.Tools;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Abarrotes.Services
{
    public class UserService : IUserService
    {

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings )
        {
            _appSettings = appSettings.Value;
        }
        public UserResponse Auth(AuthRequest oModel)
        {
            UserResponse userResponse = new UserResponse();
            
            using (var db =  new AbarrotesContext())
            {

                string sPassword = Encrypt.GetSHA256(oModel.Password);//El GetSHA256 es el nombre del metodo para encriptar


                var usuario = db.Usuarios.Where(d => d.Email == oModel.Email &&
                                                 d.Password == sPassword).FirstOrDefault();//este metodo regresa el elemento o nulo 

                if (usuario == null) return null; //si es null reresa un null 

                userResponse.email = usuario.Email; // si no es null regresa el dato 
                userResponse.token = GetToken(usuario);
            }

            return userResponse;

        }


        private string GetToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var llave = Encoding.ASCII.GetBytes(_appSettings.Secreto);


            //Aqui vamos a configurar la informcion que queremos que tenga nuestro jwt

            var tokenDescriptor = new SecurityTokenDescriptor

            {
                Subject = new ClaimsIdentity(

                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier,usuario.Id.ToString()),
                        new Claim(ClaimTypes.NameIdentifier,usuario.Email)
                    }
                    ),
                     Expires = DateTime.UtcNow.AddDays(60),//le indicamos que venza en 60 dias
                     SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(llave),SecurityAlgorithms.HmacSha256Signature)


            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);//regresa string 
        }
    }
}
