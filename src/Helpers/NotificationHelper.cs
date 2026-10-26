using System;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Buildings;
using TaleWorlds.Library;

namespace AutoSettlementBuilder.Helpers
{
    public static class NotificationHelper
    {
        private static readonly Color MessageColor = Color.FromUint(0xFFE5A93C); // Amber / Gold

        public static void NotifyAutoQueued(Town town, Building building, string language)
        {
            if (town == null || building == null) return;

            string townName = town.Name?.ToString() ?? "Settlement";
            string buildingName = building.Name?.ToString() ?? "Building";
            int nextTier = building.CurrentLevel + 1;

            string text;
            if (string.Equals(language, "en", StringComparison.OrdinalIgnoreCase))
            {
                text = $"[Realm Administration] Auto-queued '{buildingName} (Tier {nextTier})' in {townName} based on settlement needs.";
            }
            else
            {
                text = $"[Kraliyet İdaresi] {townName} için eksiklikler doğrultusunda '{buildingName} (Seviye {nextTier})' inşaat sırasına alındı.";
            }

            DisplayMessage(text);
        }

        public static void NotifyDailyProjectChanged(Town town, Building dailyBuilding, string language)
        {
            if (town == null || dailyBuilding == null) return;

            string townName = town.Name?.ToString() ?? "Settlement";
            string dailyName = dailyBuilding.Name?.ToString() ?? "Daily Project";

            string text;
            if (string.Equals(language, "en", StringComparison.OrdinalIgnoreCase))
            {
                text = $"[Realm Administration] Set daily project to '{dailyName}' in {townName} based on settlement needs.";
            }
            else
            {
                text = $"[Kraliyet İdaresi] {townName} için kriz önlemi doğrultusunda günlük proje '{dailyName}' olarak belirlendi.";
            }

            DisplayMessage(text);
        }

        public static void NotifyConstructionBoost(Town town, int amount, string language)
        {
            if (town == null) return;

            string townName = town.Name?.ToString() ?? "Settlement";

            string text;
            if (string.Equals(language, "en", StringComparison.OrdinalIgnoreCase))
            {
                text = $"[Realm Administration] Allocated {amount:N0} denars from treasury to boost construction in {townName}.";
            }
            else
            {
                text = $"[Kraliyet İdaresi] {townName} inşaat hızlandırması için hazineden {amount:N0} dinar tahsis edildi.";
            }

            DisplayMessage(text);
        }

        private static void DisplayMessage(string text)
        {
            try
            {
                InformationManager.DisplayMessage(new InformationMessage(text, MessageColor));
            }
            catch (Exception ex)
            {
                Debug.Print($"[AutoSettlementBuilder] Could not display message: {ex.Message}");
            }
        }
    }
}
