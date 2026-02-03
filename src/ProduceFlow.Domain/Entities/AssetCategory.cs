using ProduceFlow.Domain.Common;

namespace ProduceFlow.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public int DepreciationYears { get; set; }
    }
}