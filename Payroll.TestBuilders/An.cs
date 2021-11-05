namespace Payroll.TestBuilders
{
    public class An
    {
        public static EmployeeBuilder Employee => new EmployeeBuilder();

        public static EmployeeBuilder GenericEmployee =>
            new EmployeeBuilder().MonthlySchedule().SalariedClass(salary).HoldPM();

        private const float salary = 100;
    }
}