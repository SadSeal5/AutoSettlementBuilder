using System;
using System.Linq;
using AutoSettlementBuilder.Config;
using AutoSettlementBuilder.Helpers;
using Helpers;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Buildings;
using TaleWorlds.Library;

namespace AutoSettlementBuilder
{
    public static class DailyProjectEvaluator
    {
        public static void EvaluateDailyProject(Town town, ModConfig config)
        {
            if (town == null || config == null || !config.ManageDailyProjects)
                return;

            try
            {
                Building targetDaily = null;

                if (town.Loyalty < config.LoyaltyCrisisThreshold)
                {
                    targetDaily = town.Buildings.FirstOrDefault(b =>
                        b.BuildingType.IsDailyProject &&
                        b.BuildingType.HasEffect(BuildingEffectEnum.Loyalty));
                }
                else if (town.FoodStocks < config.FoodCrisisThreshold || (town.FoodChange < 0f && town.FoodStocks < 50f))
                {
                    targetDaily = town.Buildings.FirstOrDefault(b =>
                        b.BuildingType.IsDailyProject &&
                        (b.BuildingType.HasEffect(BuildingEffectEnum.FoodProduction) ||
                         b.BuildingType.HasEffect(BuildingEffectEnum.FoodStock)));
                }

                if (targetDaily != null && town.CurrentDefaultBuilding != targetDaily)
                {
                    BuildingHelper.ChangeDefaultBuilding(targetDaily, town);

                    if (config.NotifyOnAutoQueue)
                    {
                        NotificationHelper.NotifyDailyProjectChanged(town, targetDaily, config.Language);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Print($"[AutoSettlementBuilder] Error evaluating daily project for '{town.Name}': {ex.Message}");
            }
        }
    }
}
