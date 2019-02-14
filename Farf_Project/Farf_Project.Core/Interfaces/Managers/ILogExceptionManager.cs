using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Farf_Project.Core
{
   public interface ILogExceptionManager
   {
        Task<Guid> StoreLogExceptionAsync(Exception exception);
   }
}
