namespace Payroll
{
    class CommisionClassification : PaymentClassification
    {
        public CommisionClassification(double commisionRate, double salary)
        {
            CommisionRate = commisionRate;
            Salary = salary;
        }

        public double CommisionRate{ get; }
        public double Salary{ get; }
    }
}