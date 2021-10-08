using PaymentMethods;
using PayrollDomain;

namespace PayrollDataBase
{
    public static class PaymentMethodCodes
    {
        public static string Hold = "HoldPayment";
        public static string Account = "AccountPayment";
        public static string Mail = "PostPayment";    
        public static string Code(PaymentMethod employeePaymentmethod)
        {
            if (employeePaymentmethod is AccountPaymentMethod)
            {
                return PaymentMethodCodes.Account;
            }

            if (employeePaymentmethod is HoldMethod)
            {
                return PaymentMethodCodes.Hold;
            }

            if (employeePaymentmethod is MailPaymentMethod)
            {
                return PaymentMethodCodes.Mail;
            }


            return "UnknownPayment";
        }
    }
}