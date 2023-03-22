using AutoMapper;
using Week1Assignment1.Data;
using Week1Assignment1.DTO.Employee;
using Week1Assignment1.Helper;
using Week1Assignment1.Models;
using Week1Assignment1.Repository;

namespace Week1Assignment1.Services.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        /// <summary>
        /// Injecting Services
        /// </summary>
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Employee> _genericRepository;

        public EmployeeService(IMapper mapper, IGenericRepository<Employee> genericRepository)
        {
            _mapper = mapper;
            _genericRepository = genericRepository;
        }

        /// <summary>
        /// Add new employee
        /// </summary>
        /// <param name="newEmployee"></param>
        /// <returns>List of Employees</returns>
        public async Task<GetEmployeeDto> AddEmployee(GetEmployeeDto newEmployee)
        {
            var model = _mapper.Map<Employee>(newEmployee);
            var result = await _genericRepository.Insert(model);
            var model1 = _mapper.Map<GetEmployeeDto>(result);
            return model1;
        }

        /// <summary>
        /// Delete Employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<GetEmployeeDto> DeleteEmployee(int id)
        {
            var data = await _genericRepository.Delete(id);
            var model1 = _mapper.Map<GetEmployeeDto>(data);
            return model1;
        }

        /// <summary>
        /// Get all employees
        /// </summary>
        /// <returns></returns>
        public List<GetEmployeeDto> GetAllEmployees(QueryParameters queryParameter)
        {
            var result1 = _genericRepository.GetAll(
                queryParameter.OrderBy,
                queryParameter.PageSize,
                queryParameter.PageNumber,
                queryParameter.FilterExpression
            );
            var result = _mapper.Map<List<GetEmployeeDto>>(result1);
            return result;
        }

        /// <summary>
        /// Get Employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<GetEmployeeDto> GetEmployeeById(int id)
        {
            var result = _genericRepository.GetSingle(id);

            if (result == null)
                return null;

            var data = _mapper.Map<GetEmployeeDto>(result);
            return data;
        }

        /// <summary>
        /// update employee
        /// </summary>
        /// <param name="updatedEmployee"></param>
        /// <returns></returns>
        public async Task<GetEmployeeDto> UpdateEmployee(GetEmployeeDto updatedEmployee)
        {
            var data = _mapper.Map<Employee>(_genericRepository.GetSingle(updatedEmployee.Id));

            if (data == null)
                throw new Exception();

            data.Name = updatedEmployee.Name;
            data.EmployeeDept = updatedEmployee.EmployeeDept;
            data.Salary = updatedEmployee.Salary;
            await _genericRepository.Update(data);
            var model1 = _mapper.Map<GetEmployeeDto>(_genericRepository.GetSingle(updatedEmployee.Id));
            return model1;
        }
    }
}
