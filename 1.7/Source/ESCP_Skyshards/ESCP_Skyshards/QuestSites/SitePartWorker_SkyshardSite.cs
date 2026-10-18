using Verse;
using RimWorld;
using RimWorld.Planet;

namespace ESCP_Skyshards
{
    public class SitePartWorker_SkyshardSite : SitePartWorker
    {
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
