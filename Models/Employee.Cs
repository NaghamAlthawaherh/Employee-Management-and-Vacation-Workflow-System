using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee_Management.Models;

public class Employee{

 public Employee() {}
public Employee(string employeeNumber,string employeeName,int departmentId,int positionId,char gender,string? reportsTo,decimal salary){
EmployeeNumber=employeeNumber;
EmployeeName=employeeName;
DepartmentId=departmentId;
PositionId=positionId;
Gender=gender;
ReportsTo=reportsTo;
Salary=salary;
VacationDaysLeft=24;
}

[DatabaseGenerated(DatabaseGeneratedOption.None)]
public  string EmployeeNumber{ get; set; }=string.Empty;
public string? EmployeeName { get; set; }
public int DepartmentId{ get; set; }
public Department? Department { get; set; }
public int? PositionId{ get; set; }
public Position? Position{ get; set; }

public char Gender{ get; set; }

public string? ReportsTo{ get; set; }
   
public Employee? Manager { get; set; }
public ICollection<Employee>DirectReports{ get; set; }=new List<Employee>();

 
 [Range(0, 24, ErrorMessage = "Vacation days cannot exceed 24 or be negative.")]
 public int VacationDaysLeft { get; set; }
public decimal Salary { get; set; }

public ICollection<VacationRequest>?VacationRequests { get; set; }





}