using System;
using System.Collections.Generic;

namespace JakubSturc.AdventOfCode2019.Day7
{
    public class IntComp
    {
        public IEnumerator<int> Input { get; }

        public int IP { get; private set; }

        public ImmediateMemory Imm { get; }
        public PositionMemory Pos { get; }

        public int OppCode { get => Imm[IP]; }
        public int Imm1 { get => Imm[IP + 1]; }
        public int Imm2 { get => Imm[IP + 2]; }
        public int Imm3 { get => Imm[IP + 3]; }

        public int Pos1 { get => Pos[IP + 1]; set => Pos[IP + 1] = value; }
        public int Pos2 { get => Pos[IP + 2]; set => Pos[IP + 2] = value; }
        public int Pos3 { get => Pos[IP + 3]; set => Pos[IP + 3] = value; }


        public IntComp(int[] program, IEnumerable<int> input)
        {
            Imm = new ImmediateMemory(program);
            Pos = new PositionMemory(program);

            Input = input.GetEnumerator();
            IP = 0;
        }

        public IEnumerable<int> Run()
        {
            while (true)
            {
                switch (OppCode)
                {
                    case 0001: { Pos3 = Pos1 + Pos2; IP += 4; break; }
                    case 1001: { Pos3 = Pos1 + Imm2; IP += 4; break; }
                    case 0101: { Pos3 = Imm1 + Pos2; IP += 4; break; }
                    case 1101: { Pos3 = Imm1 + Imm2; IP += 4; break; }

                    case 0002: { Pos3 = Pos1 * Pos2; IP += 4; break; }
                    case 1002: { Pos3 = Pos1 * Imm2; IP += 4; break; }
                    case 0102: { Pos3 = Imm1 * Pos2; IP += 4; break; }
                    case 1102: { Pos3 = Imm1 * Imm2; IP += 4; break; }

                    case 03: { Input.MoveNext();  Pos1 = Input.Current; IP += 2; break; }

                    case 004: { yield return Pos1; IP += 2; break; }
                    case 104: { yield return Imm1; IP += 2; break; }

                    case 0005: { if (Pos1 != 0) IP = Pos2; else IP += 3; break; }
                    case 0105: { if (Imm1 != 0) IP = Pos2; else IP += 3; break; }
                    case 1005: { if (Pos1 != 0) IP = Imm2; else IP += 3; break; }
                    case 1105: { if (Imm1 != 0) IP = Imm2; else IP += 3; break; }

                    case 0006: { if (Pos1 == 0) IP = Pos2; else IP += 3; break; }
                    case 0106: { if (Imm1 == 0) IP = Pos2; else IP += 3; break; }
                    case 1006: { if (Pos1 == 0) IP = Imm2; else IP += 3; break; }
                    case 1106: { if (Imm1 == 0) IP = Imm2; else IP += 3; break; }

                    case 00007: { Pos3 = Pos1 < Pos2 ? 1 : 0; IP += 4; break; }
                    case 00107: { Pos3 = Imm1 < Pos2 ? 1 : 0; IP += 4; break; }
                    case 01007: { Pos3 = Pos1 < Imm2 ? 1 : 0; IP += 4; break; }
                    case 01107: { Pos3 = Imm1 < Imm2 ? 1 : 0; IP += 4; break; }

                    case 00008: { Pos3 = Pos1 == Pos2 ? 1 : 0; IP += 4; break; }
                    case 00108: { Pos3 = Imm1 == Pos2 ? 1 : 0; IP += 4; break; }
                    case 01008: { Pos3 = Pos1 == Imm2 ? 1 : 0; IP += 4; break; }
                    case 01108: { Pos3 = Imm1 == Imm2 ? 1 : 0; IP += 4; break; }

                    case 99: yield break;

                    default: throw new NotSupportedException();
                }
            }
        }
    }
}
