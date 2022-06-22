namespace DTP_Assessment_2022
{
    public class Attack
    {
        public int selfDefense;
        public int selfAttack;
        public int enemyDefense;
        public int enemyAttack;
        public int maxUses;
        public int uses;
        public string name;
        public Attack(int selfDefense, int selfAttack, int enemyDefense, int enemyAttack, int maxUses, string name)
        {
            this.selfDefense = selfDefense;
            this.selfAttack = selfAttack;
            this.enemyDefense = enemyDefense;
            this.enemyAttack = enemyAttack;
            this.maxUses = maxUses;
            this.uses = maxUses;
            this.name = name;
        }
    }
}