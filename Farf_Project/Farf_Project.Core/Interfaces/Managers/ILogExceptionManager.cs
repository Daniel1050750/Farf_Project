using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Farf_Project.Core
{
   public interface ILogExceptionManager
   {
        /// <summary>
        /// Store exception log into DB
        /// </summary>
        /// <param name="exception">The exception</param>
        /// <returns>Guid</returns>
        Task<Guid> StoreLogExceptionAsync(Exception exception);
   }
}
