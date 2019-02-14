using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
using Farf_Project.Core;

namespace Farf_Project.Web
{
    public class LogExceptionManager : ILogExceptionManager
    {
        #region Private Properties

        private readonly ILogExceptionRepository logExceptionRepository;
        
        #endregion

        #region Constructor

        public LogExceptionManager(ILogExceptionRepository logExceptionRepository)
        {
            this.logExceptionRepository = logExceptionRepository;
        }
       
        #endregion

        #region Public Methods

        public async Task<Guid> StoreLogExceptionAsync(Exception exception)
        {
            var exceptionId = Guid.NewGuid();
            await this.logExceptionRepository.StoreLogExceptionAsync(exceptionId, exception.ToString());
            return exceptionId;
        }

        #endregion

    }
}
