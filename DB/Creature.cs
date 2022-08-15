using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OpenGMT
{
    public class Creature
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
        public IList<Attack> Attacks { get; set; }
        public int HPMax { get; set; }
        public int HPTemporary { get; set; }
        public int Damage { get; set; }
        public int Size { get; set; }
        public int Reach { get; set; }
        public string Color { get; set; }
        public string Type { get; set; }
        public string SheetURL { get; set; }

    }

    public class Attack
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string HitModifier { get; set; }
        public string Damage { get; set; }
        public string DamageType { get; set; }
        public string Range { get; set; }
        public Creature Creature { get; internal set; }
    }
}