using BankAccounts_DotnetCore3._1version.Interfaces;
using BankAccounts_DotnetCore3._1version.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BankAccounts_DotnetCore3._1version.Respository
{
    public class DepartementRepository : IDepartmentRepository
    {
        string connectionString = "data source=RAYUDU\\SQLEXPRESS;integrated security=yes;Encrypt=True;TrustServerCertificate=True;initial catalog=hotelmanagement";
        public async Task<int> AddDeparment(Departement deptdetail)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("Usp_AddDepartment", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@deptname", deptdetail.deptname);
                cmd.Parameters.AddWithValue("@deptlocation", deptdetail.deptlocation);                
                con.Open();//we must open the connection manualay
                await cmd.ExecuteNonQueryAsync();
                
                con.Close();//we must close the connection.
            }
            return 1;
        }

        public async Task<bool> DeleteDepartmentById(int deptid)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {

                SqlCommand cmd = new SqlCommand("Usp_DeleteDepartment", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@deptid", deptid);
                con.Open();
                await cmd.ExecuteNonQueryAsync();
                con.Close();
            }
            return true;
        }

        public async Task<List<Departement>> GetDepartMentDetails()
        {
            List<Departement> lstdept = new List<Departement>();
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Usp_GetDepartment", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();//will return results of select statement
                    while (reader.Read())
                    {
                        Departement dept = new Departement();
                        dept.deptid = Convert.ToInt32(reader["deptid"]);
                        dept.deptname = Convert.ToString(reader["deptname"]);
                        dept.deptlocation = Convert.ToString(reader["deptlocation"]);
                        lstdept.Add(dept);
                    }
                    con.Close();
                }
                return lstdept;
            }
        }

        public async Task<Departement> GetDepartmentDetailsById(int deptid)
        {
            Departement dept = new Departement();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Usp_GetDepartmentById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@deptid", deptid);
                SqlDataReader dr = await cmd.ExecuteReaderAsync();
                while (dr.Read())
                {
                    dept.deptid = Convert.ToInt32(dr["deptid"]);
                    dept.deptname = Convert.ToString(dr["deptname"]);
                    dept.deptlocation = Convert.ToString(dr["deptlocation"]);
                }
            }
            return dept;
        }

        public async Task<bool> UpdateDepartment(Departement deptdetail)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("Usp_UpdateDepartment", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@deptname", deptdetail.deptname);
                cmd.Parameters.AddWithValue("@deptlocation", deptdetail.deptlocation);
                con.Open();//we must open the connection manualay
                await cmd.ExecuteNonQueryAsync();

                con.Close();//we must close the connection.
            }
            return true;
        }
    }
}
