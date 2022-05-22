using System;
using System.Collections.Generic;
using Verse;
using RimWorld;

namespace ESCP_Skyshards
{
    class RitualAttachableOutcomeEffectWorker_TriggerSkyshardStarfall : RitualAttachableOutcomeEffectWorker
    {
		public override void Apply(Dictionary<Pawn, int> totalPresence, LordJob_Ritual jobRitual, OutcomeChance outcome, out string extraOutcomeDesc, ref LookTargets letterLookTargets)
		{
			extraOutcomeDesc = null;

			if (Rand.Chance(0.5f))
			{
				IncidentParms parms = new IncidentParms
				{
					target = jobRitual.Map,
				};
				if (IncidentDefOf.ESCP_Skyshard_Starfall_Reward.Worker.TryExecute(parms))
				{
					extraOutcomeDesc = this.def.letterInfoText;
				}
			}
		}
	}
}
