using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_MVVM_WCF.Models
{
    class EmployeeService
    {
        string constring = @"Data Source=DESKTOP-CHPF16F;Initial Catalog=Employee;Integrated Security=True";
        public List<Employee> GetEmployees()
        {
            List<Employee> EmployeeList = new List<Employee>();
            string sql = "select * from tblEmployee";
            SqlConnection con = new SqlConnection(constring);
            SqlDataAdapter sqlData = new SqlDataAdapter(sql,con);
            con.Open();
            DataTable dataTable = new DataTable();
            sqlData.Fill(dataTable);
            con.Close();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Employee emp = new Employee 
                { 
                    Id = Convert.ToInt32(dataTable.Rows[i]["Id"]), 
                    Name = dataTable.Rows[i]["Name"].ToString(), 
                    Age= Convert.ToInt32(dataTable.Rows[i]["Age"]) 
                };
                EmployeeList.Add(emp);
            }
            return EmployeeList;
        }

        public bool AddEmployee(Employee employee)
        {
            bool isExecuted = false;
            SqlConnection con = new SqlConnection(constring);
            try
            {
                string sql = "INSERT INTO tblEmployee(Name,Age) values(@Name,@Age)";
                SqlCommand command = new SqlCommand(sql, con);
                command.Parameters.AddWithValue("@Name", employee.Name);
                command.Parameters.AddWithValue("@Age", employee.Age);
                con.Open();
                int rows = command.ExecuteNonQuery();
                if (rows > 0)
                    isExecuted = true;
            }
            finally
            {
                con.Close();
            }
            return isExecuted;
        }

        public bool UpdateEmployee(Employee employee)
        {
            bool isExecuted = false;
            SqlConnection con = new SqlConnection(constring);
            try
            {
                string sql = "Update tblEmployee set Name=@Name,Age=@Age where Id=@Id";
                SqlCommand command = new SqlCommand(sql, con);
                command.Parameters.AddWithValue("@Name", employee.Name);
                command.Parameters.AddWithValue("@Age", employee.Age);
                command.Parameters.AddWithValue("@Id", employee.Id);
                con.Open();
                int rows = command.ExecuteNonQuery();
                if (rows > 0)
                    isExecuted = true;
            }
            finally
            {
                con.Close();
            }
            return isExecuted;
        }

        public bool RemoveEmployee(int id)
        {
            bool isExecuted = false;
            SqlConnection con = new SqlConnection(constring);
            try
            {
                string sql = "delete from tblEmployee where Id=@Id";
                SqlCommand command = new SqlCommand(sql, con);
                command.Parameters.AddWithValue("@Id", id);
                con.Open();
                int rows = command.ExecuteNonQuery();
                if (rows > 0)
                    isExecuted = true;
            }
            finally
            {
                con.Close();
            }
            return isExecuted;
        }

        public Employee FindEmployee(int id)
        {
            string sql = "select * from tblEmployee where Id="+id;
            SqlConnection con = new SqlConnection(constring);
            SqlDataAdapter sqlData = new SqlDataAdapter(sql, con);
            con.Open();
            DataTable dataTable = new DataTable();
            sqlData.Fill(dataTable);
            con.Close();
            if (dataTable.Rows.Count > 0)
            {
                Employee emp = new Employee
                {
                    Id = Convert.ToInt32(dataTable.Rows[0]["Id"]),
                    Name = dataTable.Rows[0]["Name"].ToString(),
                    Age = Convert.ToInt32(dataTable.Rows[0]["Age"])
                };
                return emp;
            }
            else
                return null;
        }
    }
}
