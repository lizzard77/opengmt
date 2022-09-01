namespace OpenGMT
{
    public class Creature
    {
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
        public int ArmorClass { get; set; } = 10;
        public int WalkingSpeed { get; set; } = 10;
        public string AdditionalSpeed { get; set; }

        public string SkillsText { get; set; }
        public string DamageResitances { get; set; }
        public string ConditionImmunities { get; set; }
        public string Senses { get; set; }
        public string Languages { get; set; }

        public string Challenge { get; set; }
        public int XP { get; set; }
        public int ProficiencyBonus { get; set; }

        public string SpecialTraits { get; set; }
        public string Actions { get; set; }

        public int Initiative { get; set; }
        public IDictionary<string,int> Abilities { get; set; }
        public IDictionary<string,int> Skills { get; set; }
        public IDictionary<string,int> Inventory { get; set; }
        public int[] Money { get; set; } = { 0,0,0,0,0 };

        public string Age { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string EyeColor { get; set; }
        public string SkinColor { get; set; }
        public string HairColor { get; set; }

    }

    public class Attack
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string HitModifier { get; set; }
        public string Damage { get; set; }
        public string DamageType { get; set; }
        public string Range { get; set; }
    }
}