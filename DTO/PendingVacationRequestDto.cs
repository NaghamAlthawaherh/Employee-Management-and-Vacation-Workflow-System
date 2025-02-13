namespace Employee_Management;

public class PendingVacationRequestDto
{
    public string VacationDescription { get; set; } = string.Empty;
    public string EmployeeNumber { get; set; } = string.Empty;
    public string EmployeeName { get; set; } = string.Empty;
    public DateTime SubmittedOn { get; set; }
    public string VacationDuration { get; set; } = string.Empty;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public decimal EmployeeSalary { get; set; }
}
