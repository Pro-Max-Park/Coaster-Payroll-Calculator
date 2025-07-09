using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $safeprojectname$
{
    /// <summary>
    /// Represents the payslip details for an employee.
    /// </summary>
    public class PaySlip
    {
        /// <summary>
        /// Gets or sets the employee ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the first name of the employee.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the employee.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the hours worked by the employee.
        /// </summary>
        public int HoursWorked { get; set; }

        /// <summary>
        /// Gets or sets the hourly rate of the employee.
        /// </summary>
        public double HourlyRate { get; set; }

        /// <summary>
        /// Gets or sets the tax threshold status of the employee.
        /// </summary>
        public string TaxThreshold { get; set; }

        /// <summary>
        /// Gets or sets the gross pay for the employee.
        /// </summary>
        public decimal GrossPay { get; set; }

        /// <summary>
        /// Gets or sets the tax applied to the employee's pay.
        /// </summary>
        public decimal Tax { get; set; }

        /// <summary>
        /// Gets or sets the net pay after tax deduction.
        /// </summary>
        public decimal NetPay { get; set; }

        /// <summary>
        /// Gets or sets the superannuation of the employee.
        /// </summary>
        public decimal Superannuation { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="employee">employee</param>
        /// <param name="hoursWorked">hoursWorked</param>
        /// <param name="taxRate">taxRate</param>
        // Constructor
        public PaySlip(Employees employee, int hoursWorked, decimal taxRate)
        {
            Id = employee.Id;
            FirstName = employee.FirstName;
            LastName = employee.LastName;
            HoursWorked = hoursWorked;
            HourlyRate = employee.HourlyRate;
            TaxThreshold = employee.TaxThreshold;

            // Use a base class reference and conditionally create the correct subclass
            PayCalculator calculator;

            if (TaxThreshold == "Y")
            {
                calculator = new PayCalculatorWithThreshold(HourlyRate, HoursWorked);
            }
            else
            {
                calculator = new PayCalculatorNoThreshold(HourlyRate, HoursWorked);
            }

            GrossPay = calculator.calculatePay();
            Tax = calculator.calculateTax();
            Superannuation = calculator.calculateSuperannuation();
            NetPay = GrossPay - Tax;
        }
    }
}
