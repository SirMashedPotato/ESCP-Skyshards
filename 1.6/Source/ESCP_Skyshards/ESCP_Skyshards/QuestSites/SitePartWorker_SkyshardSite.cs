using System.Collections.Generic;
using RimWorld.QuestGen;
using Verse;
using Verse.Grammar;
using RimWorld;
using RimWorld.Planet;

namespace ESCP_Skyshards
{
    public class SitePartWorker_SkyshardSite : SitePartWorker
    {
		public override void Notify_GeneratedByQuestGen(SitePart part, Slate slate, List<Rule> outExtraDescriptionRules, Dictionary<string, string> outExtraDescriptionConstants)
		{
			base.Notify_GeneratedByQuestGen(part, slate, outExtraDescriptionRules, outExtraDescriptionConstants);
		}

		public override string GetPostProcessedThreatLabel(Site site, SitePart sitePart)
		{
			string text = base.GetPostProcessedThreatLabel(site, sitePart);
			if (site.HasWorldObjectTimeout)
			{
				text += " (" + "DurationLeft".Translate(site.WorldObjectTimeoutTicksLeft.ToStringTicksToPeriod(true, false, true, true)) + ")";
			}
			return text;
		}
	}
}
