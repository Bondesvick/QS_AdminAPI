using System.Collections.Generic;
using System.Xml.Serialization;

namespace QuickServiceAdmin.Core.Model
{
    [XmlRoot(ElementName = "CustomerDocumentData")]
    public class CustomerDocumentData
    {
        [XmlElement(ElementName = "DocumentData")]
        public List<DocumentData> DocumentData { get; set; }
    }
}