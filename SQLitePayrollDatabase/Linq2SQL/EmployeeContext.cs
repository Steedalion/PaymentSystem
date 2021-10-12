using System;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using PaymentMethods;
using PayrollDataBase;
using PayrollDomain;

namespace DatabaseTests.SQLiteTests
{
    public class EmployeeContext : DataContext
    {
        public EmployeeContext(IDbConnection connection) : base(connection)
        {
        }

        public Table<EmployeeUnit> Employees;
        public Table<Account> DirectDepositAccounts;
        public Table<PaycheckAddress> PaycheckAddresses;
    }

    [Table(Name = "PaycheckAddress")]
    public class PaycheckAddress
    {
        [Column(IsPrimaryKey = true, Name = nameof(EmpID))]
        public int EmpID;

        [Column(Name = nameof(Address))] public string Address;

        public PaycheckAddress()
        {
        }

        public PaycheckAddress(int id, MailPaymentMethod mail)
        {
            EmpID = id;
            Address = mail.Address;
        }
    }

    [Table(Name = "DirectDepositAccount")]
    public class Account
    {
        [Column(IsPrimaryKey = true, Name = nameof(EmpID))]
        public int EmpID;

        [Column(Name = nameof(Bank))] public string Bank;
        [Column(Name = "Account")] public int AccountNumber;

        public Account(int id, AccountPaymentMethod acc)
        {
            EmpID = id;
            Bank = acc.bank;
            AccountNumber = acc.AccountNumber;
        }

        public Account()
        {
        }
    }

    public class PaymentMethodUnit
    {
        public PaymentMethodUnit(int id, PaymentMethod employeePaymentmethod)
        {
            throw new NotImplementedException();
        }
    }

    public class PaymentScheduleUnit
    {
        public PaymentScheduleUnit(int id, PaymentSchedule employeeSchedule)
        {
            throw new NotImplementedException();
        }
    }

    public class PaymentClassificationUnit
    {
        public PaymentClassificationUnit(int id, PayrollDomain.PaymentClassification employeeClassification)
        {
            throw new NotImplementedException();
        }
    }
}