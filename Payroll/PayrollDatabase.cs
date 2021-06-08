using System.Collections;

namespace Payroll
{
    public static class PayrollDatabase
    {
        private static Hashtable db = new Hashtable();

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
        }

        public static void RemoveEmployee(int id)
        {
            db.Remove(id);
        }
    }
}