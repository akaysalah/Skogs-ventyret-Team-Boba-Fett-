using System;

namespace Skogsaventyret
{
    // player representeras i form av namn, hälsa, stridsvärden, level/XP och hur många dagar hen har överlevt.
    public class Player
    {
        public string Namn { get; private set; }

        public int Hp { get; private set; }
        public int MaxHp { get; private set; }

        public int Attack { get; private set; }
        public int Försvar { get; private set; }

        public int Level { get; private set; }
        public int Xp { get; private set; }

        public int DagarÖverlevda { get; private set; }

        // hur mycket XP som krävs för att nå nästa level.kraven blir lite högre för varje level (level 1 -> 2 kräver 100, level 2 -> 3 kräver 200, osv).
        private int XpFörNästaLevel => Level * 100;

        // konstruktor: körs när en ny player skapas. Sätter startvärden.
        public Player(string namn)
        {
            Namn = namn;

            MaxHp = 100;
            Hp = MaxHp;

            Attack = 10;
            Försvar = 5;

            Level = 1;
            Xp = 0;

            DagarÖverlevda = 0;
        }

        // spelaren tar skada. HP kan aldrig gå under 0.
        public void TaSkada(int mängd)
        {
            if (mängd < 0)
            {
                mängd = 0;
            }

            Hp -= mängd;

            if (Hp < 0)
            {
                Hp = 0;
            }
        }

        // spelaren återfår HP tillexempel genom vila. HP kan aldrig gå över MaxHp.
        public void Hela(int mängd)
        {
            if (mängd < 0)
            {
                mängd = 0;
            }

            Hp += mängd;

            if (Hp > MaxHp)
            {
                Hp = MaxHp;
            }
        }

        // spelaren får erfarenhet (XP), t.ex. efter att ha besegrat ett monster och om spelaren samlat på sig tillräckligt mycket XP så levlar hen upp.
        // om man samlar på sig väldigt mycket XP på en gång kan man levla upp flera gånger.
        public void FåXp(int mängd)
        {
            if (mängd < 0)
            {
                mängd = 0;
            }

            Xp += mängd;

            while (Xp >= XpFörNästaLevel)
            {
                Xp -= XpFörNästaLevel;
                LevlaUpp();
            }
        }

        // spelaren blir starkare: högre level, mer max-HP, attack och försvar.
        // spelaren blir också fullt återställd (helad) när hen levlar upp.
        public void LevlaUpp()
        {
            Level++;

            MaxHp += 20;
            Attack += 3;
            Försvar += 2;

            Hp = MaxHp;

            Console.WriteLine($"{Namn} klev upp till level {Level}!");
        }

        // räknar upp antalet dagar spelaren har överlevt. kallas en gång per dag som klaras av.
        public void ÖverlevDag()
        {
            DagarÖverlevda++;
        }
    }
}
