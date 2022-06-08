namespace DTP_Assessment_2022
{
    public abstract class Dino
    {
        //health change after attack = dmg * ln(attack) - defense
        //special case if dmg < 0 ie healing move
        public int maxHealth;
        public float health;
        public int defense;
        public int attack;
        public string name;
        public Attack[] attacks = new Attack[4];
        public Dino(int maxHealth, int attack, int defense, Attack[] attacks, string name)
        {
            this.maxHealth = maxHealth;
            this.health = maxHealth;
            this.attack = attack;
            this.defense = defense;
            this.attacks = attacks;
            this.name = name;
        }
        public void takeDamage(float amount) => health -= amount > 0 ? Math.Max(amount - defense,0) : amount;
        public float getDamage(float amount) => amount > 0 ? amount * (float)Math.Log(Math.Max(attack,0)+2) : amount;
    }
}