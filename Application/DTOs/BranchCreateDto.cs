namespace Application.DTOs
{
    public class BranchCreateDto
    {
        public string Name { get; set; }

        public string ManagerName { get; set; }

        public string PhoneNumber { get; set; }

        public int CityId { get; set; }

        public int DistrictId { get; set; }

        public string Address { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int StoreId { get; set; }

        public bool IsPublish { get; set; }

    }

    public class BranchDto : BranchCreateDto
    {
        public int Id { get; set; }
    }
}
