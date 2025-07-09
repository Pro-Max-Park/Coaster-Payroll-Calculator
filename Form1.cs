using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Globalization;

namespace $safeprojectname$
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Add code below to complete the implementation to populate the listBox
            // by reading the employee.csv file into a List of PaySlip objects, then binding this to the ListBox.
            // CSV file format: <employee ID>, <first name>, <last name>, <hourly rate>,<taxthreshold>
            LoadTextFileToListbox();
            
        }

        /// <summary>
        /// Loads employee data from a CSV file and populates the ListBox with employee details.
        /// </summary>
        private void LoadTextFileToListbox()
        {
            string inputFilePath = "employee.csv";
            List<Employees> emp = new List<Employees>();

            string[] EmpCsv = File.ReadAllLines(inputFilePath);

            foreach (string line in EmpCsv)
            {
                string[] part = line.Split(',');
                Employees employee = new Employees()
                {

                    Id = int.Parse(part[0]),
                    FirstName = part[1],
                    LastName = part[2],
                    HourlyRate = double.Parse(part[3]),
                    TaxThreshold = part[4]
                };
                emp.Add(employee);
            }
            listBox1.DataSource = emp;
            listBox1.DisplayMember = "FullDetail";
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            //Add code below to complete the implementation to populate the
            //payment summary(textBox2) using the PaySlip and PayCalculatorNoThreshold
            //and PayCalculatorWithThresholds classes object and methods.

            // Check if an item is selected in the listbox
            Employees selectedEmp = listBox1.SelectedItem as Employees;
            if (selectedEmp != null)
            {
                // Get the number of hours worked from textBox1
                int hoursWorked = 0;

                // Try to parse the value from textBox1
                if (int.TryParse(textBox1.Text, out hoursWorked) && hoursWorked >= 0)
                {
                    decimal taxRate = selectedEmp.TaxThreshold == "Y" ? 0.1m : 0.0m;

                    // Create a new PaySlip for the selected employee
                    PaySlip paySlip = new PaySlip(selectedEmp, hoursWorked, taxRate);

                    // Populate the textBox2 with the payment summary
                    string temp = "";
                    temp += $"ID: {paySlip.Id}\r\n";
                    temp += $"First name: {paySlip.FirstName}\r\n";
                    temp += $"last name: {paySlip.LastName}\r\n";
                    temp += $"Hours Worked: {paySlip.HoursWorked}\r\n";
                    temp += $"Hourly Rate: {paySlip.HourlyRate:C}\r\n";  // Format as currency
                    temp += $"Tax Threshold: {paySlip.TaxThreshold}\r\n";
                    temp += $"Gross Pay: {paySlip.GrossPay:C}\r\n";  // Format as currency
                    temp += $"Tax: {paySlip.Tax:C}\r\n";  // Format as currency
                    temp += $"Net Pay: {paySlip.NetPay:C}\r\n";  // Format as currency
                    temp += $"Superannuation: {paySlip.Superannuation:C}\r\n";  // Format as currency

                    // Display the payment summary in textBox2
                    textBox2.Text = temp;
                }
                else
                {
                    MessageBox.Show("Please enter a valid number of hours worked.");
                }
            }
            else
            {
                MessageBox.Show("Please select an employee from the list.");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Add code below to complete the implementation for saving the
            // calculated payment data into a csv file.
            // File naming convention: Pay_<full name>_<datetimenow>.csv
            // Data fields expected - EmployeeId, Full Name, Hours Worked, Hourly Rate, Tax Threshold, Gross Pay, Tax, Net Pay, Superannuation

            // Call the method to save the payment record to the CSV file
            CSVWriterRecordToCSVFile();
        }

        private void CSVWriterRecordToCSVFile()
        {
            // Ensure an employee is selected
            Employees selectedEmployee = listBox1.SelectedItem as Employees;
            if (selectedEmployee != null)
            {
                // Get hours worked from the textBox1 input
                if (int.TryParse(textBox1.Text,out int hoursWorked) && hoursWorked >= 0)
                {
                    // Determine the tax rate based on the employee's tax threshold
                    decimal taxRate = selectedEmployee.TaxThreshold == "Y" ? 0.1m : 0.0m;

                    // Create a PaySlip instance for the selected employee
                    PaySlip paySlip = new PaySlip(selectedEmployee, hoursWorked, taxRate);

                    // Construct full name for employee
                    string fullName = $"{selectedEmployee.FirstName} {selectedEmployee.LastName}";

                    // Prepare payment record data to be saved
                    var paymentRecord = new PaymentRecord
                    {
                        EmployeeId = paySlip.Id,
                        FullName = fullName,
                        HoursWorked = paySlip.HoursWorked,
                        HourlyRate = paySlip.HourlyRate,
                        TaxThreshold  = paySlip.TaxThreshold,
                        GrossPay = paySlip.GrossPay,
                        Tax = paySlip.Tax,
                        NetPay = paySlip.NetPay,
                        Superannuation = paySlip.Superannuation
                    };
                    
                    //string outputFilePath = Path.Combine(Environment.CurrentDirectory, "output.csv");

                    // Define the file name based on the employee's ID, full name, and current date/time
                    string fileName = $"Pay-{selectedEmployee.Id}-{fullName.Replace(" ", " ")}-{DateTime.Now:yyyy-MM-dd HH mm ss}.csv";
                    string outputFilePath = Path.Combine(Environment.CurrentDirectory, fileName);

                    // Use CsvHelper to write the payment record to the file
                    CsvConfiguration csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        ShouldQuote = _ => true // Ensure proper quoting of fields that may contain commas
                    };

                    // Open the StreamWriter and CsvWriter to write data to the individual file
                    using (var writer = new StreamWriter(outputFilePath)) 
                    using (var csv = new CsvWriter(writer, csvConfig))
                    {
                        // Write the header if needed
                        csv.WriteHeader<PaymentRecord>();
                        csv.NextRecord(); // Ensure the header is written

                        //Write the payment record to the CSV file
                        csv.WriteRecord(paymentRecord); // Directly write the values
                        csv.NextRecord();  // Move to the next line for the next record
                    }

                    MessageBox.Show($"Payment record saved successfully to {fileName}");
                }
                else
                {
                    MessageBox.Show("Please enter a valid number of hours worked.");
                }
            }
            else
            {
                MessageBox.Show("Please select an employee from the list.");
            }
        }
    }
}
