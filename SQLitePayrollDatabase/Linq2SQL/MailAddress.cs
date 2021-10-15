using System.Data.Linq.Mapping;
using PaymentMethods;

namespace PayrollDataBase.Linq2SQL
{
    [Table(Name = "PaycheckAddress")]
    public class MailAddress
    {
        [Column(IsPrimaryKey = true, Name = nameof(EmpID))]
        public int EmpID;

        [Column(Name = nameof(Address))] public string Address;

        public MailAddress()
        {
        }

        public MailAddress(int id, MailPaymentMethod mail)
        {
            EmpID = id;
            Address = mail.Address;
        }
    }
}