using Medibuddy.Models;
using System.Data;
using System.Data.Common;
using SqlDataReader = System.Data.Common.DbDataReader;

namespace Medibuddy.DataAccess
{
    public class IPDMedicineDataAccess : IIPDMedicineDataAccess
    {

        private readonly IDbConnectionFactory _connectionFactory;
        private DbConnection connection;
        private DbCommand command;

        public IPDMedicineDataAccess(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            connection = _connectionFactory.CreateConnection();
            command = connection.CreateCommand();
        }

        public async Task<IPDMedicine> Create(IPDMedicine ipdmedicine)
        {
            connection.Open();
            command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = $"Insert into {nameof(IPDMedicine)}({nameof(IPDMedicine.IPDPatientID)},{nameof(IPDMedicine.MedicineID)})" +
                                      $" Values({ipdmedicine.IPDPatientID},{ipdmedicine.MedicineID})";
            await command.ExecuteNonQueryAsync();
            connection.Close();
            return ipdmedicine;
        }

        public async Task<bool> Delete(int IPDPatientID)
        {
            connection.Open();
            command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = $"Delete from {nameof(IPDMedicine)} where {nameof(IPDMedicine.IPDPatientID)} = {IPDPatientID}";
            await command.ExecuteNonQueryAsync();
            connection.Close();
            return true;
        }

        public async Task<IEnumerable<IPDMedicine>> Get()
        {
            List<IPDMedicine> ipdmedicines = new List<IPDMedicine>();

            connection.Open();
            command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = $"Select {nameof(IPDMedicine.IPDPatientID)},{nameof(IPDMedicine.MedicineID)}" +
                                  $" from {nameof(IPDMedicine)}";

            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (reader.Read())
            {
                ipdmedicines.Add(new IPDMedicine
                {
                    IPDPatientID = Convert.ToInt32(reader.GetValue(nameof(IPDMedicine.IPDPatientID))),
                    MedicineID = Convert.ToInt32(reader.GetValue(nameof(IPDMedicine.MedicineID)))

                });
            }
            reader.Close();
            reader.Dispose();
            connection.Close();
            return ipdmedicines;
        }


        public async Task<IEnumerable<IPDMedicine>> Get(int IPDPatientID)
        {
            List<IPDMedicine> ipdmedicines = new List<IPDMedicine>();

            connection.Open();
            command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = $"Select {nameof(IPDMedicine.IPDPatientID)},{nameof(IPDMedicine.MedicineID)} from {nameof(IPDMedicine)} where {nameof(IPDMedicine.IPDPatientID)} = {IPDPatientID}";

            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (reader.Read())
            {
                ipdmedicines.Add(new IPDMedicine
                {
                    IPDPatientID = Convert.ToInt32(reader.GetValue(nameof(IPDMedicine.IPDPatientID))),
                    MedicineID = Convert.ToInt32(reader.GetValue(nameof(IPDMedicine.MedicineID)))

                });
            }
            reader.Close();
            reader.Dispose();
            connection.Close();
            return ipdmedicines;
        }
    }
}
