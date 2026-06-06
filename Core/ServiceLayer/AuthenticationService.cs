using AutoMapper;
using DomainLayer.Exceptions;
using DomainLayer.Models.IdetityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SeviceAbstraction;
using Shared.DTOs.Email;
using Shared.DTOs.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ServiceImplementation
{
    public class AuthenticationService(UserManager<ApplicationUser> userManager,
        IConfiguration configuration, IMapper mapper, INotificationsService notificationsService) : IAuthenticationService
    {

        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email) ?? throw new UserNotFoundException(loginDto.Email);

            if (!await userManager.CheckPasswordAsync(user, loginDto.Password))
                throw new UnAuthorizedException("Invalid Credentials");

            if (!user.EmailConfirmed)
                throw new UnAuthorizedException("Please verify your email first.");
            return new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = loginDto.Email,
                UserName = user.UserName!,
                Token = await CreateTokentAsync(user)
            };
        }

        public async Task RegisterAsync(RegisterDto registerDto)
        {
            var user = new ApplicationUser()
            {
                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName,
                PhoneNumber = registerDto.Phone,
                UserName = string.IsNullOrWhiteSpace(registerDto.UserName) ? registerDto.Email : registerDto.UserName
            };

            var result = await userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();

                throw new BadRequestException(errors);
            }

            await SendEmailConfirmationAsync(user);
        }

        private async Task SendEmailConfirmationAsync(ApplicationUser user)
        {
            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedToken = Uri.EscapeDataString(token);

            var confirmationLink =
                $"{configuration["Urls:BaseUrl"]}api/auth/confirm-email" +
                $"?email={user.Email}" +
                $"&token={encodedToken}";

            var body = $@"
        <div style='font-family:Arial'>
            <h2>Welcome to Otlob</h2>

            <p>
                Please verify your email address
                by clicking the button below.
            </p>

            <a href='{confirmationLink}'
               style='background:#2563eb;
                      color:white;
                      padding:12px 20px;
                      text-decoration:none;
                      border-radius:6px'>
                Verify Email
            </a>

            <p>
                If you did not create this account,
                ignore this email.
            </p>
        </div>";

            await notificationsService.SendEmailAsync(new EmailRequestDto()
            {
                To = user.Email!,
                Subject = "Verify Your Email",
                Body = body
            });
        }

        public async Task ResendConfirmationEmailAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email) ?? throw new UserNotFoundException(email);

            if (user.EmailConfirmed) throw new BadRequestException(["Email Confirmed Already"]);

            await SendEmailConfirmationAsync(user);
        }

        public async Task<string> ConfirmEmailAsync(string email, string token)
        {
            var user = await userManager.FindByEmailAsync(email) ?? throw new UserNotFoundException(email);
            if (user.EmailConfirmed) return "Email already confirmed";

            var result = await userManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();

                throw new BadRequestException(errors);
            }
            return "Email confirmed successfully";
        }

        public async Task<bool> CheckEmailAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            return user is not null;
        }

        public async Task<UserDto> GetCurrentUserAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email) ?? throw new UserNotFoundException(email);
            return new UserDto() { Email = email, DisplayName = user.DisplayName, UserName = user.UserName!, Token = await CreateTokentAsync(user) };
        }

        public async Task<AddressDto> GetCurrentUserAddressAsync(string email)
        {
            var user = await userManager.Users.Include(a => a.Address)
                                                           .FirstOrDefaultAsync(u => u.Email == email)
                                                           ?? throw new UserNotFoundException(email);
            if (user.Address is not null)
                return mapper.Map<Address, AddressDto>(user.Address);

            throw new UserAddressNotFoundException(user.UserName!);
        }

        public async Task<AddressDto> UpdateCurrentUserAddressAsync(string email, AddressDto addressDto)
        {
            var user = await userManager.Users.Include(a => a.Address)
                                                           .FirstOrDefaultAsync(u => u.Email == email)
                                                           ?? throw new UserNotFoundException(email);
            if (user.Address is not null) //update 
            {
                user.Address.FirstName = addressDto.FirstName;
                user.Address.LastName = addressDto.LastName;
                user.Address.Country = addressDto.Country;
                user.Address.City = addressDto.City;
                user.Address.Street = addressDto.Street;
            }
            else //add new 
                user.Address = mapper.Map<AddressDto, Address>(addressDto);

            await userManager.UpdateAsync(user);

            return mapper.Map<AddressDto>(user.Address);
        }

        private async Task<string> CreateTokentAsync(ApplicationUser user)
        {
            var userClaims = new List<Claim>()
            {
                new(ClaimTypes.Email,user.Email!),
                new(ClaimTypes.Name,user.UserName!),
                new(ClaimTypes.NameIdentifier,user.Id)
            };

            var userRoles = await userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
                userClaims.Add(new Claim(ClaimTypes.Role, role));

            var secretKey = configuration.GetSection("JWTOptions")["Key"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var userCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration.GetSection("JWTOptions")["Issuer"],
                audience: configuration.GetSection("JWTOptions")["Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: userCredentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
