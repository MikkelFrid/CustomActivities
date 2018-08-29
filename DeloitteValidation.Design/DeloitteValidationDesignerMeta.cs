using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities.Core.Presentation;

// Added
using System.Activities.Presentation.Metadata;

namespace DeloitteValidation.Design
{
    public sealed class DelotteValidationDesignerMeta : IRegisterMetadata
    {
        public void Register()
        {
            RegisterAll();
        }
        public static void RegisterAll()
        {
            var builder = new AttributeTableBuilder();
            DeloitteValidationDesigner.RegisterMetadata(builder);
            // TODO: Other activities can be added here
            MetadataStore.AddAttributeTable(builder.CreateTable());
        }
    }
}
