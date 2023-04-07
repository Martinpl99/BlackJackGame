using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackGame
{
    public struct CardStruct
    {
        public StringBuilder Builder { get; set; }
        public List<List<string>> Builder_list { get; set; }
        public List<Tuple<string,int>> Cards_1 { get; set; }
        public int NumberOfCards { get; set; }
        public int Value { get; set; }
    }
}
