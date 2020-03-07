using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LOH
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlayerID { get; set; }

        public int Gold { get; set; } = 0;

        public int Experience { get; set; } = 0;

        public int Level { get; set; } = 0;

        public int Health { get; set; } = 100;

        public int Damage { get; set; } = 5;

        public int Defense { get; set; } = 5;

        public int Mana { get; set; } = 5;

        public string PlayerName { get; set; } = "Toshi";

        public DateTime LastWorkTime { get; set; } = DateTime.MinValue;

        public Battle Battle { get; set; } = null;
    }
}
