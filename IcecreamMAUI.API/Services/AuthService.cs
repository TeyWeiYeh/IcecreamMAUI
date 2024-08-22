using IcecreamMAUI.API.Data;
using IcecreamMAUI.API.Data.Entities;
using IcecreamMAUI.Shared.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Platform;

namespace IcecreamMAUI.API.Services
{
    public class AuthService(DataContext context, TokenService tokenService, PasswordService passwordService)
    {
        private readonly DataContext _context = context;
        private readonly TokenService _tokenService = tokenService;
        private readonly PasswordService _passwordService = passwordService;

        public async Task<ResultWithDataDto<AuthResponseDto>> SignupAsync(SignupRequestDto dto)
        {
            //checks if the signup user email alr exist in our db
            if(await _context.Users.AsNoTracking().AnyAsync(u => u.Email == dto.Email))
            {
                return ResultWithDataDto<AuthResponseDto>.Failure("Email already exist");
            }

            //set the user details
            var user = new User
            {
                Email = dto.Email,
                Address = dto.Address,
                Name = dto.Name,
            };

            //converts the password entered into hash 
            (user.Salt, user.Hash) = _passwordService.GenerateSaltAndHash(dto.Password);

            try
            {
                //store the user in our db
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                //returns success 
                return GenerateAuthResponse(user);
            }
            catch (Exception ex)
            {
                //or failure 
                return ResultWithDataDto<AuthResponseDto>.Failure(ex.Message);
            } 
        }

        public async Task<ResultWithDataDto<AuthResponseDto>> SigninAsync(SigninRequestDto dto)
        {
            //checks if user exist
            var dbUser = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            //doesn't exist
            if (dbUser is null)
                return ResultWithDataDto<AuthResponseDto>.Failure("User not found");
            //when comparing the converted to hash password with the one store in the database, check if they're equal
            if (!_passwordService.AreEqual(dto.Password, dbUser.Salt, dbUser.Hash))
                return ResultWithDataDto<AuthResponseDto>.Failure("Incorrect Password");

            return GenerateAuthResponse(dbUser);

        }

        //method to generate token for our successful logged in user
        private ResultWithDataDto<AuthResponseDto>  GenerateAuthResponse(User user)
        {
            var loggedInUser = new LoggedInUser(user.Id, user.Name, user.Email, user.Address);
            var token = _tokenService.GenerateJwt(loggedInUser);

            var authResponse = new AuthResponseDto(loggedInUser, token);
            return ResultWithDataDto<AuthResponseDto>.Success(authResponse);
        }
    }
}
