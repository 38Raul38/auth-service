namespace AuthService.Application.Data.DTOs.Request;

public class LogoutRequestDTO
{
    
}



    /*register-request.dto.*
    login-request.dto.*
    auth-response.dto.*
    user.dto.*
    update-user.dto.*
    change-password.dto.*
    forgot-password-request.dto.*
    reset-password.dto.*
    refresh-token-request.dto.
    google-login-request.dto.* **/


    /*dto/
        request/
         LoginRequest.*
         RegisterRequest.*
         GoogleLoginRequest.*      
         RefreshTokenRequest.*
         ChangePasswordRequest.*
         ForgotPasswordRequest.*
         ResetPasswordRequest.*
         UpdateProfileRequest.*
        response/
         AuthResponse.*
         UserResponse.*
         ProfileResponse.*         
         MessageResponse.*          */




    /*JwtOptions.cs – настройки токена
    JwtPayloadModel.cs – что кладёшь в payload (userId, email, role и т.п.)
    IJwtTokenService.cs – интерфейс сервиса токенов
    JwtTokenService.cs – генерация и валидация JWT
    AuthResponse.cs – DTO ответа с токеном
    JwtAuthExtensions.cs – расширение для AddAuthentication (или просто код в Program.cs)

    Опционально для refresh‑токенов:

    RefreshToken.cs – сущность
    ITokenManager.cs
    TokenManager.cs*/

