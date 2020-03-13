using RimWorld;
using System.Linq;
using Verse;

namespace MoreHigherdifficulty
{
    public class IncidentWorker_METER : IncidentWorker
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
            Pawn animal;
            return TryFindRandomAnimal(map, out animal);
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            int j = 0;
            Map map = (Map)parms.target;
            float points = StorytellerUtility.DefaultThreatPointsNow(map);
            if (points > 2000)
            {
                points = 2000;
            }
            for (int i = 0; i < 3 + (points / 100); i++)
            {
                j++;
                IntVec3 loc = CellFinder.RandomCell(map);
                GenSpawn.Spawn(ThingDefOf.MeteoriteIncoming, loc, map);

            }
            if (TryFindRandomAnimal(map, out Pawn animal))
            {
                j++;
                IntVec3 loc = animal.InteractionCell;
                GenSpawn.Spawn(ThingDefOf.MeteoriteIncoming, loc, map);
            }
            if (j == 0)
            {
                return false;
            }
            Find.LetterStack.ReceiveLetter("LetterLabelMeterLight".Translate().CapitalizeFirst(), "LetterMeterLight".Translate(), LetterDefOf.ThreatSmall);
            Find.TickManager.slower.SignalForceNormalSpeedShort();
            return true;
        }

        private bool TryFindRandomAnimal(Map map, out Pawn animal)
        {
            return (from p in map.mapPawns.AllPawnsSpawned
                    where p.Faction == Faction.OfPlayer && p.MentalState == null
                    select p).TryRandomElement(out animal);
        }
    }

}
