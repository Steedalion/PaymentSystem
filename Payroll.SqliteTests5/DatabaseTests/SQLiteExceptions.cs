using InMemoryDBTest;
using NUnit.Framework;
using PayrollDataBase;

namespace DatabaseTests.DatabaseTests
{
    public class SQLiteExceptions : PayrollExceptionsTest
    {
        [SetUp]
        public void Setup()
        {
            database = new SqliteDB();
        }
    }
}