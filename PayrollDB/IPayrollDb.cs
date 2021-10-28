
using PayrollDomain;

namespace PayrollDB
{
    public interface IPayrollDb
    {
        Employee GetEmployee(int empId);
        void AddEmployee(int id, Employee employee);
        void Clear();
        void RemoveEmployee(int id);
        void AddUnionMember(int memberId, int empID);
        Employee GetUnionMember(int memberId);
        void RemoveUnionMember(int memberId);
        int[] GetEmployeeIds();
    }
}