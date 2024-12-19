using Microsoft.Data.SqlClient;
using System.Data;

namespace BankAccounts_DotnetCore3._1version.Interfaces
{
    public interface IConnectionFactory
    {
        SqlConnection hotelmanagementsqlConnectionString_ForAdonet();
        IDbConnection hotelmanagementsqlConnectionString();
        IDbConnection Northwind_DBSqlConnectionString();
    }
}
