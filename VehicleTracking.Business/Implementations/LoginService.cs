using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Business.Interfaces;
using VehicleTrackingAPI.Models;

namespace VehicleTracking.Business.Implementations
{
    public class LoginService : ILoginService
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly VehicleTrackContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginService"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public LoginService(VehicleTrackContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Logins the specified user name.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public bool Login(string userName, string password)
        {
            var isExist = _context.Users
               .Any(b => b.UserName == userName.Trim() && b.Password == password.Trim());

            return isExist;
        }
    }
}
