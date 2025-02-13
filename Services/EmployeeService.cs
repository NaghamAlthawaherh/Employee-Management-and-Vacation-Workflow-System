using Employee_Management;
// using Employee_Management.Migrations;
using Employee_Management.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
namespace Employee_Management;
public class EmployeeService
{
    private readonly Employee_DbContext _context;

    public  EmployeeService(Employee_DbContext context)
    {
        _context = context;
       
    }

    // public EmployeeService()
    // {
    // }

    //  Update Employee Information using Employee Number
    public void UpdateEmployeeInfo(string employeeNumber, string newName, int newDepartmentId, int newPositionId, decimal newSalary)
    {
        var employee = _context.Employees.FirstOrDefault(e => e.EmployeeNumber == employeeNumber);
        
        if (employee != null)
        {
            // Update fields
            employee.EmployeeName = newName;
            employee.DepartmentId = newDepartmentId;
            employee.PositionId = newPositionId;
            employee.Salary = newSalary;

            _context.SaveChanges();
            Console.WriteLine("Employee information updated successfully.");
        }
        else
        {
            Console.WriteLine("Employee not found.");
        }}
        public async Task GetEmployeeDetailsAsync(string employeeNumber)
{
    var employeeDetails = await (from e in _context.Employees
                                 join d in _context.Departments on e.DepartmentId equals d.DepartmentId
                                 join p in _context.Positions on e.PositionId equals p.PositionId
                                 join r in _context.Employees on e.ReportsTo equals r.EmployeeNumber into reportedTo
                                 from rt in reportedTo.DefaultIfEmpty() // To handle employees who do not report to anyone
                                 where e.EmployeeNumber == employeeNumber
                                 select new
                                 {
                                     EmployeeNumber = e.EmployeeNumber,
                                     EmployeeName = e.EmployeeName,
                                     DepartmentName = d.DepartmentName,
                                     PositionName = p.PositionName,
                                     ReportedToEmployeeName = rt != null ? rt.EmployeeName : "None", // Handle cases where there's no reported employee
                                     VacationDaysLeft = e.VacationDaysLeft
                                 }).FirstOrDefaultAsync();

    if (employeeDetails != null)
    {
        Console.WriteLine($"Employee Number: {employeeDetails.EmployeeNumber}");
        Console.WriteLine($"Employee Name: {employeeDetails.EmployeeName}");
        Console.WriteLine($"Department Name: {employeeDetails.DepartmentName}");
        Console.WriteLine($"Position Name: {employeeDetails.PositionName}");
        Console.WriteLine($"Reported To: {employeeDetails.ReportedToEmployeeName}");
        Console.WriteLine($"Total Vacation Days Left: {employeeDetails.VacationDaysLeft}");
    }
    else
    {
        Console.WriteLine($"No employee found with number {employeeNumber}");
    }}
public async Task<List<Employee>> GetEmployeesWithPendingVacationRequestsAsync()
{
    // Query to get all employees who have one or more pending vacation requests
    var employeesWithPendingRequests = await _context.Employees
        .Where(e => e.VacationRequests.Any(vr => vr.RequestState_Id == 1)) // 1 is for Pending state
        .ToListAsync();

    return employeesWithPendingRequests;
}
public async Task<List<ApprovedVacationRequestDto>> GetApprovedVacationRequestsHistoryAsync(string employeeNumber)
{
    var approvedRequests = await _context.VacationRequests
        .Where(vr => vr.EmployeeNumber == employeeNumber && vr.RequestState_Id == 2) // 2 is for Approved state
        .Select(vr => new ApprovedVacationRequestDto
        {
            VacationType = vr.VacationType.VacationTypeName,  // Assuming a navigation property exists
            VacationDescription = vr.Description,
            RequestDuration = $"{vr.Start_Date.ToShortDateString()} - {vr.End_Date.ToShortDateString()}",
            TotalVacationDays = vr.Total_Days,
            ApprovedBy = _context.Employees
                .Where(e => e.EmployeeNumber == vr.ApprovedBy)
                .Select(e => e.ReportsTo) // Assuming an Employee table has the Approver's Name
                .FirstOrDefault()?? "UnKnown"
        })
        .ToListAsync();

    return approvedRequests;
}

}




    



    








