using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Farf_Project.Core
{
    public interface ILogExceptionRepository
    {
        Task StoreLogExceptionAsync(Guid exceptionId, string exception);
    }
}
