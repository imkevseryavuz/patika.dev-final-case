using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SiteManagamentPanel.Base;
using SiteManagamentPanel.Base.Jwt;
using SiteManagamentPanel.Base.LogType;
using SiteManagementPanel.Busines;
using SiteManagementPanel.Data.Domain;
using SiteManagementPanel.Data.Uow;
using SiteManagementPanel.Schema;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SiteManagementPanel.Business;

public class LoginService : ILoginService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserLogService _userLogService;
    private readonly JwtConfig _jwtConfig;
    public LoginService(IUnitOfWork unitOfWork, IUserLogService userLogService, IOptionsMonitor<JwtConfig> jwtConfig)
    {
        _unitOfWork = unitOfWork;
        _userLogService = userLogService;
        _jwtConfig = jwtConfig.CurrentValue;
    }

    public ApiResponse<LoginResponse> Login(LoginRequest request)
    {
        if (request is null)
        {
            return new ApiResponse<LoginResponse>("Request was null");
        }

        if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
        {
            return new ApiResponse<LoginResponse>("Request was null");
        }

        request.UserName = request.UserName.Trim().ToLower();
        request.Password = request.Password.Trim();

        var user = _unitOfWork.UserRepository.Where(x => x.UserName.Equals(request.UserName)).FirstOrDefault();

        if (user is null)
        {
            Log(request.UserName, LogType.InValidUserName);
            return new ApiResponse<LoginResponse>("Invalid user informations");
        }

        if (user.Password != request.Password)
        {
            Log(request.UserName, LogType.InValidUserName);
            return new ApiResponse<LoginResponse>("Invalid user informations");
        }

        if (user.Status != 1)
        {
            Log(request.UserName, LogType.InValidUserStatus);
            return new ApiResponse<LoginResponse>("Invalid user status");
        }

        if (user.PasswordRetryCount > 3)
        {
            Log(request.UserName, LogType.PasswordRetryCountExceded);
            return new ApiResponse<LoginResponse>("Password retry count exceded");
        }

        user.LastActivity = DateTime.UtcNow;
        user.Status = 1;

        _unitOfWork.UserRepository.Update(user);
        _unitOfWork.Complete();

        string token = Token(user);

        Log(request.UserName, LogType.LogedIn);

        LoginResponse response = new()
        {
            AccessToken = token,
            ExpireTime = DateTime.Now.AddMinutes(_jwtConfig.AccessTokenExpiration),
            UserName = user.UserName
        };

        return new ApiResponse<LoginResponse>(response);
    }

    private string Token(User user)
    {
        Claim[] claims = GetClaims(user);
        var secret = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

        var jwtToken = new JwtSecurityToken(
            _jwtConfig.Issuer,
            _jwtConfig.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(_jwtConfig.AccessTokenExpiration),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            );

        var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        return accessToken;
    }


    private Claim[] GetClaims(User user)
    {
        var claims = new[]
        {
            new Claim("UserName",user.UserName),
            new Claim("UserId",user.Id.ToString()),
            new Claim("RoleType",user.Role.ToString()),
            new Claim("Status",user.Status.ToString()),
            new Claim(ClaimTypes.Role,user.Role.ToString()),
            new Claim(ClaimTypes.Name,$"{user.FirstName} {user.LastName}")
        };

        return claims;
    }

    private void Log(string username, string logType)
    {
        UserLogRequest request = new()
        {
            LogType = logType,
            UserName = username,
            TransactionDate = DateTime.UtcNow
        };
        _userLogService.Insert(request);
    }

    
}

