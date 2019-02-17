using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Farf_Project.Core
{
    public interface ILogExceptionRepository
    {
        /// <summary>
        /// Store log exception
        /// </summary>
        /// <param name="exceptionId"></param>
        /// <param name="exception"></param>
        Task StoreLogExceptionAsync(Guid exceptionId, string exception);
    }
}
