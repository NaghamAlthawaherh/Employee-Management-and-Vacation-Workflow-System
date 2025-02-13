namespace Employee_Management.Models;

public class Position{

public int PositionId { get; set; }
public required string PositionName { get; set;}

public ICollection <Employee>Employees { get; set; }=new List<Employee>();


}