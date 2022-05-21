using System;
using Verse;
using RimWorld;
using UnityEngine;
using Verse.Sound;
using Verse.AI;

namespace ESCP_Skyshards
{
    class CompUseEffect_UseSkyshard : CompUseEffect
    {
        /* ========== Basic stuff ========== */

        private SkillDef skillDef = null;
        private int skillLevel = 0;

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Defs.Look(ref skillDef, "skyshard_SkillDef");
            Scribe_Values.Look(ref skillLevel, "skyshard_SkillLevel", 1);
        }

        public override void PostPostMake()
        {
            base.PostPostMake();

            if (skillLevel == 0)
            {
                skillLevel = Rand.Range(3, 6);
                skillDef = DefDatabase<SkillDef>.GetRandom();
            }
        }

        public override string CompInspectStringExtra()
        {
            return "ESCP_Skyshard_Details".Translate(skillDef.label, skillLevel);
        }

        /* ========== The actual doing part of things ========== */

        public override bool CanBeUsedBy(Pawn p, out string failReason)
        {
            SkillRecord sr = p.skills.skills.Find(x => x.def == skillDef);
            if (sr == null)
            {
                failReason = "ERR";
                return false;
            }
            if (sr.TotallyDisabled)
            {
                failReason = "ESCP_Skyshard_SkillDisabled".Translate(p.Name, skillDef.label);
                return false;
            }
            if (sr.levelInt >= 20)
            {
                failReason = "ESCP_Skyshard_SkillMaxed".Translate(p.Name, skillDef.label);
                return false;
            }

            return base.CanBeUsedBy(p, out failReason);
        }

        public override void DoEffect(Pawn usedBy)
        {
            usedBy.records.Increment(RecordDefOf.ESCP_SkyshardsUsed);
            FleckMaker.AttachedOverlay(usedBy, FleckDefOf.PsycastAreaEffect, Vector3.zero, 1f, -1f);
            SkillRecord sr = usedBy.skills.skills.Find(x => x.def == skillDef);
            //ensuring the added levels don't go over 20
            int temp = sr.levelInt + skillLevel;
            sr.levelInt = Mathf.Clamp(temp, 1, 20);
        }
    }
}
