using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Victoria_II_Custom_Lib.Countries
{
    public class BudgetInfo
    {
        public decimal PoorTax { get; set; }

        public decimal MiddleTax { get; set; }

        public decimal RichTax { get; set; }

        public decimal TariffTax { get; set; }

        public decimal LandSpending { get; set; }

        public decimal NavalSpending { get; set; }

        public decimal StockpileSpending { get; set; }

        public decimal EducationSpending { get; set; }

        public decimal AdminSpending { get; set; }

        public decimal SocialSpending { get; set; }

        public decimal MilitarySpending { get; set; }
    }
}
