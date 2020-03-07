using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LOH
{
    public class Battle
    {
        [Key]
        public int BattleID { get; set; }
        public int PlayerID { get; set; }
        public int Turn { get; set; }
        public Monster Monster { get; set; }
    }
}
