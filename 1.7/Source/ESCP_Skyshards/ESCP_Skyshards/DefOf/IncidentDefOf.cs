using RimWorld;

namespace ESCP_Skyshards
{
    [DefOf]
    public static class IncidentDefOf
    {
        static IncidentDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(IncidentDefOf));
        }

        public static IncidentDef ESCP_Skyshard_Starfall_Reward;
    }
}
