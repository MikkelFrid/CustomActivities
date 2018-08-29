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
        /*

        [DefaultValue(null)]
        public InArgument<string> Text { get; set; }

        */

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
                    reg = new Regex(@"^\w{4}\d{6,7}$");
                    break;

                case "OptionB":
                    
                    break;
                default:
                    
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
