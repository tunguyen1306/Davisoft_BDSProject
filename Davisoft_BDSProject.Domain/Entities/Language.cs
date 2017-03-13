using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Davisoft_BDSProject.Domain.Entities
{
    public class Language : EntityBase
    {
        public string Value { get; set; }
        public string DisplayName { get; set; }
        public string Image { get; set; }
    }
}
