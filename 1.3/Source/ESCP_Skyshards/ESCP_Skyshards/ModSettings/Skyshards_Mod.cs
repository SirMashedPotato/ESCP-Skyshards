using UnityEngine;
using Verse;
using System;

namespace ESCP_Skyshards
{
    public class Skyshards_Mod : Mod
    {
        Skyshards_ModSettings settings;

        public Skyshards_Mod(ModContentPack contentPack) : base(contentPack)
        {
            this.settings = GetSettings<Skyshards_ModSettings>();
        }

        public override string SettingsCategory() => "ESCP_Skyshard_ModName".Translate();

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listing_Standard = new Listing_Standard();
            listing_Standard.Begin(inRect);
            listing_Standard.Gap();

            listing_Standard.Label("ESCP_Skyshard_MinLevelGained".Translate() + " (" + settings.ESCP_Skyshard_MinLevelGained + " level/s)");
            settings.ESCP_Skyshard_MinLevelGained = (int)listing_Standard.Slider(settings.ESCP_Skyshard_MinLevelGained, 1, settings.ESCP_Skyshard_MaxLevelGained);
            listing_Standard.Gap();

            listing_Standard.Label("ESCP_Skyshard_MaxLevelGained".Translate() + " (" + settings.ESCP_Skyshard_MaxLevelGained + " level/s)");
            settings.ESCP_Skyshard_MaxLevelGained = (int)listing_Standard.Slider(settings.ESCP_Skyshard_MaxLevelGained, settings.ESCP_Skyshard_MinLevelGained, 20);
            listing_Standard.Gap();

            listing_Standard.GapLine();
            listing_Standard.Gap();

            listing_Standard.CheckboxLabeled("ESCP_Skyshard_EnableStarfall".Translate(), ref settings.ESCP_Skyshard_EnableStarfall);
            listing_Standard.Gap();

            listing_Standard.CheckboxLabeled("ESCP_Skyshard_EnableInertSkyshards".Translate(), ref settings.ESCP_Skyshard_EnableInertSkyshards, "ESCP_Skyshard_EnableInertSkyshards_Tooltip".Translate());
            listing_Standard.Gap();

            listing_Standard.CheckboxLabeled("ESCP_Skyshard_DisableSkillCap".Translate(), ref settings.ESCP_Skyshard_DisableSkillCap, "ESCP_Skyshard_DisableSkillCap_Tooltip".Translate());
            listing_Standard.Gap();

            listing_Standard.GapLine();
            listing_Standard.Gap();

            //reset
            Rect rectDefault = listing_Standard.GetRect(30f);
            TooltipHandler.TipRegion(rectDefault, "ESCP_Reset".Translate());
            if (Widgets.ButtonText(rectDefault, "ESCP_Reset".Translate(), true, true, true))
            {
                settings.ResetSettings();
            }

            listing_Standard.End();
            base.DoSettingsWindowContents(inRect);
        }
    }
}
