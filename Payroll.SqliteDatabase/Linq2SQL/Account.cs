using System.Data.Linq.Mapping;
using PaymentMethods;

namespace PayrollDataBase.Linq2SQL
{
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
}