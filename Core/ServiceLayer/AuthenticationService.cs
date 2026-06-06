using AutoMapper;
using DomainLayer.Exceptions;
using DomainLayer.Models.IdetityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SeviceAbstraction;
using Shared.DTOs.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ServiceImplementation
{
    public class AuthenticationService(UserManager<ApplicationUser> userManager, IConfiguration configuration, IMapper mapper) : IAuthenticationService
    {

        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email) ?? throw new UserNotFoundException(loginDto.Email);

            if (!await userManager.CheckPasswordAsync(user, loginDto.Password))
                throw new UnAuthorizedException();

            return new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = loginDto.Email,
                Token = await CreateTokentAsync(user)
            };
        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = new ApplicationUser()
            {
                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName,
                PhoneNumber = registerDto.Phone,
                UserName = registerDto.UserName
            };

            var result = await userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();

                throw new BadRequestException(errors);
            }
            return new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await CreateTokentAsync(user)
            };

        }

        public async Task<bool> CheckEmailAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            return user is not null;
        }

        public async Task<UserDto> GetCurrentUserAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email) ?? throw new UserNotFoundException(email);
            return new UserDto() { Email = email, DisplayName = user.DisplayName, Token = await CreateTokentAsync(user) };
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
