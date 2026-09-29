using System;

namespace Skogsaventyret
{
    // Klass som hanterar striden mellan spelaren och monstret
    public class Strid
    {
        // Spelaren och monstret som slåss
        private Spelare spelare;
        private Monster monster;

        // Används när vi behöver slumpa fram skada
        private Random slumpgenerator = new Random();

        // Tar emot spelaren och monstret när striden startar
        public Strid(Spelare spelare, Monster monster)
        {
            this.spelare = spelare;
            this.monster = monster;
        }

        // Kör själva striden
        public bool Kör()
        {
            // Fortsätter så länge båda har HP kvar
            while (spelare.Hp > 0 && monster.Hp > 0)
            {
                Console.WriteLine("\n--- STRID ---");

                // Visar HP för båda
                Console.WriteLine(
                    $"{spelare.Namn}: {spelare.Hp}/{spelare.MaxHp} HP"
                );

                Console.WriteLine(
                    $"{monster.Namn}: {monster.Hp} HP"
                );

                // Spelaren väljer vad den vill göra
                Console.WriteLine("\nVad vill du göra?");
                Console.WriteLine("1) Försvara");
                Console.WriteLine("2) Anfall");
                Console.WriteLine("3) Spring");

                string val = Console.ReadLine();

                switch (val)
                {
                    case "1":
                        Försvara();
                        break;

                    case "2":
                        Anfall();
                        break;

                    case "3":
                        Spring();
                        return false;

                    default:
                        Console.WriteLine("Välj 1, 2 eller 3.");
                        continue;
                }
            }

            // Returnerar true om spelaren vann
            return spelare.Hp > 0;
        }

        // Försvara: spelaren tar halva monstrets attackskada
        private void Försvara()
        {
            int skada = monster.Attack / 2;

            spelare.Hp -= skada;

            Console.WriteLine(
                $"{spelare.Namn} försvarar sig och tar {skada} skada!"
            );

            VisaHp();
        }

        // Anfall: spelarens attack minus monstrets försvar
        private void Anfall()
        {
            int skada = spelare.Attack - monster.Forsvar;

            // Spelaren gör alltid minst 1 skada
            if (skada < 1)
            {
                skada = 1;
            }

            monster.Hp -= skada;

            Console.WriteLine(
                $"{spelare.Namn} anfaller och gör {skada} skada!"
            );

            // Monstret attackerar tillbaka om det fortfarande lever
            if (monster.Hp > 0)
            {
                spelare.Hp -= monster.Attack;

                Console.WriteLine(
                    $"{monster.Namn} attackerar tillbaka och gör " +
                    $"{monster.Attack} skada!"
                );
            }

            VisaHp();
        }

        // Spring: spelaren tar slumpmässig skada
        private void Spring()
        {
            // Slumpar skada mellan 1 och monstrets attack
            int skada = slumpgenerator.Next(1, monster.Attack + 1);

            spelare.Hp -= skada;

            Console.WriteLine(
                $"{spelare.Namn} springer iväg men tar {skada} skada!"
            );

            VisaHp();
        }

        // Visar hur mycket HP spelaren och monstret har kvar
        private void VisaHp()
        {
            Console.WriteLine(
                $"HP kvar: {spelare.Namn}: {spelare.Hp} | " +
                $"{monster.Namn}: {monster.Hp}"
            );
        }
    }
}