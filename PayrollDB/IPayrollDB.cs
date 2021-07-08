
using Payroll;

public interface IPayrollDB
    {
        Employee GetEmployee(int empId);
        void AddEmployee(int id, Employee employee);
        void Clear();
        void RemoveEmployee(int id);
        void AddUnionMember(int memberId, int id);
        Employee GetUnionMember(int memberId);
        void RemoveUnionMember(int memberId);
        int[] GetEmployeeIds();
    }