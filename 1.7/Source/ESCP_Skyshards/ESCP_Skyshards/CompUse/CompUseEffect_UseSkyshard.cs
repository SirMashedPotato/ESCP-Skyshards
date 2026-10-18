using Verse;
using RimWorld;
using RimWorld.Planet;
using UnityEngine;

namespace ESCP_Skyshards
{
    public class CompUseEffect_UseSkyshard : CompUseEffect
    {
        private SkillDef _skillDef;
        private int _skillLevel;
        public Site Site;

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Defs.Look(ref _skillDef, "skyshard_SkillDef");
            Scribe_Values.Look(ref _skillLevel, "skyshard_SkillLevel", 1);
            Scribe_References.Look(ref Site, "skyshard_Site");
        }

        public override void PostPostMake()
        {
            base.PostPostMake();

            if (_skillLevel != 0) return;
            _skillLevel = Rand.Range(Skyshards_ModSettings.MinLevelGained, Skyshards_ModSettings.MaxLevelGained);
            _skillDef = DefDatabase<SkillDef>.GetRandom();
        }

        public override string TransformLabel(string label)
        {
            return base.TransformLabel(label + " (" + _skillDef.LabelCap +  " +" + _skillLevel + ")");
        }

        public override string CompInspectStringExtra()
        {
            return "ESCP_Skyshard_Details".Translate(_skillDef.label, _skillLevel);
        }

        public override AcceptanceReport CanBeUsedBy(Pawn p)
        {
            SkillRecord sr = p.skills.skills.Find(x => x.def == _skillDef);
            if (sr == null)
            {
                return "ERR: pawn doesn't have that skill at all";
            }
            if (sr.TotallyDisabled)
            {
                return "ESCP_Skyshard_SkillDisabled".Translate(p.Name, _skillDef.label);
            }
            if (sr.levelInt >= 20 && !Skyshards_ModSettings.DisableSkillCap)
            {
                return "ESCP_Skyshard_SkillMaxed".Translate(p.Name, _skillDef.label);
            }
            return true;
        }

        public override void DoEffect(Pawn usedBy)
        {
            usedBy.records.Increment(RecordDefOf.ESCP_SkyshardsUsed);
            FleckMaker.AttachedOverlay(usedBy, FleckDefOf.PsycastAreaEffect, Vector3.zero, 1f, -1f);
            SkillRecord sr = usedBy.skills.skills.Find(x => x.def == _skillDef);
            //ensuring the added levels don't go over 20
            bool flag = Skyshards_ModSettings.DisableSkillCap;
            int temp = sr.levelInt + _skillLevel;
            sr.levelInt = !flag ? Mathf.Clamp(temp, 1, 20) : temp;
            //completeing quest
            if (Site != null)
            {
                QuestUtility.SendQuestTargetSignals(Site.questTags, "SkyshardUsed", this.Named("SUBJECT"));
            }
            Messages.Message("ESCP_Skyshard_UsedMessage".Translate(usedBy.NameFullColored, _skillLevel, _skillDef.label), usedBy, MessageTypeDefOf.PositiveEvent, false);

            //creating inert skyshard
            if (!Skyshards_ModSettings.EnableInertSkyshards) return;
            Thing thing = ThingMaker.MakeThing(ThingDefOf.ESCP_SkyshardSculpture);
            thing.TryGetComp<Comp_SkyshardSculpture>().Setup(usedBy, _skillDef, _skillLevel);
            thing = thing.MakeMinified();
            GenPlace.TryPlaceThing(thing, parent.Position, parent.Map, ThingPlaceMode.Near);
        }
    }
}
