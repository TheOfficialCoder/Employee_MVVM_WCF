using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_MVVM_WCF.Models
{
    class EmployeeService
    {
        public List<Employee> EmployeeList;

        public EmployeeService()
        {
            EmployeeList = new List<Employee>
            {
                new Employee{Id=1,Name="Himanshu",Age=24},
                new Employee{Id=2,Name="Kamli",Age=28}
            };
        }

        public List<Employee> GetEmployees()
        {
            return EmployeeList;
        }

        public bool AddEmployee(Employee employee)
        {
            EmployeeList.Add(employee);
            return true;
        }

        public bool UpdateEmployee(Employee employee)
        {
            var data = EmployeeList.SingleOrDefault(m => m.Id == employee.Id);
            if (data == null)
                return false;
            data.Name = employee.Name;
            data.Age = employee.Age;
            return true;
        }

        public bool RemoveEmployee(int id)
        {
            var data = EmployeeList.SingleOrDefault(m => m.Id == id);
            if (data == null)
                return false;
            EmployeeList.Remove(data);
            return true;
        }

        public Employee FindEmployee(int id)
        {
            var data = EmployeeList.SingleOrDefault(m => m.Id == id);
            if (data == null)
                return null;
            return data;
        }
    }
}
