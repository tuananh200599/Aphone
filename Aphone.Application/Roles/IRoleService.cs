using Aphone.ViewModel.Roles;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aphone.Application.Roles
{
    public interface IRoleService
    {
        Task<List<RoleVm>> GetAll();
    }
}
