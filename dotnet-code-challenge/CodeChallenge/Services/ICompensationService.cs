using CodeChallenge.Models;
using System.Collections.Generic;
using System;

namespace CodeChallenge.Services
{
    public interface ICompensationService
    {
        Compensation Create(String employeeId, Compensation compensation);
        List<Compensation> GetByEmployeeId(String id);
    }
}
