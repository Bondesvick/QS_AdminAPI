using System.Collections.Generic;
using System.Xml.Serialization;

namespace QuickServiceAdmin.Core.Model
{
    [XmlRoot(ElementName = "CustomerPhoneEmailInformation")]
    public class CustomerPhoneEmailInformation
    {
        [XmlElement(ElementName = "phoneEmailInfo")]
        public List<PhoneEmailInfo> PhoneEmailInfo { get; set; }
    }
}