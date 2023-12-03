namespace Jacco.AOC
{
    public class ConsoleWrapper
    {
        private static int StarChance = 50;
        private static char[] Stars = new char[] { '*', '\'', '.' };

        private static ConsoleColor[] potentialStarColours = new ConsoleColor[] {
            ConsoleColor.Yellow,
            ConsoleColor.Red,
            ConsoleColor.Green,
            ConsoleColor.Blue,
            ConsoleColor.Magenta,
            ConsoleColor.Cyan,
            ConsoleColor.White
        };

        private enum BarType
        {
            Top,
            Middle,
            MiddleThin,
            Bottom
        }

        private static int lastStar = 0;

        private static void PrintPotentialStar() {
            var oldColour = Console.ForegroundColor;
            int r = new Random().Next(0, 100);
            // Randomly print a star, or not
            if (lastStar > 0 && r <= StarChance) {
                Console.ForegroundColor = potentialStarColours[new Random().Next(0, potentialStarColours.Length)];
                Console.Write(Stars[new Random().Next(0, Stars.Length)]);
                lastStar = 0;
            } else {
                Console.Write(" ");
                lastStar++;
            }
            Console.ForegroundColor = oldColour;
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

        private static void PrintEmptyLine(int length)
        {
            Console.Write("║");
            for (int i = 0; i < length; i++)
            {
                PrintPotentialStar();
                // Console.Write(" ");
            }
            Console.WriteLine("║");
        }

        public static void PrintTitle(string title, string subtitle)
        {
            // Pad title and subtitle with a space (for the stars)
            title = $" {title} ";
            subtitle = $" {subtitle} ";

            int maxWidth = title.Length > subtitle.Length ? title.Length : subtitle.Length;
            int padding = maxWidth / 4;
            int width = maxWidth + 2 * padding;

            Console.ForegroundColor = ConsoleColor.Red;

            PrintHorizontalBar(width, BarType.Top);
            PrintEmptyLine(width);

            // Write title
            Console.Write("║");
            for (int i = 0; i < padding; i++)
            {
                PrintPotentialStar();
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(title);
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < padding; i++)
            {
                PrintPotentialStar();
            }
            Console.WriteLine("║");

            PrintEmptyLine(width);
            PrintHorizontalBar(width, BarType.MiddleThin);

            // Write subtitle
            int subtitlePadding = (width - subtitle.Length) / 2;
            Console.Write("║");
            for (int i = 0; i < subtitlePadding; i++)
            {
                PrintPotentialStar();
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(subtitle);
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < width - subtitlePadding - subtitle.Length; i++)
            {
                PrintPotentialStar();
            }
            Console.WriteLine("║");

            PrintHorizontalBar(width, BarType.Bottom);
        }
    }
}
