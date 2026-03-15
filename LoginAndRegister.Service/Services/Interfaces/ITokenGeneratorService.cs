using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginAndRegister.Service.Services.Interfaces
{
    public interface ITokenGeneratorService
    {
        string GenerateJwtToken();
    }
}
