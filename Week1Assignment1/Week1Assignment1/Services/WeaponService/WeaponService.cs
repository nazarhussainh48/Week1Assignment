using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Week1Assignment1.Data;
using Week1Assignment1.DTO.Employee;
using Week1Assignment1.DTO.Weapon;
using Week1Assignment1.Models;

namespace Week1Assignment1.Services.WeaponService
{
    public class WeaponService : IWeaponService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public WeaponService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
        public async Task<GetEmployeeDto> AddWeapon(AddWeaponDto newWeapon)
        {
            
                Employee employee = await _context.Employees.FirstOrDefaultAsync(c=> c.Id == newWeapon.EmployeeId
                && c.User.Id == int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));
                if (employee == null)
                {
                    return null;
                }
                Weapon weapon = new Weapon
                {
                    Name = newWeapon.Name,
                    Damage = newWeapon.Damage,
                    Employee = employee 
                };
                await _context.Weapons.AddAsync(weapon);
                await _context.SaveChangesAsync();

                var data = _mapper.Map<GetEmployeeDto>(employee);
                return data;
            
        }
    }
}
