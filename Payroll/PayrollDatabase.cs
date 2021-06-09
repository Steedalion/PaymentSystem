using System;
using System.Collections;

namespace Payroll
{
    public static class PayrollDatabase
    {
        private static Hashtable db = new Hashtable();
        private static Hashtable union = new Hashtable();

        public static Employee GetEmployee(int empId)
        {
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
            db.Remove(id);
        }

        public static void AddUnionMember(int memberId, int id)
        {
            if (!db.Contains(id)) throw new EmployeeNotFound();
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
    }

    public class UnionMemberNotFound : Exception
    {
    }

    public class EmployeeNotFound : Exception
    {
    }
}