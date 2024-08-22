using IcecreamMAUI.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IcecreamMAUI.Services
{
    public class AuthService
    {
        private const string AuthKey = "AuthKey";
        public LoggedInUser? User { get; private set; }
        public string? Token { get; private set; }

        //if user has logged in before, keep the status as logged in
        public void Signin(AuthResponseDto dto)
        {
            var serialised = JsonSerializer.Serialize(dto);
            Preferences.Default.Set(AuthKey, serialised);
            
            (User, Token) = dto;
        }

        //get the login status when opening the app (keep user's signed in state)
        public void Initialize()
        {
            if (Preferences.Default.ContainsKey(AuthKey))
            {
                var serialized = Preferences.Default.Get<string?> (AuthKey, null);
                if(string.IsNullOrWhiteSpace(serialized))
                {
                    Preferences.Default.Remove(AuthKey);
                }
                else
                {
                    (User, Token) = JsonSerializer.Deserialize<AuthResponseDto>(serialized)!;
                }
            }
        }

        public void Signout()
        {
            Preferences.Default.Remove(AuthKey);
            (User, Token) = (null, null);
        }
    }
}
