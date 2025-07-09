using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $safeprojectname$
{
    public class Employees
    {
        /// <summary>
        /// Gets or sets the Employee ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or set the first name of the employee.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the last name of the employee.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the hourly rate of the employee.
        /// </summary>
        public double HourlyRate { get; set; } = 0;

        /// <summary>
        /// Gets or sets the tax threshold status of the employee (Y/N).
        /// </summary>
        public string TaxThreshold { get; set; } = string.Empty;

        /// <summary>
        /// Gets the full detail of the employee in the format: "ID. First Name, Last Name".
        /// </summary>
        public string FullDetail => $"{Id}. {FirstName}, {LastName}";
    }
}
