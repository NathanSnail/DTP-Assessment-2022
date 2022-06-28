namespace DTP_Assessment_2022
{
    public class Dino
    {
        //health change after attack = dmg * ln(attack) - defense
        //special case if dmg < 0 ie healing move
        public int maxHealth;
        public float health;
        public float defense;
        public float attack;
        public string name;
        public Attack[] attacks = new Attack[4];
        public Dino(int maxHealth, float attack, float defense, Attack[] attacks, string name)
        {
            this.maxHealth = maxHealth;
            this.health = maxHealth;
            this.attack = attack;
            this.defense = defense;
            this.attacks = attacks;
            this.name = name;
        }
        public void takeDamage(float amount) => health -= amount > 0 ? Math.Max(amount - defense, 0) : amount;
        public Dino MakeClone() => new Dino(maxHealth, attack, defense, new Attack[]
        { attacks[0].makeClone(), attacks[1].makeClone(), attacks[2].makeClone(), attacks[3].makeClone() },
        name);
    }
}