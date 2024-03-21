using RimWorld;
using RimWorld.Planet;
using Verse;

namespace ESCP_Skyshards
{
    public class GenStep_SkyshardSite  : GenStep_Scatterer
    {
		public override int SeedPart => 918672345;

        protected override bool CanScatterAt(IntVec3 loc, Map map)
        {
            return base.CanScatterAt(loc, map) && loc.Standable(map);
        }

        protected override void ScatterAt(IntVec3 loc, Map map, GenStepParams parms, int count = 1)
        {

            Thing thing = ThingMaker.MakeThing(ThingDefOf.ESCP_SkyshardBuilding);
            GenSpawn.Spawn(thing, loc, map);
            thing.TryGetComp<CompUseEffect_UseSkyshard>().site = parms.sitePart.site;

            MapGenerator.rootsToUnfog.Add(loc);
            MapGenerator.SetVar("RectOfInterest", CellRect.CenteredOn(loc, 1, 1));
        }
    }
}
