using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berlin.Application.Invoice
{
    class NumbersToWords
    {
        private static String[] units = { "zero", "unu", "doi", "trei",
    "patru", "cinci", "șase", "șapte", "opt", "nouă", "zece", "unsprezece",
    "doisprezece", "treisprezece", "paisprezece", "cincisprezece", "șaisprezece",
    "șaptesprezece", "uptsprezece", "nouăsprezece" };
        private static String[] tens = { "", "", "douăzeci", "treizeci", "patruzeci",
    "cincizeci", "șaizeci", "șaptezeci", "optzeci", "nouăzeci" };

        public static String ConvertAmount(double amount)
        {
            try
            {
                Int64 amount_int = (Int64)amount;
                Int64 amount_dec = (Int64)Math.Round((amount - (double)(amount_int)) * 100);
                if (amount_dec == 0)
                {
                    return Convert(amount_int) + " Only.";
                }
                else
                {
                    return Convert(amount_int) + " Point " + Convert(amount_dec) + " Only.";
                }
            }
            catch (Exception e)
            {
                // TODO: handle exception  
            }
            return "";
        }

        public static String Convert(Int64 i)
        {
            if (i < 20)
            {
                return units[i];
            }
            if (i < 100)
            {
                return tens[i / 10] + ((i % 10 > 0) ? " și " + Convert(i % 10) : "");
            }
            if (i < 1000)
            {
                return units[i / 100] + " sute"
                        + ((i % 100 > 0) ? " " + Convert(i % 100) : "");
            }
            if (i < 100000)
            {
                return Convert(i / 1000) + " mii "
                        + ((i % 1000 > 0) ? " " + Convert(i % 1000) : "");
            }
            if (i < 10000000)
            {
                return Convert(i / 100000) + " zeci de mii "
                        + ((i % 100000 > 0) ? " " + Convert(i % 100000) : "");
            }
            if (i < 1000000000)
            {
                return Convert(i / 10000000) + " sute de mii "
                        + ((i % 10000000 > 0) ? " " + Convert(i % 10000000) : "");
            }
            return Convert(i / 1000000000) + " milioane "
                    + ((i % 1000000000 > 0) ? " " + Convert(i % 1000000000) : "");
        }
    }
}
