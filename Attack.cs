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
        //dereference object
        public Attack MakeClone()
        {
            return new Attack(selfDefense, selfAttack, enemyDefense, enemyAttack, maxUses, name);
        }
        //run an attack
        public void UseAttack(float multiplier, Dino selfDino, Dino enemyDino)
        {
            //amount > 0 ? amount * (float)Math.Log(Math.Max(attack,0)+2) : amount;
            if (uses <= 0)
            {
                return;
            }
            uses -= 1;
            float enemyDamage = enemyAttack > 0 ? enemyAttack * (float)Math.Log(Math.Max(selfDino.attack, 0) + 2) : enemyAttack;
            enemyDino.TakeDamage(enemyDamage > 0 ? enemyDamage * multiplier : enemyDamage / multiplier);
            selfDino.TakeDamage(selfAttack > 0 ? selfAttack / multiplier : selfAttack * multiplier);
            enemyDino.defenseModifier -= enemyDefense > 0 ? enemyDefense * multiplier : enemyDefense / multiplier;
            selfDino.defenseModifier -= selfDefense > 0 ? selfDefense / multiplier : selfDefense * multiplier;
        }
    }
}