namespace Application.DTOs
{
    public class CityCreateDto
    {
        public string Name { get; set; }
    }

    public class CityDto : CityCreateDto
    {
        public int Id { get; set; }
    }
}
