namespace AutoSettlementBuilder.Config
{
    public class ModConfig
    {
        public bool Enabled { get; set; } = true;
        public bool NotifyOnAutoQueue { get; set; } = true;
        public bool ManageDailyProjects { get; set; } = true;
        public float LoyaltyCrisisThreshold { get; set; } = 30f;
        public float FoodCrisisThreshold { get; set; } = 20f;
        public float SecurityCrisisThreshold { get; set; } = 40f;
        public bool PrioritizeMasonFirst { get; set; } = true;
        public bool AutoBoostConstructionFund { get; set; } = false;
        public int AutoBoostMinPlayerGold { get; set; } = 150000;
        public int AutoBoostFundAmount { get; set; } = 10000;
        public string Language { get; set; } = "tr"; // "tr" or "en"
    }
}
