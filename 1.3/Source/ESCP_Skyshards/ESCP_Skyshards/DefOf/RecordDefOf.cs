using System;
using Verse;
using RimWorld;

namespace ESCP_Skyshards
{
    [DefOf]
    public static class RecordDefOf
    {
        static RecordDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(RecordDefOf));
        }

        public static RecordDef ESCP_SkyshardsUsed;
    }
}
