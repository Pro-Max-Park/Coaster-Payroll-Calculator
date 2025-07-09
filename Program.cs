using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace $safeprojectname$
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }


    /// <summary>
    /// Class a capture details accociated with an employee's pay slip record
    /// </summary>
    public class Payslip
    {
        
    }

    /// <summary>
    /// Base class to hold all Pay calculation functions
    /// Default class behaviour is tax calculated with tax threshold applied
    /// </summary>
    public class Paycalculator
    {
        
    }

    /// <summary>
    /// Extends PayCalculator class handling No tax threshold
    /// </summary>
    public class PaycalculatorNoThreshold 
    {
        
    }

    /// <summary>
    /// Extends PayCalculator class handling With tax threshold
    /// </summary>
    public class PaycalculatorWithThreshold 
    {
        
    }

    public class TaxBracket
    {
        public decimal Min { get; set; }
        public decimal Max { get; set; }
        public decimal Rate { get; set; }
        public decimal BaseTax { get; set; }

        public TaxBracket(decimal min, decimal max, decimal rate, decimal baseTax)
        {
            Min = min;
            Max = max;
            Rate = rate;
            BaseTax = baseTax;
        }
    }

    public class TaxRateLoader
    {
        public static List<TaxBracket> LoadTaxBrackets(string filePath)
        {
            var taxBrackets = new List<TaxBracket>();

            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length == 4)
                {
                    decimal min = decimal.Parse(parts[0]);
                    decimal max = decimal.Parse(parts[1]);
                    decimal rate = decimal.Parse(parts[2]);
                    decimal baseTax = decimal.Parse(parts[3]);

                    taxBrackets.Add(new TaxBracket(min, max, rate, baseTax));
                }
            }

            return taxBrackets;
        }
    }
    public class PaymentRecord
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
        public int HoursWorked { get; set; }
        public double HourlyRate { get; set; }
        public string TaxThreshold { get; set; }
        public decimal GrossPay { get; set; }
        public decimal Tax { get; set; }
        public decimal NetPay { get; set; }
        public decimal Superannuation { get; set; }
    }
}
