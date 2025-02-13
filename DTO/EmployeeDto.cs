using Employee_Management.Models;
using System;
namespace Employee_Management;

public class EmployeeDto{

    // Employee's unique identifier
    public string? EmployeeNumber { get; set; }

    // Full name of the employee
    public string? EmployeeName { get; set; }

    // Name of the department the employee belongs to
    public string? DepartmentName { get; set; }

    // Position or job title of the employee
    public string? PositionName { get; set; }

    // Name of the employee's manager (if applicable)
    public string? ReportedToEmployeeName { get; set; }

    // Total number of vacation days the employee has left
    public int TotalVacationDaysLeft { get; set; }
}



