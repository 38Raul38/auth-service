namespace AuthService.Application.Data.DTOs.Response;

public record RefreshTokenResponse(
    string AccessToken,
    string RefreshToken,
    DateTime RefreshTokenExpiresAt
);
