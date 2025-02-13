using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Employee_Management.Models;
using Employee_Management;


public class VacationService
{
    private readonly Employee_DbContext _context;

    public VacationService(Employee_DbContext context)
    {
        _context = context;
    }

    public  async Task<VacationRequest> CreateVacationRequestAsync(DateTime submission,string descriptipn,string employeeId,string vacationtypecode ,DateOnly startDate, DateOnly endDate,int totalDays,string? approver,string? decliner)
        {       
 if (startDate >= endDate)
            {
                throw new ArgumentException("End date must be after start date.");
            }
              //  Check for overlapping vacations
    bool hasOverlap = await _context.VacationRequests
        .AnyAsync(v => v.EmployeeNumber == employeeId &&
                       v.Start_Date < endDate &&
                       v.End_Date > startDate);

    if (hasOverlap)
    {
        throw new InvalidOperationException("Vacation request overlaps with an existing request.");
    }
   
         var vacationRequest = new VacationRequest
            {
                SubmissionDate=submission,
                Description=descriptipn,
                EmployeeNumber = employeeId,
                VacationType_Code=vacationtypecode,
                Start_Date = startDate,
                End_Date = endDate,
                Total_Days=totalDays,
                RequestState_Id =1,
                ApprovedBy=approver,
                DeclinedBy=decliner
            };
             // Add the request to the database
            _context.VacationRequests.Add(vacationRequest);
    try
    {
        await _context.SaveChangesAsync();
        Console.WriteLine("Vacation request saved successfully!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error saving changes: {ex.Message}");
        if (ex.InnerException != null)
        {
            Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
        }
        throw;
    }
            Console.WriteLine("Request added Succsessfully");

            return vacationRequest;
        }

        public async Task<RequestState?> GetRequestStateByIdAsync(int requestStateId)
{
    return await _context.RequestStates
        .AsNoTracking()  // Prevents tracking conflicts
        .FirstOrDefaultAsync(rs => rs.StateId == requestStateId);
}


    public async Task<bool> ApproveVacationRequestAsync(int requestId,string approver)
    {
        // Fetch the vacation request
        var vacationRequest = await _context.VacationRequests
            .Include(vr => vr.Employee) // Include the related Employee
            .FirstOrDefaultAsync(vr => vr.RequestId == requestId);

        if (vacationRequest == null)
        {
            throw new ArgumentException("Vacation request not found.");
        }

        // Calculate the number of vacation days requested
         var Total_Days = (vacationRequest.End_Date.ToDateTime(TimeOnly.MinValue) - 
                  vacationRequest.Start_Date.ToDateTime(TimeOnly.MinValue)).Days;


        // Check if the employee has enough vacation days left
        if (vacationRequest.Employee.VacationDaysLeft < Total_Days)
        {
            throw new InvalidOperationException("Employee does not have enough vacation days left.");
        }
         if(vacationRequest.RequestState_Id == 2)
        // Update the vacation request status
        
        vacationRequest.ApprovedBy = approver;
        // Deduct the vacation days from the employee's balance
        vacationRequest.Employee.VacationDaysLeft -= Total_Days;

        // Save changes to the database
        await _context.SaveChangesAsync();

        return true;
    }


    // Decline vacation request
   public async Task DeclineVacationRequestAsync(int requestId, string decliner)
    {
        var request = await _context.VacationRequests
            .FirstOrDefaultAsync(r => r.RequestId == requestId && r.RequestState_Id == 1); // Pending requests

        if (request != null)
        {
            request.Decline(decliner);
            await _context.SaveChangesAsync();
            Console.WriteLine($"Vacation request {requestId} has been declined.");
        }
        else
        {
            Console.WriteLine($"Vacation request {requestId} is not in pending state.");
        }
    }

    // Get all pending vacation requests
   public  async Task<List<VacationRequest>> GetPendingRequestsAsync()
    {
        return await _context.VacationRequests
            .Where(r => r.RequestState_Id == 1)  // Pending requests
            .ToListAsync();
    }
  public async Task UpdateVacationBalanceAfterApprovalAsync(int requestId)
{
    // Step 1: Retrieve the vacation request from the database
    var vacationRequest = await _context.VacationRequests
        .FirstOrDefaultAsync(r => r.RequestId == requestId && r.RequestState_Id == 2); // Approved state

    if (vacationRequest == null)
    {
        Console.WriteLine($"Vacation request {requestId} is not in approved state.");
        return;
    }

    // Step 2: Get the employee associated with the request
    var employee = await _context.Employees
        .FirstOrDefaultAsync(e => e.EmployeeNumber == vacationRequest.EmployeeNumber);

    if (employee == null)
    {
        Console.WriteLine($"Employee with ID {vacationRequest.EmployeeNumber} not found.");
        return;
    }

    // Step 3: Check if the employee has enough vacation days
    if (employee.VacationDaysLeft >= vacationRequest.Total_Days)
    {
        // Step 4: Deduct the vacation days from the employee's balance
        employee.VacationDaysLeft -= vacationRequest.Total_Days;

        // Step 5: Save changes to the database
        await _context.SaveChangesAsync();

        Console.WriteLine($"Vacation days for employee {employee.EmployeeNumber} have been updated.");
    }
    else
    {
        Console.WriteLine($"Employee {employee.EmployeeNumber} does not have enough vacation days.");
    }
}
public async Task<List<PendingVacationRequestDto>> GetPendingVacationRequestsAsync()
{
    var pendingRequests = await _context.VacationRequests
        .Where(vr => vr.RequestState_Id == 1) // 1 is for Pending state
        .Select(vr => new PendingVacationRequestDto
        {
            VacationDescription = vr.Description,
            EmployeeNumber = vr.Employee.EmployeeNumber, 
            EmployeeName = vr.Employee.EmployeeName,
            SubmittedOn = vr.SubmissionDate,
            VacationDuration = (vr.Total_Days > 7) ? $"{vr.Total_Days / 7} weeks" : $"{vr.Total_Days} days",
            StartDate = vr.Start_Date,
            EndDate = vr.End_Date,
            EmployeeSalary = vr.Employee.Salary
        })
        .ToListAsync();

    return pendingRequests;
}
}





