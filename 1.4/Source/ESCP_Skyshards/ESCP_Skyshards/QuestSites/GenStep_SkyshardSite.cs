using Verse;
using RimWorld;
using UnityEngine;

namespace ESCP_Skyshards
{
    public class GenStep_SkyshardSite  : GenStep
    {
		public override int SeedPart
		{
			get
			{
				return 918672345;
			}
		}

		public override void Generate(Map map, GenStepParams parms)
		{
			TraverseParms traverseParams = TraverseParms.For(TraverseMode.NoPassClosedDoors, Danger.Deadly, false, false, false).WithFenceblocked(true);
			IntVec3 root;
			if (RCellFinder.TryFindRandomCellNearTheCenterOfTheMapWith((IntVec3 x) => x.Standable(map) && !x.GetTerrain(map).IsWater && !x.Fogged(map) && map.reachability.CanReachMapEdge(x, traverseParams) && x.GetRoom(map).CellCount >= this.MinRoomCells, map, out root))
			{
				Thing thing = ThingMaker.MakeThing(ThingDefOf.ESCP_SkyshardBuilding);
				GenSpawn.Spawn(thing, root, map);
				thing.TryGetComp<CompUseEffect_UseSkyshard>().site = parms.sitePart.site;
			}
		}

		private int MinRoomCells = 225;
	}
}
