using Microsoft.EntityFrameworkCore;
namespace Employee_Management.Models;
 

 public class Department{
   
 public int DepartmentId{get; set;}
 public  required string DepartmentName{get; set;}
 public ICollection <Employee>Employees{get; set;}=new List<Employee>();
 }