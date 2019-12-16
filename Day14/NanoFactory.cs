using System;
using System.Collections.Generic;
using System.Linq;


namespace JakubSturc.AdventOfCode2019.Day14
{
    public class NanoFactory
    {
        private readonly Dictionary<Chemical, Reaction> _reactions;

        public NanoFactory(IEnumerable<Reaction> reactions)
        {
            _reactions = reactions.ToDictionary(r => r.Output.Chemical);
        }

        public long GetOreRequirementFor(ReactionItem ri)
        {
            var pool = ReactionPool.InitWith(ri);
            var reactionItem = ReduceToOre(pool);
            return reactionItem.Quantity;
        }

        private ReactionItem ReduceToOre(ReactionPool pool)
        {
            while (true)
            {
                var first = pool.FirstNonZeroNonOre();

                if (first == null)
                {
                    return pool[Chemical.ORE];
                }
                var chem = first.Value.Chemical;
                var req = first.Value.Quantity;
                var reaction = _reactions[chem];
                var pos = reaction.Output.Quantity;
                var act = DivU(req, pos);
                var ris = reaction.Input
                    .Select(ri => act * ri)
                    .Append(-act * reaction.Output);
                pool.Update(ris);

                long DivU(long a, long b) => a % b == 0 ? a / b : (a / b) + 1;
            }
        }

        

    }
}
