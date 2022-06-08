namespace DTP_Assessment_2022
{
    public class Attack
    {
        public int selfDefense;
        public int selfHealth;
        public int selfAttack;
        public int enemyDefense;
        public int enemyHealth;
        public int enemyAttack;
        public int maxUses;
        public int uses;
        public string name;
        public Attack(int selfDefense, int selfHealth, int selfAttack, int enemyDefense, int enemyHealth, int enemyAttack, int maxUses, string name)
        {
            this.selfDefense = selfDefense;
            this.selfHealth = selfHealth;
            this.selfAttack = selfAttack;
            this.enemyDefense = enemyDefense;
            this.enemyHealth = enemyHealth;
            this.enemyAttack = enemyAttack;
            this.maxUses = maxUses;
            this.uses = maxUses;
            this.name = name;
        }
    }
}