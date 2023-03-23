using CodeChallenge.Models;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CodeChallenge.Repositories
{
    public interface IEmployeeRepository
    {
        Employee GetById(String id);
        Employee GetById(String id, Expression<Func<Employee, object>> includes);
        Employee Add(Employee employee);
        Employee Remove(Employee employee);
        Compensation Add(Employee employee, Compensation compensation);
        Task SaveAsync();
    }
}