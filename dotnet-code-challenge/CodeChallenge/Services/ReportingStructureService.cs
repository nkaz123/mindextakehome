using CodeChallenge.Models;

namespace CodeChallenge.Services
{
    public class ReportingStructureService : IReportingStructureService
    {
        private readonly IEmployeeService _employeeService;

        public ReportingStructureService(IEmployeeService employeeService)
        {
            _employeeService= employeeService;
        }

        public ReportingStructure GetReportingStructure(string id)
        {
            var employee = _employeeService.GetById(id, e=> e.DirectReports);

            if(employee == null)
            {
                return null;
            }

            var reportingStructure = new ReportingStructure()
            {
                Employee = employee,
                NumberOfReports = 0,
            };

            CalculatNumReports(employee, reportingStructure);

            return reportingStructure;
        }

        private void CalculatNumReports(Employee employee, ReportingStructure reportingStructure)
        {
            if (employee?.DirectReports?.Count > 0)
            {
                foreach(var directReport in employee.DirectReports)
                {
                    reportingStructure.NumberOfReports++;
                    directReport.DirectReports = _employeeService.GetDirectReports(directReport.EmployeeId);

                    CalculatNumReports(directReport, reportingStructure);
                }
            }
        }
    }
}
