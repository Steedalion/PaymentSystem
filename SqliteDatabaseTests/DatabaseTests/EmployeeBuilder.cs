using PaymentClassifications.PaymentClassifications;
using PaymentMethods;
using PayrollDomain;
using Schedules;

namespace DatabaseTests.DatabaseTests
{
    public class EmployeeBuilder
    {
        private int id;
        private string name;
        private string address;
        private PaymentSchedule schedule;
        private PaymentClassification classification;
        private PaymentMethod paymentMethod;

        public static implicit operator Employee(EmployeeBuilder builder) => builder.Build();

        public Employee Build()
        {
            Employee e = new Employee(id, name, address);
            e.Schedule = schedule;
            e.Classification = classification;
            e.Paymentmethod = paymentMethod;
            return e;
        }

        public EmployeeBuilder WeeklySchedule()
        {
            schedule = new WeeklySchedule();
            return this;
        }

        public EmployeeBuilder MonthlySchedule()
        {
            schedule = new MonthlyPaymentSchedule();
            return this;
        }

        public EmployeeBuilder SalariedClass(float salary)
        {
            classification = new SalariedClassification(salary);
            return this;
        }

        public EmployeeBuilder Hold()
        {
            paymentMethod = new HoldMethod();
            return this;
        }

        public EmployeeBuilder BiweeklySchedule()
        {
            schedule = new Biweekly();
            return this;
        }

        public EmployeeBuilder Hourly(double rate)
        {
            classification = new HourlyClassification(rate);
            return this;
        }

        public EmployeeBuilder Commision(double commisionRate, double salary)
        {
            classification = new CommisionClassification(commisionRate, salary);
            return this;
        }

        public EmployeeBuilder Mail(string address)
        {
            paymentMethod = new MailPaymentMethod(address);
            return this;
        }

        public EmployeeBuilder Bank(string BankName, int accountNumber)
        {
            paymentMethod = new AccountPaymentMethod(BankName, accountNumber);
            return this;
        }
    }
}