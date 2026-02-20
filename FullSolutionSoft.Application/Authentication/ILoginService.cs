using FullSolutionSoft.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace FullSolutionSoft.Application.Authentication;

public interface ILoginService
{
    Task<LoginResult?> LoginAsync(LoginRequest request);
}
