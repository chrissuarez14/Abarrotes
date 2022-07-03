using Abarrotes.Models.Request;
using Abarrotes.Models.Response;

namespace Abarrotes.Services
{
    public interface IUserService
    {

        UserResponse Auth(AuthRequest oModel); //Aqui se recibe el request que recibimos en el control



    }
}
