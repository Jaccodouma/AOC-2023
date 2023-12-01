namespace Jacco.AOC.Helpers
{
    public static class Input
    {
        public static string Read(string path)
        {
            return File.ReadAllText(path);
        }

        public static string[] ReadLines(string path)
        {
            return File.ReadAllLines(path);
        }
    }
}