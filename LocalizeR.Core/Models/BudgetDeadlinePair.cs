namespace LocalizeR.Core.Models
{
    public class BudgetDeadlinePair
    {
        public int budget { get; set; }
        public DateTime? deadline { get; set; }
        public Guid requesterID { get; set; }

        public string severity { get; set; }
        public string requesterUsername { get; set; }
    }
}
