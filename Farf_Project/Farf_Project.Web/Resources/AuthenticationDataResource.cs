using System;

namespace Farf_Project.Core
{
    public class AuthenticationDataResource : EntityBase
    {
        /// <summary>
        /// Username
        /// </summary>
        public string Username { get; set; }
        
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
    }
}