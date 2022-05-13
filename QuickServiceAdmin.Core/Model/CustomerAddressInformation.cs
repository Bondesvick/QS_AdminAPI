using System.Collections.Generic;
using System.Xml.Serialization;

namespace QuickServiceAdmin.Core.Model
{
    [XmlRoot(ElementName = "CustomerAddressInformation")]
    public class CustomerAddressInformation
    {
        [XmlElement(ElementName = "Address")] public List<Address> Address { get; set; }
    }
}