using System.Text.RegularExpressions;

namespace AuthService.Application.Validators;

    public static class RegexPatterns
    {
        public static readonly Regex Username = new(@"^(?![._])[a-zA-Z0-9._]{4,24}$", RegexOptions.Compiled);
        public static readonly Regex Password = new(@"^[a-zA-Z0-9!@#$%^&*()_+\-=]{8,24}$", RegexOptions.Compiled);
        public static readonly Regex Email = new(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,24}$", RegexOptions.Compiled);
    }
    