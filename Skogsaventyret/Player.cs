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

        // Hur mycket XP som krävs för att nå nästa level.
        // Vi gör den lite högre för varje level (level 1 -> 2 kräver 100, level 2 -> 3 kräver 200, osv).
        private int XpFörNästaLevel => Level * 100;

        // Konstruktor: körs när en ny spelare skapas. Sätter startvärden.
        public Spelare(string namn)
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
    }
}