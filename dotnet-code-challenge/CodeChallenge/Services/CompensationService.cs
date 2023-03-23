using CodeChallenge.Models;
using CodeChallenge.Repositories;
using System;
using System.Collections.Generic;

namespace CodeChallenge.Services
{
    public class CompensationService : ICompensationService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public CompensationService(IEmployeeRepository employeeRepository) 
        {
            _employeeRepository = employeeRepository;
        }

        public Compensation Create(String employeeId, Compensation compensation)
        {
            if (compensation == null)
            {
                return null;
            }

            var employee = _employeeRepository.GetById(employeeId);

            if(employee == null)
            {
                return null;
            }

            _employeeRepository.Add(employee, compensation);
            _employeeRepository.SaveAsync().Wait();

            return compensation;
        }

        public List<Compensation> GetByEmployeeId(String id)
        {
            var employee = _employeeRepository.GetById(id, e => e.Compensations);

            if (employee == null)
            {
                return null;
            }

            return employee.Compensations;
        }
    }
}
