namespace ProduceFlow.Application.DTOs.Categories
{
    public class UpdateCategoryRequest 
    {
        public string Name { get; set; } = string.Empty;
        public int DepreciationYears { get; set; }
    }
}