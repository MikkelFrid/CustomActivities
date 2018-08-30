using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities.Core.Presentation;

// Added
using System.Activities.Presentation.Metadata;

namespace DeloittePDF.Design
{
    public sealed class DeloittePDFDesignerMeta : IRegisterMetadata
    {
        public void Register()
        {
            RegisterAll();
        }
        public static void RegisterAll()
        {
            var builder = new AttributeTableBuilder();
            DeloittePDFDesigner.RegisterMetadata(builder);
            // TODO: Other activities can be added here
            MetadataStore.AddAttributeTable(builder.CreateTable());
        }
    }
}
