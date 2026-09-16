using SIA_2026_Zapatero.Shared.Dmodel;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIA_2026_Zapatero.Shared.Services
{
public  interface IAuthService
    {
        Task<MethodResult> RegisterAsync(RegisterModel model);

        Task<MethodResult> LoginAsync(LoginModel model);

    }
}
