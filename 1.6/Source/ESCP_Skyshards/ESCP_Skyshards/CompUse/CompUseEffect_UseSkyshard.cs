using Verse;
using RimWorld;
using RimWorld.Planet;
using UnityEngine;

namespace ESCP_Skyshards
{
    public class CompUseEffect_UseSkyshard : CompUseEffect
    {
        /* ========== Basic stuff ========== */

        public CompProperties_UseEffectSkyshard Props => (CompProperties_UseEffectSkyshard)props;

        private SkillDef skillDef = null;
        private int skillLevel = 0;
        public Site site;

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Defs.Look(ref skillDef, "skyshard_SkillDef");
            Scribe_Values.Look(ref skillLevel, "skyshard_SkillLevel", 1);
            Scribe_References.Look(ref site, "skyshard_Site");
        }

        public override void PostPostMake()
        {
            base.PostPostMake();

            if (skillLevel == 0)
            {
                skillLevel = Rand.Range(Skyshards_ModSettings.MinLevelGained, Skyshards_ModSettings.MaxLevelGained);
                skillDef = DefDatabase<SkillDef>.GetRandom();
            }
        }

        public override string TransformLabel(string label)
        {
            return base.TransformLabel(label + " (" + skillDef.LabelCap +  " +" + skillLevel + ")");
        }

        public override string CompInspectStringExtra()
        {
            return "ESCP_Skyshard_Details".Translate(skillDef.label, skillLevel);
        }

        /* ========== The actual doing part of things ========== */

        public override AcceptanceReport CanBeUsedBy(Pawn p)
        {
            SkillRecord sr = p.skills.skills.Find(x => x.def == skillDef);
            if (sr == null)
            {
                return "ERR: pawn doesn't have that skill at all";
            }
            if (sr.TotallyDisabled)
            {
                return "ESCP_Skyshard_SkillDisabled".Translate(p.Name, skillDef.label);
            }
            if (sr.levelInt >= 20 && !Skyshards_ModSettings.DisableSkillCap)
            {
                return "ESCP_Skyshard_SkillMaxed".Translate(p.Name, skillDef.label);
            }
            return true;
        }

        public override void DoEffect(Pawn usedBy)
        {
            usedBy.records.Increment(RecordDefOf.ESCP_SkyshardsUsed);
            FleckMaker.AttachedOverlay(usedBy, FleckDefOf.PsycastAreaEffect, Vector3.zero, 1f, -1f);
            SkillRecord sr = usedBy.skills.skills.Find(x => x.def == skillDef);
            //ensuring the added levels don't go over 20
            bool flag = Skyshards_ModSettings.DisableSkillCap;
            int temp = sr.levelInt + skillLevel;
            sr.levelInt = !flag ? Mathf.Clamp(temp, 1, 20) : temp;
            //completeing quest
            if (site != null)
            {
                QuestUtility.SendQuestTargetSignals(site.questTags, "SkyshardUsed", this.Named("SUBJECT"));
            }
            Messages.Message("ESCP_Skyshard_UsedMessage".Translate(usedBy.NameFullColored, skillLevel, skillDef.label), usedBy, MessageTypeDefOf.PositiveEvent, false);

            //creating inert skyshard
            if (Skyshards_ModSettings.EnableInertSkyshards)
            {
                Thing thing = ThingMaker.MakeThing(ThingDefOf.ESCP_SkyshardSculpture);
                thing.TryGetComp<Comp_SkyshardSculpture>().Setup(usedBy, skillDef, skillLevel);
                thing = thing.MakeMinified();
                GenPlace.TryPlaceThing(thing, parent.Position, parent.Map, ThingPlaceMode.Near);
            }
        }
    }
}
