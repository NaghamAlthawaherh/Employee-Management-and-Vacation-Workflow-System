namespace Employee_Management;
public class ApprovedVacationRequestDto
{
    public string VacationType { get; set; } = string.Empty;
    public string VacationDescription { get; set; } = string.Empty;
    public string RequestDuration { get; set; } = string.Empty;
    public int TotalVacationDays { get; set; }
    public string ApprovedBy { get; set; } = string.Empty;
}
