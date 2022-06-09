using Verse;
using RimWorld;

namespace ESCP_Skyshards
{
    class Comp_SkyshardSculpture : ThingComp
    {

        private string usedByName = "";
        private SkillDef skillDef = null;
        private int skillLevel = 0;

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Defs.Look(ref skillDef, "skyshard_SkillDef");
            Scribe_Values.Look(ref skillLevel, "skyshard_SkillLevel", 1);
            Scribe_Values.Look(ref usedByName, "skyshard_UsedByName", "");
        }

        public override string CompInspectStringExtra()
        {
            return skillDef != null ? "ESCP_Skyshard_SculptureDetails".Translate(usedByName, skillLevel, skillDef.label) : null;
        }

        public void Setup(Pawn usedBy, SkillDef skill, int level)
        {
            usedByName = usedBy.Name.ToStringFull;
            skillDef = skill;
            skillLevel = level;
        }
    }
}
