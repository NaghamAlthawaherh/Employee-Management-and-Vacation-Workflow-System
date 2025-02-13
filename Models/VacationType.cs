namespace Employee_Management.Models;

public class VacationType{

public string? Code{set;get;}
public string? VacationTypeName{ get; set; }

 public ICollection<VacationRequest> VacationRequests { get; set; }=new List<VacationRequest>();


}