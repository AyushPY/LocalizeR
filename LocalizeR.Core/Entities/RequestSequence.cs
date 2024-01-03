using LocalizeR.Core.Identity;

namespace LocalizeR.Core.Entities
{
    public class RequestSequence
    {
        public int Id { get; set; }
        public int budget { get; set; }
        public DateTime deadline { get; set; }

        public string RequestDetails { get; set; }
        public Guid ServiceId { get; set; }

        public string? Severity { get; set; }
        public ApplicationUser ServiceProfile { get; set; }

        public Guid RequesterId { get; set; }

        public ApplicationUser RequesterProfile { get; set; }

    }
}
