using Employee_MVVM_WCF.Commands;
using Employee_MVVM_WCF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_MVVM_WCF.ViewModel
{
    class EmployeeViewModel: INotifyPropertyChanged
    {
        #region event
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
        #endregion

        #region Properties
        //Message
        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged("Message"); }
        }

        private Employee newEmployee;
        public Employee NewEmployee
        {
            get { return newEmployee; }
            set { newEmployee = value; OnPropertyChanged("NewEmployee"); }
        }
        #endregion

        EmployeeService _db;
        public EmployeeViewModel()
        {
            _db = new EmployeeService();
            LoadData();
            NewEmployee = new Employee();
            saveCommand = new RelayCommand(SaveNewEmployee);
            searchCommand = new RelayCommand(SearchEmployee);
            updateCommand = new RelayCommand(UpdateEmployee);
            deleteCommand = new RelayCommand(DeleteEmployee);
        }

        #region GetAllEmployee
        private ObservableCollection<Employee> employeesList;
        public ObservableCollection<Employee> EmployeesList
        {
            get { return employeesList; }
            set { employeesList = value; OnPropertyChanged("EmployeesList"); }
        }
        private void LoadData()
        {
            EmployeesList = new ObservableCollection<Employee>(_db.GetEmployees());
        }
        #endregion

        #region SaveNewEmployee
        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get { return saveCommand; }
        }
        public void SaveNewEmployee()
        {
            var isSaved = _db.AddEmployee(newEmployee);
            LoadData();
            if (isSaved)
                Message = "Employee Added Successfully...!!!";
            else
                Message = "Error...!!! Try Again Later";
        }
        #endregion

        #region FindEmployee
        private RelayCommand searchCommand;
        public RelayCommand SearchCommand
        {
            get { return searchCommand; }
        }

        public void SearchEmployee()
        {
            var emp = _db.FindEmployee(newEmployee.Id);
            if (emp != null)
            {
                Message = "";
                newEmployee.Name = emp.Name;
                newEmployee.Age = emp.Age;
            }
            else
            {
                Message = "Employee Not Found";
                newEmployee.Name = "";
                newEmployee.Age = 0;
            }
                
        }
        #endregion

        #region UpdateEmployee
        private RelayCommand updateCommand;
        public RelayCommand UpdateCommand
        {
            get { return updateCommand; }
        }

        public void UpdateEmployee()
        {
            var isExecuted = _db.UpdateEmployee(newEmployee);
            if (isExecuted)
            {
               Message = "Employee Data Updated";
                LoadData();
            }
            else
                Message = "Failed to Update";
        }
        #endregion

        #region DeleteEmployee
        private RelayCommand deleteCommand;

        public RelayCommand DeleteCommand
        {
            get { return deleteCommand; }
        }

        public void DeleteEmployee()
        {
            var data = _db.RemoveEmployee(newEmployee.Id);
            if (data)
            {
                Message = "Employee Removed Successfully";
                LoadData();
            }
            else
                Message = "Failed to Remove";
        }
        #endregion



    }
}
