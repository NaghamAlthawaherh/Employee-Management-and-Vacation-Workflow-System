namespace Employee_Management.Models;

public class RequestState{

public int StateId{ get; set; }
public string StateName { get; set; }

public ICollection<VacationRequest> VacationRequests { get; set; }=new List<VacationRequest>();



}