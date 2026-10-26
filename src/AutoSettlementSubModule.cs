using System;
using AutoSettlementBuilder.Config;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace AutoSettlementBuilder
{
    public class AutoSettlementSubModule : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            try
            {
                ConfigManager.Initialize();
                Debug.Print("[AutoSettlementBuilder] Initialized successfully.");
            }
            catch (Exception ex)
            {
                Debug.Print($"[AutoSettlementBuilder] Error during OnSubModuleLoad: {ex.Message}");
            }
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            base.OnGameStart(game, gameStarterObject);

            if (game.GameType is Campaign && gameStarterObject is CampaignGameStarter campaignStarter)
            {
                try
                {
                    campaignStarter.AddBehavior(new AutoSettlementBehavior());
                    Debug.Print("[AutoSettlementBuilder] AutoSettlementBehavior registered.");
                }
                catch (Exception ex)
                {
                    Debug.Print($"[AutoSettlementBuilder] Error adding AutoSettlementBehavior: {ex.Message}");
                }
            }
        }
    }
}
