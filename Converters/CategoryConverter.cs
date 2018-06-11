using ApiCore.BO;

namespace ApiCore.Converters
{
    public static class CategoryConverter
    {
        public static DTO.Category ToDto(this Category category)
        {
            return new DTO.Category
            {
                Id = category.Id,
                Name = category.Name
            };
        }
    }
}