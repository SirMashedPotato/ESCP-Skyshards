using Verse;

namespace ESCP_Skyshards
{
    class Skyshards_ModSettings : ModSettings
    {
        private static Skyshards_ModSettings _instance;

        public static int MinLevelGained
        {
            get
            {
                return _instance.ESCP_Skyshard_MinLevelGained;
            }
        }

        public static int MaxLevelGained
        {
            get
            {
                return _instance.ESCP_Skyshard_MaxLevelGained;
            }
        }

        public static bool DisableSkillCap
        {
            get
            {
                return _instance.ESCP_Skyshard_DisableSkillCap;
            }
        }

        public static bool EnableStarfall
        {
            get
            {
                return _instance.ESCP_Skyshard_EnableStarfall;
            }
        }

        public static bool EnableInertSkyshards
        {
            get
            {
                return _instance.ESCP_Skyshard_EnableInertSkyshards;
            }
        }

        /*  */

        public int ESCP_Skyshard_MinLevelGained = 3;
        public int ESCP_Skyshard_MaxLevelGained = 6;
        public bool ESCP_Skyshard_DisableSkillCap = false;
        public bool ESCP_Skyshard_EnableStarfall = true;
        public bool ESCP_Skyshard_EnableInertSkyshards = true;

        public Skyshards_ModSettings()
        {
            _instance = this;
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref ESCP_Skyshard_MinLevelGained, "ESCP_Skyshard_MinLevelGained", 3);
            Scribe_Values.Look(ref ESCP_Skyshard_MaxLevelGained, "ESCP_Skyshard_MaxLevelGained", 6);
            Scribe_Values.Look(ref ESCP_Skyshard_DisableSkillCap, "ESCP_Skyshard_DisableSkillCap", false);
            Scribe_Values.Look(ref ESCP_Skyshard_EnableStarfall, "ESCP_Skyshard_MaxESCP_Skyshard_EnableStarfallLevelGained", true);
            Scribe_Values.Look(ref ESCP_Skyshard_EnableInertSkyshards, "ESCP_Skyshard_EnableInertSkyshards", true);
        }

        public void ResetSettings()
        {
            _instance.ESCP_Skyshard_MinLevelGained = 3;
            _instance.ESCP_Skyshard_MaxLevelGained = 6;
            _instance.ESCP_Skyshard_DisableSkillCap = false;
            _instance.ESCP_Skyshard_EnableStarfall = true;
            _instance.ESCP_Skyshard_EnableInertSkyshards = true;
        }
    }
}
