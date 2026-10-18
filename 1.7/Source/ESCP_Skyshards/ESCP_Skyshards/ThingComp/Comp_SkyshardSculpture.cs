using Verse;
using RimWorld;

namespace ESCP_Skyshards
{
    class Comp_SkyshardSculpture : ThingComp
    {

        private string _usedByName = "";
        private SkillDef _skillDef;
        private int _skillLevel;

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Defs.Look(ref _skillDef, "skyshard_SkillDef");
            Scribe_Values.Look(ref _skillLevel, "skyshard_SkillLevel", 1);
            Scribe_Values.Look(ref _usedByName, "skyshard_UsedByName", "");
        }

        public override string CompInspectStringExtra()
        {
            return _skillDef != null ? "ESCP_Skyshard_SculptureDetails".Translate(_usedByName, _skillLevel, _skillDef.label) : "ESCP_Skyshard_SculptureDetails_Unknown".Translate();
        }

        public void Setup(Pawn usedBy, SkillDef skill, int level)
        {
            _usedByName = usedBy.Name.ToStringFull;
            _skillDef = skill;
            _skillLevel = level;
        }
    }
}
