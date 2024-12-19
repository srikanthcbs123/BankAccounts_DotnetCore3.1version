using BankAccounts_DotnetCore3._1version.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;

namespace BankAccounts_DotnetCore3._1version.DbConnect
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly IConfiguration _config;
        public ConnectionFactory(IConfiguration config)
        {
            _config = config;
        }
        public SqlConnection hotelmanagementsqlConnectionString_ForAdonet()
        {
            var connStr = Convert.ToString(_config.GetSection("ConnectionStrings:hotelmanagementsqlConnectionString").Value);
            //Creates an SqlConnection Object to store the sqlconnection.
            SqlConnection con = new SqlConnection(connStr);
            return con;
        }
        public IDbConnection hotelmanagementsqlConnectionString()
        {
            var connStr = Convert.ToString(_config.GetSection("ConnectionStrings:MidLandSqlConnectionString").Value);
            //Creates an IDbConnection Object to store the sqlconnection.
            IDbConnection con = new SqlConnection(connStr);
            return con;
        }

        public IDbConnection Northwind_DBSqlConnectionString()
        {
            var connStr = Convert.ToString(_config.GetSection("ConnectionStrings:Northwind_DBSqlConnectionString").Value);
            // Creates an IDbConnection Object to store the sqlconnection.
            IDbConnection _connection = new SqlConnection(connStr);
            return _connection;
        }
    }
}
