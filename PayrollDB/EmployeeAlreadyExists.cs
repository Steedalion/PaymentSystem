using System;

public class EmployeeAlreadyExists : Exception
{
    public EmployeeAlreadyExists()
    {
    }

    public EmployeeAlreadyExists(string message) : base(message)
    {
    }
}