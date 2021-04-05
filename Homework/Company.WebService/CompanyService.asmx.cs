using Company.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Company.WebService
{
    /// <summary>
    /// Summary description for CompanyService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(true)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CompanyService : System.Web.Services.WebService
    {
        string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CompanyConnectionString"].ConnectionString;

        [WebMethod]
        public int AddEmployee(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sqlQuery = $@"INSERT INTO Employees (UserID, Name, Surname, Patronymic, Trainee, Department, Country)
                    VALUES ({employee.ID}, '{employee.Name}', '{employee.Surname}', '{employee.Patronymic}', '{employee.Trainee}', '{employee.CurDep[0]}', '{employee.CurDep[1]}')";

                var command = new SqlCommand(sqlQuery, connection);
                return command.ExecuteNonQuery();
            }
        }

        [WebMethod]
        public int AddDepartment(Department department)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sqlQuery = $@"INSERT INTO Departments (DepartmentName, Country)
                    VALUES ('{department.Dep}', '{department.Country}')";

                var command = new SqlCommand(sqlQuery, connection);
                return command.ExecuteNonQuery();
            }
        }

        [WebMethod]
        public List<Employee> LoadEmployees()
        {
            List<Employee> employees = new List<Employee>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sqlQuery = $@"SELECT * FROM Employees";

                var command = new SqlCommand(sqlQuery, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var employee = new Employee();

                            employee.ID = (int)reader.GetValue(0);
                            employee.Name = reader["Name"].ToString();
                            employee.Surname = reader["Surname"].ToString();
                            employee.Patronymic = reader["Patronymic"].ToString();
                            employee.Trainee = reader.GetBoolean(4);
                            employee.CurDep[0] = reader["Department"].ToString();
                            employee.CurDep[1] = reader["Country"].ToString();

                            employees.Add(employee);
                        }
                    }
                    return employees;
                }
            }
        }

        [WebMethod]
        public List<Department> LoadDepartments()
        {
            List<Department> departments = new List<Department>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sqlQuery = $@"SELECT * FROM Departments";

                var command = new SqlCommand(sqlQuery, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var department = new Department();

                        department.Dep = reader["DepartmentName"].ToString();
                        department.Country = reader["Country"].ToString();

                        departments.Add(department);
                    }
                    return departments;
                }
            }
        }

        [WebMethod]
        public int Remove(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sqlQuery = $@"DELETE FROM Employees WHERE UserID = '{employee.ID}'";
                var command = new SqlCommand(sqlQuery, connection);
                return command.ExecuteNonQuery();
            }
        }

        [WebMethod]
        public int Update(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sqlQuery = $@"UPDATE Employees SET 
                    Name = '{employee.Name}', Surname = '{employee.Surname}', Patronymic = '{employee.Patronymic}', Trainee = '{employee.Trainee}', 
                    Department = '{employee.CurDep[0]}', Country = '{employee.CurDep[1]}'
                    WHERE UserID = '{employee.ID}'";

                var command = new SqlCommand(sqlQuery, connection);
                return command.ExecuteNonQuery();
            }
        }
    }
}
