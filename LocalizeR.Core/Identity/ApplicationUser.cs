using LocalizeR.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace LocalizeR.Core.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? BusinessName { get; set; }
        public string? Location { get; set; }

        public Guid? ImageId { get; set; }
        public byte[]? ImageData { get; set; }
        public string? ServiceType { get; set; }
        public List<Rating> Ratings { get; set; }
        public int? ServiceId { get; set; }
    }

}
