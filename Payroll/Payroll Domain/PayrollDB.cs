using System;
using System.Collections;

namespace Payroll
{
    public static class PayrollDB
    {
        private static Hashtable db = new Hashtable();
        private static Hashtable union = new Hashtable();

        public static Employee GetEmployee(int empId)
        {
            if (!db.Contains(empId))
            {
                // throw new EmployeeNotFound();
                return Employee.NULL;
            }
            return db[empId] as Employee;
        }

        public static void AddEmployee(int id, Employee employee)
        {
            db.Add(id, employee);
        }

        public static void Clear()
        {
            db.Clear();
            union.Clear();
        }

        public static void RemoveEmployee(int id)
        {
            if (!db.Contains(id)) throw new EmployeeNotFound();
            db.Remove(id);
        }

        public static void AddUnionMember(int memberId, int id)
        {
            if (!db.Contains(id)) throw new EmployeeNotFound();
            Employee e = GetEmployee(id);
            union.Add(memberId, id);
        }

        public static Employee GetUnionMember(int memberId)
        {
            if (!union.Contains(memberId))
            {
                throw new UnionMemberNotFound();
            }
            int empId = (int) union[memberId];
            return GetEmployee(empId);
        }

        public static void RemoveUnionMember(int memberId)
        {
            if (!union.Contains(memberId))
            {
                throw new UnionMemberNotFound();
            }
            union.Remove(memberId);
        }
    }

  

    public class UnionMemberNotFound : Exception
    {
    }

    public class EmployeeNotFound : Exception
    {
    }
}