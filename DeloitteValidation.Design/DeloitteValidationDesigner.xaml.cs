using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;

using System.Activities.Presentation.Metadata;
using System.ComponentModel;

namespace DeloitteValidation.Design
{
    // Interaction logic for DeloitteValition.xaml
   public partial class DeloitteValidationDesigner
    {
       public DeloitteValidationDesigner()
       {
          this.InitializeComponent();
       }
    
       public static void RegisterMetadata(AttributeTableBuilder builder)
       {
           builder.AddCustomAttributes(typeof(RegexCollection), new CategoryAttribute("Validation"));
           builder.AddCustomAttributes(typeof(RegexCollection), new DesignerAttribute(typeof(DeloitteValidationDesigner)));
           builder.AddCustomAttributes(typeof(RegexCollection), new DescriptionAttribute("My sample activity"));
       }
  }
}
