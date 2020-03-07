using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LOH
{
    public class Monster
    {
        [Key]
        public int MonsterID { get; set; }

        public string Name { get; set; }

        public int Gold { get; set; }

        public int Experience { get; set; }

        public int Level { get; set; }

        public int Health { get; set; }

        public int Damage { get; set; }

        public int Defense { get; set; }

        public int Mana { get; set; }

    }
}
