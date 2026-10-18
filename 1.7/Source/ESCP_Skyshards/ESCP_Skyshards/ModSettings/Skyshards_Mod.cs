using UnityEngine;
using Verse;

namespace ESCP_Skyshards
{
    public class Skyshards_Mod : Mod
    {
        readonly Skyshards_ModSettings _settings;

        public Skyshards_Mod(ModContentPack contentPack) : base(contentPack)
        {
            this._settings = GetSettings<Skyshards_ModSettings>();
        }

        public override string SettingsCategory() => "ESCP_Skyshard_ModName".Translate();

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listing_Standard = new();
            listing_Standard.Begin(inRect);
            listing_Standard.Gap();

            listing_Standard.Label("ESCP_Skyshard_MinLevelGained".Translate() + " (" + _settings.ESCP_Skyshard_MinLevelGained + " level/s)");
            _settings.ESCP_Skyshard_MinLevelGained = (int)listing_Standard.Slider(_settings.ESCP_Skyshard_MinLevelGained, 1, _settings.ESCP_Skyshard_MaxLevelGained);
            listing_Standard.Gap();

            listing_Standard.Label("ESCP_Skyshard_MaxLevelGained".Translate() + " (" + _settings.ESCP_Skyshard_MaxLevelGained + " level/s)");
            _settings.ESCP_Skyshard_MaxLevelGained = (int)listing_Standard.Slider(_settings.ESCP_Skyshard_MaxLevelGained, _settings.ESCP_Skyshard_MinLevelGained, 20);
            listing_Standard.Gap();

            listing_Standard.GapLine();
            listing_Standard.Gap();

            listing_Standard.CheckboxLabeled("ESCP_Skyshard_EnableStarfall".Translate(), ref _settings.ESCP_Skyshard_EnableStarfall);
            listing_Standard.Gap();

            listing_Standard.CheckboxLabeled("ESCP_Skyshard_EnableInertSkyshards".Translate(), ref _settings.ESCP_Skyshard_EnableInertSkyshards, "ESCP_Skyshard_EnableInertSkyshards_Tooltip".Translate());
            listing_Standard.Gap();

            listing_Standard.CheckboxLabeled("ESCP_Skyshard_DisableSkillCap".Translate(), ref _settings.ESCP_Skyshard_DisableSkillCap, "ESCP_Skyshard_DisableSkillCap_Tooltip".Translate());
            listing_Standard.Gap();

            listing_Standard.GapLine();
            listing_Standard.Gap();

            //reset
            Rect rectDefault = listing_Standard.GetRect(30f);
            TooltipHandler.TipRegion(rectDefault, "ESCP_Reset".Translate());
            if (Widgets.ButtonText(rectDefault, "ESCP_Reset".Translate(), true, true, true))
            {
                _settings.ResetSettings();
            }

            listing_Standard.End();
            base.DoSettingsWindowContents(inRect);
        }
    }
}
