using RimWorld;
using Verse;

namespace MoreHigherdifficulty
{
    class IncidentWorker_TornadoEmergence : IncidentWorker
    {

        protected override bool CanFireNowSub(IncidentParms parms)
        {
            if (!base.CanFireNowSub(parms))
            {
                return false;
            }
            if (Find.Storyteller.difficulty.difficulty != 8)
            {
                return false;
            }
            Map map = (Map)parms.target;
            return true;
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            if (!TryFindEntryCell(map, out IntVec3 cell))
            {
                return false;
            }

            for (int i = 0; i < Rand.Range(1, 3); i++)
            {

                IntVec3 loc = CellFinder.RandomCell(map);

                GenSpawn.Spawn(ThingDefOf.Tornado, loc, map);

            }

            Find.LetterStack.ReceiveLetter("LetterLabelTornadoEmergence".Translate().CapitalizeFirst(), "LetterTornadoEmergence".Translate(), LetterDefOf.ThreatSmall);
            Find.TickManager.slower.SignalForceNormalSpeedShort();
            return true;
        }

        private bool TryFindEntryCell(Map map, out IntVec3 cell)
        {
            return RCellFinder.TryFindRandomPawnEntryCell(out cell, map, CellFinder.EdgeRoadChance_Animal + 0.2f);
        }

    }
}
