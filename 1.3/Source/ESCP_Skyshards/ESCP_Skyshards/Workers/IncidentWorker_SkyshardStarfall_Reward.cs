using System;
using Verse;
using RimWorld;

namespace ESCP_Skyshards
{
    class IncidentWorker_SkyshardStarfall_Reward : IncidentWorker
    {
        protected override bool CanFireNowSub(IncidentParms parms)
        {
            if (!base.CanFireNowSub(parms))
            {
                return false;
            }

            return PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive_FreeColonists_NoCryptosleep.Any<Pawn>();
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {

            int count = Rand.RangeInclusive(3, 5);
            for (int i = 0; i < count; i++)
            {
                Quest quest = QuestUtility.GenerateQuestAndMakeAvailable(this.def.questScriptDef, parms.points);
                QuestUtility.SendLetterQuestAvailable(quest);
            }
            base.SendStandardLetter("ESCP_Skyshard_Starfall_Label".Translate(), "ESCP_Skyshard_Starfall_Description".Translate(Faction.OfPlayer.def.pawnsPlural), this.def.letterDef, parms, LookTargets.Invalid);
            return true;
        }
    }
}
