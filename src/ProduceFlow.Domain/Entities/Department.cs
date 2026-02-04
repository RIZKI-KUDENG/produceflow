using ProduceFlow.Domain.Common;

namespace ProduceFlow.Domain.Entities
{
    public class Department : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string CostCenterCode { get; set; } = string.Empty;
    }
}