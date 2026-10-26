using System;
using AutoSettlementBuilder.Config;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Buildings;
using TaleWorlds.Library;

namespace AutoSettlementBuilder
{
    public class AutoSettlementBehavior : CampaignBehaviorBase
    {
        public override void RegisterEvents()
        {
            CampaignEvents.DailyTickSettlementEvent.AddNonSerializedListener(this, OnDailyTickSettlement);
            CampaignEvents.OnBuildingLevelChangedEvent.AddNonSerializedListener(this, OnBuildingLevelChanged);
        }

        private void OnDailyTickSettlement(Settlement settlement)
        {
            if (settlement == null || !settlement.IsFortification || settlement.Town == null)
                return;

            if (settlement.OwnerClan != Clan.PlayerClan)
                return;

            var config = ConfigManager.Config;
            if (!config.Enabled)
                return;

            try
            {
                if (settlement.Town.BuildingsInProgress.Count == 0)
                {
                    SettlementNeedEvaluator.EvaluateAndEnqueue(settlement.Town, config);
                }

                if (config.ManageDailyProjects)
                {
                    DailyProjectEvaluator.EvaluateDailyProject(settlement.Town, config);
                }
            }
            catch (Exception ex)
            {
                Debug.Print($"[AutoSettlementBuilder] Error in OnDailyTickSettlement for '{settlement.Name}': {ex.Message}");
            }
        }

        private void OnBuildingLevelChanged(Town town, Building building, int levelChange)
        {
            if (town == null || town.OwnerClan != Clan.PlayerClan)
                return;

            var config = ConfigManager.Config;
            if (!config.Enabled)
                return;

            try
            {
                if (town.BuildingsInProgress.Count == 0)
                {
                    SettlementNeedEvaluator.EvaluateAndEnqueue(town, config);
                }
            }
            catch (Exception ex)
            {
                Debug.Print($"[AutoSettlementBuilder] Error in OnBuildingLevelChanged for '{town.Name}': {ex.Message}");
            }
        }

        public override void SyncData(IDataStore dataStore)
        {
            // Left intentionally empty so save files remain completely vanilla and uncorrupted.
        }
    }
}
