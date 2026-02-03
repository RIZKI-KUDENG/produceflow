using ProduceFlow.Domain.Common;

namespace ProduceFlow.Domain.Entities
{
    public class Location : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;
    }
}