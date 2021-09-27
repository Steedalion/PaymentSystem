using System;
using PaymentClassification;
using PayrollDB;
using Transactions.DBTransaction;

namespace Presenters
{
    public class AddEmployeePresenter
    {
        private readonly AddEmployeeView view;
        public readonly TransactionContainer Container;
        private readonly IPayrollDb db;

        public int EmpId
        {
            get => empId;
            set
            {
                empId = value;
                UpdateView();
            }
        }

        public bool IsHourly
        {
            get => isHourly;
            set
            {
                isHourly = value;
                UpdateView();
            }
        }

        public bool IsSalary
        {
            get => isSalary;
            set
            {
                isSalary = value;
                UpdateView();
            }
        }

        public bool IsCommision
        {
            get => isCommision;
            set
            {
                isCommision = value;
                UpdateView();
            }
        }

        private int empId;
        private bool isHourly;
        private string name;
        private string address;
        private double hourlyRate;
        private double salary;
        private bool isSalary;
        private bool isCommision;
        private double commisionRate;
        private double commisionSalary;

        private void UpdateView()
        {
            view.UpdateSubmitButton(AllInfoCollected());
        }

        public AddEmployeePresenter(AddEmployeeView view, TransactionContainer container, IPayrollDb database)
        {
            this.view = view;
            Container = container;
            db = database;
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                UpdateView();
            }
        }

        public string Address
        {
            get => address;
            set
            {
                address = value;
                UpdateView();
            }
        }

        public double HourlyRate
        {
            get => hourlyRate;
            set
            {
                hourlyRate = value;
                UpdateView();
            }
        }

        public double Salary
        {
            get => salary;
            set
            {
                salary = value;
                UpdateView();
            }
        }

        public double CommisionRate
        {
            get => commisionRate;
            set
            {
                commisionRate = value;
                UpdateView();
            }
        }

        public double CommisionSalary
        {
            get => commisionSalary;
            set
            {
                commisionSalary = value;
                UpdateView();
            }
        }

        public AddEmployeeTransaction CreateTransaction()
        {
            if (!AllInfoCollected())
            {
                throw new InsufficientInformationToAddEmployee();
            }

            if (IsHourly)
            {
                return new AddHourlyEmployeeTransaction(db, EmpId, Name, Address, HourlyRate);
            }

            if (IsSalary)
            {
                return new AddSalaryEmployeeTransaction(db, EmpId, Name, Address, Salary);
            }

            // if (IsCommision)
            {
                return new AddCommissionedEmployeeTransaction(db, EmpId, Name, Address, CommisionSalary, CommisionRate);
            }
            // throw new NotSupportedException("Employee payment not defined. This can logically never happen");
        }

        public bool AllInfoCollected()
        {
            bool result = EmpId > 0;
            result &= Name != null;
            result &= Address != null;
            result &= IsHourly || IsSalary || IsCommision;
            if (IsHourly)
            {
                result &= HourlyRate > 0;
            }

            if (IsSalary)
            {
                result &= Salary > 0;
            }

            if (IsCommision)
            {
                result &= CommisionRate > 0;
                result &= CommisionSalary > 0;
            }

            return result;
        }


        public void AddEmployeeToTransactions()
        {
            Container.Add(CreateTransaction());
        }
    }

    public class InsufficientInformationToAddEmployee : NotSupportedException
    {
    }
}