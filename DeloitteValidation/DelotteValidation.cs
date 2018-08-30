// Standard
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

// Added to all custom activities
using System.Activities;
using System.ComponentModel;
using System.Drawing;

// Added to this custom acitivity
using System.Text.RegularExpressions;

namespace DeloitteValidation
{
    // Define class and name the acitivty
    [ToolboxBitmap(typeof(RegexCollection), "Regex.png")]
    public sealed class RegexCollection : CodeActivity
    {
        /// <summary> 
        /// Gets or sets Option. 
        /// </summary> 
        public MyEnum Option { get; set; }

        public Regex reg;

        [Category("Input")]
        [RequiredArgument]
        public InArgument<string> Text { get; set; }

        [Category("Output")]
        public OutArgument<bool> Result { get; set; }

        [Category("Output")]
        public OutArgument<string> ResultText { get; set; }

        protected override void Execute(CodeActivityContext context)
        {

            
           
            switch (string.Format("{0}",this.Option))
            {
                case "Email":
                    reg = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*\s*$");
                    break;

                case "Url":
                    reg = new Regex(@"^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$");
                    break;

                case "Ip":
                    reg = new Regex(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");
                    break;

                case "DatetimeDk":
                    reg = new Regex(@"^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$");
                    break;

                case "Datetime":
                    reg = new Regex(@"^(?=\d)(?:(?:(?:(?:(?:0?[13578]|1[02])(\/|-|\.)31)\1|(?:(?:0?[1,3-9]|1[0-2])(\/|-|\.)(?:29|30)\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})|(?:0?2(\/|-|\.)29\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))|(?:(?:0?[1-9])|(?:1[0-2]))(\/|-|\.)(?:0?[1-9]|1\d|2[0-8])\4(?:(?:1[6-9]|[2-9]\d)?\d{2}))($|\ (?=\d)))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\ [AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$");
                    break;
                case "DaysoftheweekDk":
                    reg = new Regex(@"^(Søn|søn|Man|man|(T(irs|ors))|(t(irs|ors))|Fre|fre)(dag|\.)?$|Ons|ons(\.|dag)?$|Lør|lør(\.|dag)?$|T((ir?)|(o?r?))|t((ir?)|(o?r?))\.?$");

                    break;
                case "Daysoftheweek":
                    reg = new Regex(@"^(Sun|Mon|(T(ues|hurs))|Fri)(day|\.)?$|Wed(\.|nesday)?$|Sat(\.|urday)?$|T((ue?)|(hu?r?))\.?$");
                    break;

                case "MonthsDk":
                    reg = new Regex(@"^(?:J|j(anuar|u(ni|li))|(Februar|februar)|Ma(rts|j)|ma(rts|j)|A(pril|ugust)|a(pril|ugust)|(((Sept|Nov|Dec)em)|Okto)ber)|(((sept|nov|dec)em)|okto)ber$");
                    break;

                case "Months":
                    reg = new Regex(@"^(?:J(anuary|u(ne|ly))|February|Ma(rch|y)|A(pril|ugust)|(((Sept|Nov|Dec)em)|Octo)ber)$");
                    break;

                case "CurrencyDk":
                    reg = new Regex(@"^[+-]?[0-9]{1,3}(?:.?[0-9]{3})*(?:\,[0-9]{2})?$");
                    break;

                case "Currency":
                    reg = new Regex(@"^[+-]?[0-9]{1,3}(?:,?[0-9]{3})*(?:\.[0-9]{2})?$");
                    break;

                case "PhoneNumberDk":
                    reg = new Regex(@"^(?:(?<local>(?:\d ?){8})|(?<international>\+(?:\d ?){10}))");
                    break;

                case "AddressDk":
                    reg = new Regex(@"^(?:[A-zæøåÆØÅ]{2,40}\.?\s)+(?:[0-9]){1,5}\w?(?:\s.*)?$");
                    break;

                case "WholeNumbers":
                    reg = new Regex(@"^\d+$");
                    break;

                case "DecimailComma":
                    reg = new Regex(@"^\d*\,\d+$");
                    break;

                case "DecimalDot":
                    reg = new Regex(@"^\d*\.\d+$");
                    break;

                case "Initials":
                    reg = new Regex(@"(?<![A-Z])[A-Z]{2,5}(?![A-Z])");
                    break;

                case "XML":
                    reg = new Regex(@"<(\w+)(\s(\w*="".*? "")?)*((/>)|((/*?)>.*?</\1>))");
                    break;
                default:
                    reg = null;
                    break;
            }

            if (!reg.IsMatch(string.Format("{0}", context.GetValue(this.Text))))
            {
                Result.Set(context, false);
            }
            else
            {
                Result.Set(context, true);
            }

            ResultText.Set(context, string.Format("{0}", context.GetValue(this.Text)));

        }
    }
}
