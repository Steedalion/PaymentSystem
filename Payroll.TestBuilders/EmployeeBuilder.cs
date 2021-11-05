using PaymentClassifications.PaymentClassifications;
using PaymentMethods;
using PayrollDomain;
using Schedules;

namespace Payroll.TestBuilders
{
    public class EmployeeBuilder
    {
        private int id;
        private string name;
        private string address;
        private IPaymentSchedule schedule;
        private IPaymentClassification classification;
        private IPaymentMethod paymentMethod;

        public static implicit operator Employee(EmployeeBuilder builder) => builder.Build();

        public Employee Build()
        {
            Employee e = new Employee(id, name, address);
            e.Schedule = schedule;
            e.Classification = classification;
            e.Paymentmethod = paymentMethod;
            return e;
        }

        public EmployeeBuilder WithoutPaymentClassification()
        {
            classification = null;
            return this;
        }

        public EmployeeBuilder SalariedClass(float salary)
        {
            classification = new SalariedClassification(salary);
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

        public EmployeeBuilder WithoutSchedule()
        {
            schedule = null;
            return this;
        }

        public EmployeeBuilder WithSchedule<T>() where T : IPaymentSchedule, new()
        {
            schedule = new T();
            return this;
        }

        public EmployeeBuilder BiweeklySchedule() => WithSchedule<Biweekly>();
        public EmployeeBuilder WeeklySchedule() => WithSchedule<WeeklySchedule>();
        public EmployeeBuilder MonthlySchedule() => WithSchedule<MonthlyPaymentSchedule>();


        public EmployeeBuilder MailPM(string address)
        {
            paymentMethod = new MailPaymentMethod(address);
            return this;
        }

        public EmployeeBuilder Bank(string BankName, int accountNumber)
        {
            paymentMethod = new AccountPaymentMethod(BankName, accountNumber);
            return this;
        }

        public EmployeeBuilder HoldPM()
        {
            paymentMethod = new HoldMethod();
            return this;
        }

        public EmployeeBuilder WithoutPaymentMethod()
        {
            paymentMethod = null;
            return this;
        }
    }
}