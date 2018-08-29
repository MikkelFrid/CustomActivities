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
    public sealed class RegexCollection : NativeActivity<string>
    {
        /// <summary> 
        /// Gets or sets Option. 
        /// </summary> 
        public MyEnum Option { get; set; }
        public bool TestCode { get; set; }

        [DefaultValue(null)]
        public InArgument<string> Text { get; set; }


        protected override void Execute(NativeActivityContext context)
        {
            this.Result.Set(
                context,
                string.Format(
                    "Text is {0}, TestCode is {1}, Option is {2}",
                    context.GetValue(this.Text),
                    this.TestCode,
                    this.Option));
        }
    }
}
