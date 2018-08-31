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

namespace DeloittePDF.DesignSplitPDF
{
    // Interaction logic for DeloittePDF.xaml
    public partial class DeloittePDFDesignSplitPDF
    {
        public DeloittePDFDesignSplitPDF()
        {
            this.InitializeComponent();
        }

        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            builder.AddCustomAttributes(typeof(SplitPDF), new CategoryAttribute("Deloitte PDF"));
            builder.AddCustomAttributes(typeof(SplitPDF), new DesignerAttribute(typeof(DeloittePDFDesignSplitPDF)));
            builder.AddCustomAttributes(typeof(SplitPDF), new DescriptionAttribute("Splits a PDF file into multiple files"));
        }
    }
}
