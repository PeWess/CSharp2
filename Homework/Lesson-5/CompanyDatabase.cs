using Company.Controls;
using Company.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_5
{
    public class CompanyDatabase
    {
        public const string ConnectionString = "Data Source=localhost\\MSSQLSERVER01;Initial Catalog=Company;User ID=Company Root;Password=12345";

        Random rnd = new Random();
        string filename = AppDomain.CurrentDomain.BaseDirectory + "Names.txt";


        public List<string> departments = new List<string>{
        "Information Security",
        "IT Support",
        "IT Development",
        "Software Development",
        "Quality Control"};

        public List<string> countries = new List<string>{
        "Россия",
        "Германия",
        "Швеция",
        "Китай",
        "США"};

        public ObservableCollection<Employee> Employees { get; set; }
        public ObservableCollection<Department> Departments { get; set; }
        public List<int> Identifiers { get; set; }

        public CompanyDatabase()
        {
            Employees = new ObservableCollection<Employee>();
            Departments = new ObservableCollection<Department>();
            Identifiers = new List<int>();
            LoadFromDatabase();
        }

        public int AddEmployee(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sqlQuery = $@"INSERT INTO Employees (UserID, Name, Surname, Patronymic, Trainee, Department, Country)
                    VALUES ({employee.ID}, '{employee.Name}', '{employee.Surname}', '{employee.Patronymic}', '{employee.Trainee}', '{employee.CurDep[0]}', '{employee.CurDep[1]}')";

                var command = new SqlCommand(sqlQuery, connection);
                var result = command.ExecuteNonQuery();
                if (result > 0)
                {
                    Employees.Add(employee);
                }
                return result;
            }
        }

        public int AddDepartment(Department department)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sqlQuery = $@"INSERT INTO Departments (DepartmentName, Country)
                    VALUES ('{department.Dep}', '{department.Country}')";

                var command = new SqlCommand(sqlQuery, connection);
                var result = command.ExecuteNonQuery();
                if (result > 0)
                {
                    Departments.Add(department);
                }
                return result;
            }
        }

        public void LoadFromDatabase()
        {
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

                            Employees.Add(employee);
                        }
                    }
                }

                sqlQuery = $@"SELECT * FROM Departments";

                command = new SqlCommand(sqlQuery, connection);
                using (SqlDataReader reader1 = command.ExecuteReader())
                {
                    while (reader1.Read())
                    {
                        var department = new Department();

                        department.Dep = reader1["DepartmentName"].ToString();
                        department.Country = reader1["Country"].ToString();

                        Departments.Add(department);
                        WorkerControl.DepartmentsList.Add($"{department.Dep} - {department.Country}");
                    }
                }
            }
        }

        public int Remove(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sqlQuery = $@"DELETE FROM Employees WHERE UserID = '{employee.ID}'";
                var command = new SqlCommand(sqlQuery, connection);
                var result = command.ExecuteNonQuery();
                if (result > 0)
                {
                    Employees.Remove(employee);
                }
                return result;
            }
        }

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

        public void GenerateCompanyList()
        {
            StreamReader sr = new StreamReader(filename);

            for (int i = 0; i < 5; i++)
            {
                string department = departments[i];
                string country = countries[i];
                Departments.Add(new Department(department, country));
            }

            while (!sr.EndOfStream)
            {
                int id = rnd.Next(1000, 10000);
                while (Identifiers.Contains(id) == true)
                {
                    id = rnd.Next(1000, 10000);
                }
                Identifiers.Add(id);
                string[] FIO = sr.ReadLine().Split(' ');
                string surname = FIO[0];
                string name = FIO[1];
                string patronymic = FIO[2];
                bool trainee = rnd.Next(0, 2) == 0 ? false : true;
                Department department = Departments[rnd.Next(0, Departments.Count)];
                Employees.Add(new Employee(id, name, surname, patronymic, trainee, department));
            }
            sr.Close();
        }
    }
}
