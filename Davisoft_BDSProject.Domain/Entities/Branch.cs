using System.ComponentModel.DataAnnotations.Schema;
using Davisoft_BDSProject.Domain.Enum;

namespace Davisoft_BDSProject.Domain.Entities
{
    public class Branch : Entity
    {
        public string Code { get; set; }
        public string Name { get; set; }// default for English name
        public string Name2 { get; set; }// for thai name
        public string Address { get; set; }
        public string AddressEnglish { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string TaxNumber { get; set; }
    }
}
