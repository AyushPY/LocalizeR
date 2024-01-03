namespace LocalizeR.Core.DTO
{
    public class RequestDTO
    {
        public int budget { get; set; }
        public DateTime? deadline { get; set; }
        public string requestDetails { get; set; }
        public Guid serviceID { get; set; }
        public Guid UserID { get; set; }

    }
}
