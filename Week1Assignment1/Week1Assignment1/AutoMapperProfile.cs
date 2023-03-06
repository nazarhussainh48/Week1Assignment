using AutoMapper;
using Week1Assignment1.DTO.Employee;
using Week1Assignment1.DTO.Weapon;
using Week1Assignment1.Models;

namespace Week1Assignment1
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Employee, GetEmployeeDto>();
            CreateMap<GetEmployeeDto, Employee>();
            CreateMap<Weapon, GetWeaponDto>(); //for one to one relation
        }
    }
}
