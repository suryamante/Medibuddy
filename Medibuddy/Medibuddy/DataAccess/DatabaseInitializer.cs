using System.Data.Common;

namespace Medibuddy.DataAccess
{
    /// <summary>
    /// Creates the schema when running against SQLite so the in-memory test database
    /// has all tables. No-op for SQL Server, which is provisioned by the SQL scripts.
    /// Table and column names match the model names used by the DataAccess queries.
    /// </summary>
    public static class DatabaseInitializer
    {
        public static void Initialize(IDbConnectionFactory connectionFactory)
        {
            if (!connectionFactory.IsSqlite)
            {
                return;
            }

            using DbConnection connection = connectionFactory.CreateConnection();
            connection.Open();
            using DbCommand command = connection.CreateCommand();
            command.CommandText = Schema;
            command.ExecuteNonQuery();
        }

        private const string Schema = @"
CREATE TABLE IF NOT EXISTS Patient (
    PID INTEGER PRIMARY KEY AUTOINCREMENT,
    FirstName TEXT, MidName TEXT, LastName TEXT, Mobile TEXT,
    Email TEXT, Address TEXT, Gender TEXT, DOB TEXT);

CREATE TABLE IF NOT EXISTS Department (
    DepID INTEGER PRIMARY KEY AUTOINCREMENT,
    DepName TEXT);

CREATE TABLE IF NOT EXISTS Doctor (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT, Type TEXT, Mobile TEXT, Email TEXT,
    Gender TEXT, Fees REAL, Salary REAL);

CREATE TABLE IF NOT EXISTS Nurse (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT, Mobile TEXT, Email TEXT, Gender TEXT, Salary REAL);

CREATE TABLE IF NOT EXISTS Test (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT, Price INTEGER);

CREATE TABLE IF NOT EXISTS Medicine (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT, Price INTEGER);

CREATE TABLE IF NOT EXISTS Ward (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    DepId INTEGER, RoomSpecialCapacity INTEGER,
    RoomSharedCapacity INTEGER, RoomGeneralCapacity INTEGER);

CREATE TABLE IF NOT EXISTS Room (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    WardId INTEGER, Type TEXT, Rate REAL,
    CurrentBedCapacity INTEGER, MaxBedCapacity INTEGER);

CREATE TABLE IF NOT EXISTS OPDBilling (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    PID INTEGER, DocId INTEGER);

CREATE TABLE IF NOT EXISTS OPDTest (
    OPDBillingID INTEGER, TestID INTEGER);

CREATE TABLE IF NOT EXISTS OPDMedicine (
    OPDBillingID INTEGER, MedicineID INTEGER);

CREATE TABLE IF NOT EXISTS OPDPatient (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    PID INTEGER, DocId INTEGER, VisitDate TEXT,
    OPDBillingID INTEGER, Discharged TEXT);

CREATE TABLE IF NOT EXISTS IPDPatient (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    PID INTEGER, DocId INTEGER, NurseID INTEGER,
    EntryDate TEXT, ExitDate TEXT, RoomID INTEGER, Discharged TEXT);

CREATE TABLE IF NOT EXISTS IPDTest (
    IPDPatientID INTEGER, TestID INTEGER);

CREATE TABLE IF NOT EXISTS IPDMedicine (
    IPDPatientID INTEGER, MedicineID INTEGER);
";
    }
}
