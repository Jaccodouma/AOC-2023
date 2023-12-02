namespace Jacco.AOC
{
    public class WelcomeMessage
    {
        private static int StarChance = 75;
        private static char[] Stars = new char[] { '*' };

        public static void PrintTitle(string title, string subtitle) {
            int maxWidth = title.Length > subtitle.Length ? title.Length : subtitle.Length;
            int padding = (maxWidth - title.Length) / 2;
            
            Console.ForegroundColor = ConsoleColor.Red;

            // Write top bar
            Console.Write("╔");
            for (int i = 0; i < maxWidth + padding + 2; i++) {
                Console.Write("═");
            }
            Console.WriteLine("╗");
        }

        public static void Show() {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("╔═══════════════════════════════════╗");
            Console.Write("║");
            PotentialStarLine(35);
            Console.WriteLine("║");
            Console.WriteLine("║  Jacco's Advent Of Code attempt!  ║");
            Console.Write("║");
            PotentialStarLine(35);
            Console.WriteLine("║");
            Console.WriteLine("╟───────────────────────────────────╢");
            Console.WriteLine("║        Happy Hollidays! <3        ║");
            Console.WriteLine("╚═══════════════════════════════════╝");

            Console.ResetColor();
        }

        public static void PotentialStarLine(int length) {
            for (int i = 0; i < length; i++) {
                PotentialStar();
            }
        }

        private static int lastStar = 0;
        public static void PotentialStar() {
            var oldColour = Console.ForegroundColor;
            int r = new Random().Next(0, 100);
            // Randomly print a star, or not
            if (r > StarChance && lastStar > 0) {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(Stars[new Random().Next(0, Stars.Length)]);
                Console.ResetColor();
                lastStar = 0;
            } else {
                Console.Write(" ");
                lastStar++;
            }
            Console.ForegroundColor = oldColour;
        }
    }
}
