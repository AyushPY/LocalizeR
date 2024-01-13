namespace RepositoryContracts.Models
{
    public class ServiceProviderInfo
    {
        public string BusinessName { get; set; }
        public string UserName { get; set; }
        public string ContactNo { get; set; }
        public string Location { get; set; }

        public string ServiceType { get; set; }

        public Guid Id { get; set; }
    }
}
