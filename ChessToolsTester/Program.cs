using System;

using ChessTools;

namespace ChessToolsTester
{
    class Program
    {
        static void Main(string[] args)
        {

            var games = PGNReader.read("C:/Users/Austin/source/repos/ChessBrowser/ChessBrowser/ChessToolsTester/Data/kb1.pgn");
            Console.WriteLine(games.Count);
            foreach (ChessGame game in games.GetRange(0, 10))
            {
                Console.WriteLine(game);

                Console.WriteLine("----------");
            }
        }
    }
}
