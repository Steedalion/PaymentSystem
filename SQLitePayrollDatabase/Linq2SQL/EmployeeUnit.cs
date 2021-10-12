using System;
using System.Data.Linq.Mapping;
using PayrollDataBase;
using PayrollDomain;

namespace DatabaseTests.SQLiteTests
{
    [Table(Name = "Employees")]
    public class EmployeeUnit
    {
        [Column(IsPrimaryKey = true, Name = nameof(EmpID))]
        public int EmpID;

        [Column(Name = nameof(Name))] public string Name;
        [Column(Name = nameof(Address))] public string Address;
        [Column(Name = nameof(ScheduleType))] public string ScheduleType;

        [Column(Name = nameof(PaymentMethodType))]
        public string PaymentMethodType;

        [Column(Name = nameof(PaymentClassificationType))]
        public string PaymentClassificationType;

        public EmployeeUnit()
        {
        }

        public EmployeeUnit(int empId, Employee emp)
        {
            EmpID = empId;
            Name = emp.Name;
            Address = emp.myAddress;
            ScheduleType = ScheduleCodes.Code(emp.Schedule);
            PaymentMethodType = PaymentMethodCodes.Code(emp.Paymentmethod);
            PaymentClassificationType = ClassificationCodes.Code(emp.Classification);
        }

        public static EmployeeUnit FromEmployee(int empID, Employee employee)
        {
            return new EmployeeUnit(empID, employee);
        }

        public Employee toEmployee()
        {
            Employee emp = new Employee(EmpID, Name, Address);
            return emp;
        }
    }
}