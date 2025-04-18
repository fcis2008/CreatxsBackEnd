namespace Application.DTOs
{
    public class DistrictCreateDto
    {
        public string Name { get; set; }
        public string Notes { get; set; }
    }

    public class DistrictDto : DistrictCreateDto
    {
        public int Id { get; set; }
    }
}
