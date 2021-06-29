using System;
using System.Collections;
using System.Linq;

namespace Payroll
{
    public class InMemoryDB : IPayrollDB
    {
        private static Hashtable db = new Hashtable();
        private static Hashtable union = new Hashtable();

        public Employee GetEmployee(int empId)
        {
            if (!db.Contains(empId))
            {
                // throw new EmployeeNotFound();
                return Employee.NULL;
            }
            return db[empId] as Employee;
        }

        public void AddEmployee(int id, Employee employee)
        {
            db.Add(id, employee);
        }

        public void Clear()
        {
            db.Clear();
            union.Clear();
        }

        public void RemoveEmployee(int id)
        {
            if (!db.Contains(id)) throw new EmployeeNotFound();
            db.Remove(id);
        }

        public void AddUnionMember(int memberId, int id)
        {
            if (!db.Contains(id)) throw new EmployeeNotFound();
            Employee e = GetEmployee(id);
            union.Add(memberId, id);
        }

        public Employee GetUnionMember(int memberId)
        {
            if (!union.Contains(memberId))
            {
                throw new UnionMemberNotFound();
            }
            int empId = (int) union[memberId];
            return GetEmployee(empId);
        }

        public void RemoveUnionMember(int memberId)
        {
            if (!union.Contains(memberId))
            {
                throw new UnionMemberNotFound();
            }
            union.Remove(memberId);
        }

        public int[] GetEmployeeIds()
        {
            return db.Keys.Cast<int>().ToArray();
        }
    }

  

    public class UnionMemberNotFound : Exception
    {
    }

    public class EmployeeNotFound : Exception
    {
    }
}