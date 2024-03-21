using Verse;
using RimWorld;

namespace ESCP_Skyshards
{
    public class CompProperties_UseEffectSkyshard : CompProperties_UseEffect
    {
        public CompProperties_UseEffectSkyshard()
        {
            compClass = typeof(CompUseEffect_UseSkyshard);
        }
    }
}