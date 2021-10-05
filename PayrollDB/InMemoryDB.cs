using System;
using System.Collections;
using System.Linq;
using PayrollDomain;

namespace PayrollDB
{
    public class InMemoryDB : IPayrollDb
    {
        private static Hashtable db = new Hashtable();
        private static Hashtable union = new Hashtable();

        public Employee GetEmployee(int empId)
        {
            if (!db.Contains(empId))
            {
                return Employee.NULL;
            }

            return db[empId] as Employee;
        }

        public void AddEmployee(int id, Employee employee)
        {
            try
            {
                db.Add(id, employee);
            }
            catch (ArgumentException e)
            {
                throw new EmployeeIdAlreadyExists();
            }
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
}