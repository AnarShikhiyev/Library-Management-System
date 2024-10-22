using Microsoft.EntityFrameworkCore;
using ProjectLibrary_Back.DTO;
using ProjectLibrary_Back.Interfaces;
using ProjectLibrary_Back.Models;

namespace ProjectLibrary_Back.Services
{
    public class UserAuthService : IUserAuthService
    {
        private readonly LibraryProjectContext _context;

        public UserAuthService(LibraryProjectContext context)
        {
            _context = context;
        }
        public async Task<string?> LoginAsync(LoginDto dto)
        {
            var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == dto.Username && u.Password == dto.Password);

            if (user == null || !user.Active)
            {
                return null; 
            }
            var userlogin = new UserLogin
            {
                UserId = user.Id,
                UserLoginDate = DateTime.Now
            };

            await _context.UserLogins.AddAsync(userlogin);
            await _context.SaveChangesAsync();


            return user.Id.ToString();
        }

        public async Task<bool> LogoutAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return false; 
            }
            user.Active = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RegisterUserAsync(RegisterUserDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Username == dto.Username))
            {
                return false;
            }

            var user = new User
            {
                Firstname = dto.Firstname,
                Lastname = dto.Lastname,
                Username = dto.Username,
                Password = dto.Password, 
                RoleId = dto.RoleId,
                Active = true
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
