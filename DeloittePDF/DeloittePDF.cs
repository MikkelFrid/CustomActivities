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
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.Diagnostics;
using System.IO;

namespace DeloittePDF
{
    // Start class and name the acitivty MergePDF
    [ToolboxBitmap(typeof(MergePDF), "MergePDF.png")]
    public sealed class MergePDF : CodeActivity
    {
        public DisplayViewer Option { get; set; }

        //Input: Set the input string
        [Category("Input")]
        [Description("File Directory")]
        [RequiredArgument]
        public InArgument<string> FileDirectory { get; set; }

        [Category("Input")]
        [Description("Name of the merged file")]
        [RequiredArgument]
        public InArgument<string> OutputName { get; set; }

        //Input: Set the string of the validated input string for debug
        [Category("Output")]
        [Description("Output full path")]
        public OutArgument<string> OutputFullPath { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            // Get some file names
            var files = Directory.EnumerateFiles(FileDirectory.Get(context), "*.*", SearchOption.AllDirectories)
            .Where(s => s.ToLower().EndsWith(".pdf"));

            // Open the output document
            PdfDocument outputDocument = new PdfDocument();

            // Iterate files
            foreach (string file in files)
            {
                // Open the document to import pages from it.
                PdfDocument inputDocument = PdfReader.Open(file, PdfDocumentOpenMode.Import);

                // Iterate pages
                int count = inputDocument.PageCount;
                for (int idx = 0; idx < count; idx++)
                {
                    // Get the page from the external document...
                    PdfPage page = inputDocument.Pages[idx];
                    // ...and add it to the output document.
                    outputDocument.AddPage(page);
                }
            }

            // Save the document...
            string filename = string.Format("{0}", FileDirectory.Get(context)) + "\\" + string.Format("{0}",OutputName.Get(context));
            outputDocument.Save(filename);

            // Set full output path 
            OutputFullPath.Set(context, filename);

            // Based on input from the selecter, open or do not open when merged
            if (string.Format("{0}", this.Option) == "Yes")
            {
                // ...and start a viewer.
                Process.Start(filename);
            }
        }
    }
    // End class and name the acitivty MergePDF

    // Start class and name the acitivty SplitPDF
    [ToolboxBitmap(typeof(SplitPDF), "SplitPDF.png")]
    public sealed class SplitPDF : CodeActivity
    {
        //Input: Set the input string
        [Category("Input")]
        [Description("PDF File")]
        [RequiredArgument]
        public InArgument<string> PdfFile { get; set; }

        [Category("Input")]
        [Description("Output Directory")]
        [RequiredArgument]
        public InArgument<string> OutputDirectory { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            // Get a fresh copy of the sample PDF file
            /*string filename = PdfFile.Get(context);
            File.Copy(Path.Combine("../../../../../PDFs/", filename),
              Path.Combine(Directory.GetCurrentDirectory(), filename), true);

            // Open the file
            PdfDocument inputDocument = PdfReader.Open(filename, PdfDocumentOpenMode.Import);*/

            var byteArray = File.ReadAllBytes(PdfFile.Get(context));
            using (var pdfOut = new PdfDocument())
            {
                using (var msInput = new MemoryStream(byteArray))
                {
                    var inputDocument = PdfReader.Open(msInput, PdfDocumentOpenMode.Import);
                    string name = Path.GetFileNameWithoutExtension(PdfFile.Get(context));
                    for (int idx = 0; idx < inputDocument.PageCount; idx++)
                    {
                        // Create new document
                        PdfDocument outputDocument = new PdfDocument();
                        outputDocument.Version = inputDocument.Version;
                        outputDocument.Info.Title =
                          String.Format("Page {0} of {1}", idx + 1, inputDocument.Info.Title);
                        outputDocument.Info.Creator = inputDocument.Info.Creator;

                        // Add the page and save it
                        outputDocument.AddPage(inputDocument.Pages[idx]);
                        outputDocument.Save(OutputDirectory.Get(context) + "\\" + String.Format("{0} - Page {1}.pdf", name, idx + 1));
                    }
                }

            }
        }
    }
    // End class and name the acitivty SplitPDF
}
