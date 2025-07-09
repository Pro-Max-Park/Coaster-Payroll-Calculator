using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace $safeprojectname$
{
    public class PayCalculatorNoThreshold : PayCalculator
    {
        private List<TaxBracket> taxBrackets;

        public PayCalculatorNoThreshold(double hourlyRate, int hoursWorked)
            : base(hourlyRate, hoursWorked)
        {
            taxBrackets = TaxRateLoader.LoadTaxBrackets("taxrate-nothreshold.csv");
        }

        public override decimal calculateTax()
        {
            decimal grossPay = calculatePay();
            return CalculateTax(grossPay);
        }

        private decimal CalculateTax(decimal grossPay)
        {

            foreach (var bracket in taxBrackets)
            {
                if (grossPay > bracket.Min && grossPay <= bracket.Max)
                {
                    return (grossPay * bracket.Rate) - bracket.BaseTax;
                }
            }

            return 0; // Return 0 if no tax bracket matched
        }

        public override decimal calculateSuperannuation()
        {
            // Example superannuation calculation: 11.5% of gross pay
            return calculatePay() * 0.115m;
        }
    }
}
