using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Employee_Management;

using Employee_Management.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using Microsoft.VisualBasic;

public partial class Program
{
    public void EmployeeService(Employee_DbContext context)
    {
        context = context;
    }
    static async Task Main(string[] args)
    {
                // // Set up dependency injection
        var serviceProvider = new ServiceCollection()
            .AddDbContext<Employee_DbContext>(options =>
                options.UseSqlServer("Server=DESKTOP-51BRH30;Database=[Employee Management];Trusted_Connection=True;TrustServerCertificate=True;Connect Timeout=60;"))
            .AddScoped<VacationService>()
            .BuildServiceProvider();
        var vacationService2 = serviceProvider.GetRequiredService<VacationService>();

        // Admin menu
        while (true)
        {
            Console.WriteLine("Admin Menu:");
            Console.WriteLine("1. Create Vacation Request");
            Console.WriteLine("2. View and Take Action on Pending Vacation Requests");
            Console.WriteLine("3. Exit");
            Console.Write("Select an option: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await CreateVacationRequest(vacationService2);
                    break;
                case "2":
                    await ShowPendingRequestsAndHandleActions(vacationService2);
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
        var options = new DbContextOptionsBuilder<Employee_DbContext>()
            .UseSqlServer("Server=DESKTOP-51BRH30;Database=[Employee Management];Trusted_Connection=True;TrustServerCertificate=True;Connect Timeout=60;")
            .Options;
        var context = new Employee_DbContext(options);



        //   AddDepartment(context);
        //   AddPositions(context);
        //   UpdateEmployee(context);
        //  AddVacationTypes(context);
        // AddRequeststate(context);


        // LINQ 1
        // var emp=from employee in context.Employees
        // select new{
        //     employee.EmployeeNumber,
        //     employee.EmployeeName,
        //     employee.DepartmentId,
        //     employee.Salary

        // };
        // foreach (var emps in emp)
        // {
        //     Console.WriteLine($"Employee No: {emps.EmployeeNumber}, Name: {emps.EmployeeName}, Department: {emps.DepartmentId}, Salary: {emps.Salary}");
        // }  


        var employeeService = new EmployeeService(context);
        //  LINQ 2  
        // Console.Write("Enter Employee Number to Reeturn: ");
        // string? employeeNumber = Console.ReadLine();

        // await employeeService.GetEmployeeDetailsAsync(employeeNumber);


        //    LINQ 3
        //    var employeesWithPendingRequests = await employeeService.GetEmployeesWithPendingVacationRequestsAsync();

        //     // Print out the employees
        //     if (employeesWithPendingRequests.Any())
        //     {
        //         Console.WriteLine("Employees with pending vacation requests:");
        //         foreach (var employee in employeesWithPendingRequests)
        //         {
        //             Console.WriteLine($"Employee Number: {employee.EmployeeNumber}, Name: {employee.EmployeeName}");
        //         }
        //     }
        //     else
        //     {
        //         Console.WriteLine("No employees have pending vacation requests.");
        //     }

        // LINQ 4
        //  Console.Write("Enter Employee Number: ");
        //         string employeeNumber1 = Console.ReadLine();
        //         if (string.IsNullOrWhiteSpace(employeeNumber1))
        // {
        //     Console.WriteLine("Invalid Employee Number.");
        //     return;
        // }

        //  var vacationHistory = await employeeService.GetApprovedVacationRequestsHistoryAsync(employeeNumber1);

        //         if (vacationHistory.Any())
        //         {
        //             Console.WriteLine("Approved Vacation Requests:");
        //             foreach (var request in vacationHistory)
        //             {
        //                 Console.WriteLine($"Vacation Type: {request.VacationType}");
        //                 Console.WriteLine($"Description: {request.VacationDescription}");
        //                 Console.WriteLine($"Duration: {request.RequestDuration}");
        //                 Console.WriteLine($"Total Days: {request.TotalVacationDays}");
        //                 Console.WriteLine($"Approved By: {request.ApprovedBy}");
        //                 Console.WriteLine("--------------------------------------------------");
        //             }
        //         }
        //         else
        //         {
        //             Console.WriteLine("No approved vacation requests found for this employee.");
        //         }

        //  LINQ 5
        //                    var vacationService = new VacationService(context);
        //          var pendingRequests = await vacationService.GetPendingVacationRequestsAsync();

        // if (pendingRequests.Any())
        // {
        //     Console.WriteLine("Pending Vacation Requests:");
        //     foreach (var request in pendingRequests)
        //     {
        //         Console.WriteLine($"Description: {request.VacationDescription}");
        //         Console.WriteLine($"Employee Number: {request.EmployeeNumber}");
        //         Console.WriteLine($"Employee Name: {request.EmployeeName}");
        //         Console.WriteLine($"Submitted On: {request.SubmittedOn:yyyy-MM-dd}");
        //         Console.WriteLine($"Vacation Duration: {request.VacationDuration}");
        //         Console.WriteLine($"Start Date: {request.StartDate:yyyy-MM-dd}");
        //         Console.WriteLine($"End Date: {request.EndDate:yyyy-MM-dd}");
        //         Console.WriteLine($"Employee Salary: {request.EmployeeSalary:C}");
        //         Console.WriteLine("--------------------------------------------------");
        //     }
        // }
        // else
        // {
        //     Console.WriteLine("No pending vacation requests found.");
        // }  
    }


    private static void AddDepartment(Employee_DbContext dbContext)
    {


        using (var context = new Employee_DbContext())
        {
            var departments = new List<Department>
            {
                new Department {  DepartmentName = "Human Resources" },
                new Department {  DepartmentName = "Finance" },
                new Department {  DepartmentName = "Information Technology" },
                new Department {  DepartmentName = "Marketing" },
                new Department {  DepartmentName = "Sales" },
                new Department {  DepartmentName = "Customer Service" },
                new Department { DepartmentName = "Operations" },
                new Department {  DepartmentName = "Research & Development" },
                new Department {  DepartmentName = "Legal" },
                new Department {  DepartmentName = "Procurement" },
                new Department {  DepartmentName = "Administration" },
                new Department {  DepartmentName = "Logistics & Supply Chain" },
                new Department {  DepartmentName = "Engineering" },
                new Department {  DepartmentName = "Health & Safety" },
                new Department {  DepartmentName = "Public Relations" },
                new Department {  DepartmentName = "Training & Development" },
                new Department {  DepartmentName = "Business Strategy" },
                new Department {  DepartmentName = "Quality Assurance" },
                new Department {  DepartmentName = "Product Management" },
                new Department {  DepartmentName = "Security" }
            };

            context.Departments.AddRange(departments); // Bulk insert departments
            context.SaveChanges(); // Save changes to database
            Console.WriteLine("20 departments added successfully.");
        }
    }
    private static void AddPositions(Employee_DbContext dbContext)
    {

        var positions = new List<Position>{
          new Position{PositionName="HR Manager"},
          new Position{PositionName="Financial Analyst"},
          new Position{PositionName="Software Engineer"},
          new Position{PositionName="Marketing Specialist"},
          new Position{PositionName="Sales Executive"},
          new Position{PositionName="Customer Support Representaion"},
          new Position{PositionName="Operations Manager"},
          new Position{PositionName="Research Scientist"},
          new Position{PositionName="Legal Advisor"},
          new Position{PositionName="Procurement Officer"},
          new Position{PositionName="Administrative Coordinator"},
          new Position{PositionName="Supply Chain Analyst"},
          new Position{PositionName="Mechanical Engineer"},
          new Position{PositionName="Safty Officer"},
          new Position{PositionName="Public Relation Specialist"},
          new Position{PositionName="Training Coordinator"},
          new Position{PositionName="Business Consultant"},
          new Position{PositionName="Quality Control Inspector"},
          new Position{PositionName="Product Manager"},
          new Position{PositionName="Security Supervisor"}
         };
        dbContext.AddRange(positions);
        dbContext.SaveChanges();
        Console.WriteLine("20 Positions added successfully.");

    }
    static void AddEmployee(Employee_DbContext dbContext)
    {

        var employees = new List<Employee>{
            new Employee("EMP001","Ahmad Saeed",1,1,'M',null,2000),
            new Employee("EMP002","Mohammed Bakri",2,2,'M',"EMP001",500),
            new Employee("EMP010", "Mariam Hashmi", 19, 19, 'F', null, 3000.75m),
            new Employee("EMP008", "Layla Kindi", 11, 11, 'F', "EMP001", 400.5m),
            new Employee("EMP003", "Fatima Zahra", 3, 3, 'F', "EMP010", 500.5m),
            new Employee("EMP004", "Noor Elhadi", 4, 4, 'F', "EMP008", 700.75m),
            new Employee("EMP005", "Khalid Mansour", 5, 5, 'M', "EMP004", 650.0m),
            new Employee("EMP007", "Youssef Otaibi", 18, 18, 'M', "EMP010", 1000.0m),
            new Employee("EMP006", "Jana Abdulkarim", 6, 6, 'F', "EMP007", 950.3m),
            new Employee("EMP009", "Amr Dajani", 13, 13, 'M', "EMP008", 470.2m),
            };
        dbContext.Employees.AddRange(employees);
        dbContext.SaveChanges();
        Console.WriteLine("10 Employees added successfully.");

    }
    static void AddVacationTypes(Employee_DbContext dbContext)
    {
        var types = new List<VacationType>{

    new VacationType { Code = "A", VacationTypeName = "Annual Leave" },
            new VacationType { Code = "S", VacationTypeName = "Sick Leave" },
            new VacationType { Code = "M", VacationTypeName = "Maternity Leave" },
            new VacationType { Code = "P", VacationTypeName = "Public Holiday" },
            new VacationType { Code = "U", VacationTypeName = "Unpaid Leave"}
            };
        dbContext.AddRange(types);
        dbContext.SaveChanges();
        Console.WriteLine("Added successfully.");
    }
    public static void AddRequeststate(Employee_DbContext dbContext)
    {
        dbContext.ChangeTracker.Clear();
        var existingState = dbContext.RequestStates.Find(1);  // Example: Check for StateId=1
        if (existingState == null)
        {
            var newState = new List<RequestState>{
new RequestState{StateName="Pending"},
new RequestState{StateName="Approved"},
new RequestState{StateName="Declined"}
};
            dbContext.AddRange(newState);
        }
        else
        {
            dbContext.Attach(existingState);

        }
        dbContext.SaveChangesAsync();
    }

    static void UpdateEmployee(Employee_DbContext dbContext)
    {

        var service = new EmployeeService(dbContext);

        Console.Write("Enter Employee Number to Update: ");
        string? employeeNumber = Console.ReadLine();
        Console.Write("Enter New Name: ");
        string? newName = Console.ReadLine();

        Console.Write("Enter New Department ID: ");
        int newDepartmentId = int.Parse(Console.ReadLine());

        Console.Write("Enter New Position ID: ");
        int newPositionId = int.Parse(Console.ReadLine());

        Console.Write("Enter New Salary: ");
        decimal newSalary = decimal.Parse(Console.ReadLine());

        // Call the update method
        service.UpdateEmployeeInfo(employeeNumber, newName, newDepartmentId, newPositionId, newSalary);

    }
    static async Task CreateVacationRequest(VacationService vacationService)
    {
        try
        {
            Console.WriteLine("Enter vacation request details:");

            Console.Write("Employee Number: ");
            string employeeId = Console.ReadLine()!;

            Console.Write("Description: ");
            string description = Console.ReadLine()!;

            Console.Write("Vacation Type Code (e.g., A, S,U): ");
            string vacationTypeCode = Console.ReadLine()!;

            Console.Write("Start Date (yyyy-MM-dd): ");
            DateOnly startDate = DateOnly.Parse(Console.ReadLine()!);

            Console.Write("End Date (yyyy-MM-dd): ");
            DateOnly endDate = DateOnly.Parse(Console.ReadLine()!);

            int totalDays = endDate.DayNumber - startDate.DayNumber;


            // Create the vacation request
            var vacationRequest = await vacationService.CreateVacationRequestAsync(

               DateTime.UtcNow,
               description,
                employeeId,
              vacationTypeCode,
              startDate,
              endDate,
              totalDays,
              null,
              null
);

            Console.WriteLine($"Vacation request created successfully with ID: {vacationRequest.RequestId}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }


    private static async Task ShowPendingRequestsAndHandleActions(VacationService vacationService)
    {
        var pendingRequests = await vacationService.GetPendingRequestsAsync();

        if (pendingRequests.Count == 0)
        {
            Console.WriteLine("No pending vacation requests to review.");
            return;
        }

        Console.WriteLine("Pending Vacation Requests:");
        foreach (var request in pendingRequests)
        {
            Console.WriteLine($"Request ID: {request.RequestId}, Employee: {request.EmployeeNumber}, Start Date: {request.Start_Date}, End Date: {request.End_Date}");
        }

        Console.Write("Enter the Request ID you want to take action on: ");
        int requestId = int.Parse(Console.ReadLine()!);

        Console.WriteLine("Choose an action:");
        Console.WriteLine("1. Approve");
        Console.WriteLine("2. Decline");

        string action = Console.ReadLine()!.Trim();

        Console.Write("Enter your employee number: ");
        string employeeNumber = Console.ReadLine()!;

        if (action == "1")
        {
            await vacationService.ApproveVacationRequestAsync(requestId, employeeNumber);
        }
        else if (action == "2")
        {
            await vacationService.DeclineVacationRequestAsync(requestId, employeeNumber);
        }
        else
        {
            Console.WriteLine("Invalid action. Please choose 1 (Approve) or 2 (Decline).");
        }

    }
}












































