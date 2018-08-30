using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace DeloitteValidation
{
    public enum MyEnum
    {
        [Display(Name = "Email Address")]
        Email,
        [Display(Name = "URL")]
        Url,
        [Display(Name = "IP Address")]
        Ip,
        [Display(Name = "Datetime (DK)")]
        DatetimeDk,
        [Display(Name = "Datetime")]
        Datetime,
        [Display(Name = "Days of the week (DK)")]
        DaysoftheweekDk,
        [Display(Name = "Days of the week")]
        Daysoftheweek,
        [Display(Name = "Months (DK)")]
        MonthsDk,
        [Display(Name = "Months")]
        Months,
        [Display(Name = "Currency (DK)")]
        CurrencyDk,
        [Display(Name = "Currency")]
        Currency,
        [Display(Name = "Phone number (DK)")]
        PhoneNumberDk,
        [Display(Name = "Address (DK)")]
        AddressDk,
        [Display(Name = "Whole Numbers")]
        WholeNumbers,
        [Display(Name = "Decimal Numbers (,)")]
        DecimailComma,
        [Display(Name = "Decimal Numbers (.)")]
        DecimalDot,
        [Display(Name = "Initials (2-5 Captial letters)")]
        Initials,
        [Display(Name = "XML Tag")]
        XML
    }
}
