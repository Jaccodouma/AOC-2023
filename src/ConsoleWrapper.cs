namespace Jacco.AOC
{
    public class ConsoleWrapper
    {
        private static int StarChance = 75;
        private static char[] Stars = new char[] { '*' };

        private enum BarType
        {
            Top,
            Middle,
            MiddleThin,
            Bottom
        }

        private static void PrintHorizontalBar(int length, BarType barType)
        {
            // Set left based on bar type
            string left = "╠"; 
            string right = "╣"; 
            string middle = "═";

            switch (barType)
            {
                case BarType.Top:
                    left = "╔";
                    right = "╗";
                    middle = "═";
                    break;
                case BarType.MiddleThin:
                    left = "╟";
                    right = "╢";
                    middle = "─";
                    break;
                case BarType.Bottom:
                    left = "╚";
                    right = "╝";
                    break;
                default:
                    break;
            }

            Console.Write(left);
            for (int i = 0; i < length; i++)
            {
                Console.Write(middle);
            }
            Console.WriteLine(right);
        }

        public static void PrintTitle(string title, string subtitle)
        {
            int maxWidth = title.Length > subtitle.Length ? title.Length : subtitle.Length;
            int padding = maxWidth / 4;

            Console.ForegroundColor = ConsoleColor.Red;

            PrintHorizontalBar(maxWidth + 2*padding, BarType.Top);

            // Write title
            Console.Write("║");
            for (int i = 0; i < padding; i++)
            {
                Console.Write(" ");
            }
            Console.Write(title);
            for (int i = 0; i < padding; i++)
            {
                Console.Write(" ");
            }
            Console.WriteLine("║");

            PrintHorizontalBar(maxWidth + 2*padding, BarType.MiddleThin);

            PrintHorizontalBar(maxWidth + 2*padding, BarType.Bottom);
        }

        public static void PotentialStarLine(int length)
        {
            for (int i = 0; i < length; i++)
            {
                PotentialStar();
            }
        }

        private static int lastStar = 0;
        public static void PotentialStar()
        {
            var oldColour = Console.ForegroundColor;
            int r = new Random().Next(0, 100);
            // Randomly print a star, or not
            if (r > StarChance && lastStar > 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(Stars[new Random().Next(0, Stars.Length)]);
                lastStar = 0;
            }
            else
            {
                Console.Write(" ");
                lastStar++;
            }
            Console.ForegroundColor = oldColour;
        }
    }
}
