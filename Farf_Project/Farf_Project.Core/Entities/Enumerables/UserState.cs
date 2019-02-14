using System;
using System.Collections.Generic;
using System.Text;

namespace Farf_Project.Core
{
    public enum UserState
    {
        Active = 1,
        Inactive
    }

    public enum UserRolePermission
    {
        NoAccess = 0,
        Operator,
        Administrator
    }
}
