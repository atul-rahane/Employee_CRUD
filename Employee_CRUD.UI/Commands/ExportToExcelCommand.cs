using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Employee_CRUD.Domain.Models;
using Employee_CRUD.UI.Stores;
using ClosedXML.Excel;

namespace Employee_CRUD.UI.Commands
{
    public class ExportToExcelCommand : CommandBase
    {
        private readonly EmployeesStore _employeesStore;
        
        public ExportToExcelCommand(EmployeesStore employeesStore)
        {
            _employeesStore = employeesStore;
        }

        public override void Execute(object parameter)
        {
            List<Employee> employees = _employeesStore.Employees.ToList();

            using (var wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("EmployeeData");
                ws.Cell(1, 1).InsertData(employees);

                string fileName = $"c:\\employees{DateTime.Now:yyyy-dd-M-HH-mm-ss}.csv";

                System.IO.File.WriteAllLines(fileName,
                ws.RowsUsed().Select(row =>
                    string.Join(", ", row.Cells(1, row.LastCellUsed(false).Address.ColumnNumber)
                        .Select(cell => cell.GetValue<string>()))
                ));

                MessageBox.Show($"Data exported to file {fileName} successfully.");
            }

            
        }
    }
}
