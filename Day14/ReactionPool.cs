using System.Collections.Generic;
using System.Linq;

namespace JakubSturc.AdventOfCode2019.Day14
{
    public class ReactionPool
    {
        private readonly ReactionItem[] _chems;

        private ReactionPool()
        {
            _chems = Chemical.All.Select(c => new ReactionItem(c, 0)).ToArray();
        }

        public static ReactionPool InitWith(ReactionItem ri)
        {
            var pool = new ReactionPool();
            pool[ri.Chemical] = ri;
            return pool;
        }

        public ReactionItem? FirstNonZeroNonOre()
        {
            foreach (var ri in _chems)
            {
                if (ri.Chemical == Chemical.ORE) continue;
                if (ri.Quantity < 1) continue;
                return ri;
            }

            return null;
        }

        public void Update(IEnumerable<ReactionItem> ris)
        {
            foreach (var ri in ris)
            {
                this[ri.Chemical] += ri;
            }
        }

        internal ReactionItem this[Chemical chem] 
        {
            get => _chems[chem.Id];
            set => _chems[chem.Id] = value;
        }
    }
}
