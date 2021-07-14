namespace Payroll.Tests.SQLiteTests
{
    public static class MethodCodes
    {
        public static string Hold = "HoldPayment";
        public static string Account = "AccountPayment";
        public static string Mail = "PostPayment";    
        public static string Code(PaymentMethod employeePaymentmethod)
        {
            if (employeePaymentmethod is AccountPaymentMethod)
            {
                return MethodCodes.Account;
            }

            if (employeePaymentmethod is HoldMethod)
            {
                return MethodCodes.Hold;
            }

            if (employeePaymentmethod is MailPaymentMethod)
            {
                return MethodCodes.Mail;
            }


            return "UnknownPayment";
        }
    }
}