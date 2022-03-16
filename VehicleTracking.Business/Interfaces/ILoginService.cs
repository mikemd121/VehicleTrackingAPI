using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Business.Interfaces
{
    public interface ILoginService
    {
        bool Login(string userName, string Password);
    }
}
