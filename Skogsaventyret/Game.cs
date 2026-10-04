using System;
namespace Skogsaventyret
{

    // Public class Game som interagerar med resten av projektet.

    public class Game
    {
        // Lista med platser i Göteborg som ska slumpas fram varje gång spelaren är ute på äventyr.
        private static string[] Platser =
        {
            "Du strosar runt i Majorna",
            "Du tar en promenad i Tingstadsvass",
            "Du beundrar landskapet i Partille",
            "Du promenerar på Linnégatan",
            "Du vandrar genom Nordstan",
            "Du promenerar genom Biskopsgården"
        };

        //Slumpgenerator som används för att generera en slumpmässig plats och slumpmässiga "monster".
        private Random slumpgenerator = new Random();

        //fältdeklarationer
        private Player player;
        private bool spelPågår; // Håller koll på om spelet fortfarande pågår.
        private int dag;        // Vilken dag i spelet vi är på.

        // Sätter upp startläget innan spelet börjar.
        public Game()
        {
            dag = 1;
            spelPågår = true;
        }

        // Startar spelet. Frågar efter spelarens namn och kör sedan
        // spelloopen (en dag i taget) tills spelPågår blir false.
        public void Starta()
        {
            Console.WriteLine("Hallå eller! Välkommen till GBG.");
            Console.Write("Vad heter du?:");
            string namn = Console.ReadLine();

            player = new Player(namn);

            while (spelPågår)
            {
                Promenad();
            }

            Console.WriteLine("Game Over gubben. Bättre lycka nästa gång.");
        }

        // Kör en dag i spelet och visar status. låter spelaren välja mellan att vila eller äventyra, och kollar om spelaren dog.
        private void Promenad()
        {
            Console.WriteLine($"\n--- Dag {dag} ---");
            Console.WriteLine($"{player.Namn} | HP: {player.Hp}/{player.MaxHp} | Level: {player.Level} | XP: {player.Xp}");
            Console.WriteLine("Vad vill du göra?");
            Console.WriteLine("1) Ta en Öl i Kvillebäcken");
            Console.WriteLine("2) Strosa på stan (äventyra)");

            string val = Console.ReadLine();

            // Beroende på vad spelaren skrev in körs olika saker.
            switch (val)
            {
                case "1":
                    Vila();
                    break;
                case "2":
                    Äventyra();
                    break;
                default:
                    // Om spelaren skrev något annat än 1 eller 2, be dem försöka igen.
                    Console.WriteLine("Vafan sägeru? Försök igen.");
                    return;
            }

            dag++; // Nästa dag har kommit.
            player.ÖverlevDag(); // Spelaren har överlevt ännu en dag.

            // Om spelaren är helt slut (0 HP eller mindre) är spelet över.
            if (player.Hp <= 0)
            {
                Console.WriteLine($"{player.Namn} klarade inte av trycket... Game over gubben.");
                spelPågår = false;
            }
        }

        // Spelaren vilar och återhämtar halva sin HP
        private void Vila()
        {
            Console.WriteLine($"{player.Namn} sover ut hemma i Vassen och känner sig pigg.");
            player.Hela(player.MaxHp / 2);
        }

        // Spelaren ger sig ut på äventyr på en slumpad plats. ett slumpat monster dyker upp och de slåss mot varandra.
        // Om spelaren vinner får de erfarenhetspoäng (XP)
        private void Äventyra()
        {
            //vi slumpmässar fram en plats
            string plats = Platser[slumpgenerator.Next(Platser.Length)];
            Console.WriteLine($"{plats}...");

            // vi skapar ett slumpat monster att slåss mot
            Monster monster = Monsterfabrik.SkapaSlumpatMonster();
            Console.WriteLine($"En {monster.Namn} dyker upp! {monster.Beskrivning}");

            // starta striden och se om spelaren vinner eller förlorar
            Strid strid = new Strid(player, monster);
            bool spelarenVann = strid.Kör();

            if (spelarenVann)
            {
                Console.WriteLine($"{player.Namn} vann och fick {monster.XpBelöning} XP!");
                player.FåXp(monster.XpBelöning);
            }
        }

