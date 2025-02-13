using System.ComponentModel.DataAnnotations.Schema;

namespace Employee_Management.Models;

public class VacationRequest{
[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
public int RequestId{   get; set; }
public DateTime SubmissionDate{ get; set; }
public required string Description{  get; set; }
public  required string EmployeeNumber{ get; set; }
public Employee? Employee { get; set; } // Employee who submitted the request
public VacationType? VacationType { get; set; } // Vacation Type (S, U, A, O, B)
 public string? VacationType_Code{ get; set; }
 public DateOnly Start_Date{ get; set; }
 public DateOnly End_Date{ get; set; }
 public int Total_Days{ get; set; }
 public RequestState? RequestState { get; set; } // Request State (Submitted, Approved, Declined)
 public int? RequestState_Id{    get; set; }
 public string? ApprovedBy { get; set; }
public Employee? Approver { get; set; } // Approver (Nullable)
public string? DeclinedBy { get; set; }
public Employee? Decliner { get; set; } // Decliner (Nullable)


  public void Approve(string approver)
    {
        RequestState_Id = 2; // Approved
        ApprovedBy = approver;
    }

 public void Decline(string decliner)
    {
        RequestState_Id = 3; // Declined
        DeclinedBy = decliner;
    }



}

