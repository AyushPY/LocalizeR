using LocalizeR.Core.Identity;

namespace LocalizeR.Core.Entities
{
    public class Rating
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public Guid ServiceProviderId { get; set; }
        public ApplicationUser ServiceProviderProfile { get; set; }
    }
}
