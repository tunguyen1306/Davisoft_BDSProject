using System.ComponentModel.DataAnnotations.Schema;

namespace Davisoft_BDSProject.Domain.Entities
{
    public class Counter : EntityBase
    {
        public Counter()
        {
        }

        public Counter(string name, int value)
        {
            Name = name;
            Value = value;
        }
        public Counter(string name,int branchId, int value)
        {
            Name = name;
            BranchID = branchId;
            Value = value;
        }

        public string Name { get; set; }
        public int Value { get; set; }

        public int? BranchID { get; set; }

        [ForeignKey("BranchID")]
        public virtual Branch Branch { get; set; }
    }
}
