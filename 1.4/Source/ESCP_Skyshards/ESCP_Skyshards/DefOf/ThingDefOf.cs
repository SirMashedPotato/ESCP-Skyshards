using Verse;
using RimWorld;

namespace ESCP_Skyshards
{
    [DefOf]
    public static class ThingDefOf
    {
        static ThingDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(ThingDefOf));
        }

        public static ThingDef ESCP_SkyshardBuilding;
        public static ThingDef ESCP_SkyshardSculpture;
    }
}
