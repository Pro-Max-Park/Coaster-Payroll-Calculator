using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $safeprojectname$
{
    public abstract class PayCalculator
    {
        protected double hourlyRate;
        protected int hoursWorked;

        public PayCalculator(double hourlyRate, int hoursWorked)
        {
            this.hourlyRate = hourlyRate;
            this.hoursWorked = hoursWorked;
        }

        // Calculate the Gross Pay (Hours Worked * Hourly Rate)
        public decimal calculatePay()
        {
            return (decimal)(hourlyRate * hoursWorked);
        }

        public abstract decimal calculateTax();
        public abstract decimal calculateSuperannuation();
    }
}
