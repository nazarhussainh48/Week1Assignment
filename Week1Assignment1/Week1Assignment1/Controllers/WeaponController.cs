using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Week1Assignment1.DTO.Weapon;
using Week1Assignment1.Services.WeaponService;

namespace Week1Assignment1.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeaponController : BaseController
    {
        private readonly IWeaponService _weaponService;

        public WeaponController(IWeaponService weaponService)
        {
            _weaponService = weaponService;
        }

        [HttpPost]
        public async Task<IActionResult> AddWeapon(AddWeaponDto newWeapon)
        {
            try
            { 
            var result = await _weaponService.AddWeapon(newWeapon);
            return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
