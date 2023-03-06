using Week1Assignment1.DTO.Employee;
using Week1Assignment1.DTO.Weapon;

namespace Week1Assignment1.Services.WeaponService
{
    public interface IWeaponService
    {
        Task<GetEmployeeDto> AddWeapon(AddWeaponDto newWeapon);
    }
}
