using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ChessTools
{
    public class ChessGame
    {
        public string Event;
        public string Site;
        public DateTime EventDate;
        public string Round;
        public string White;
        public string Black;
        public uint WhiteElo;
        public uint BlackElo;
        public char Result;
        public string Moves;

        public override string ToString()
        {
            string gameInfo = "";

            gameInfo += $"Event: {Event}\n";
            gameInfo += $"Site: {Site}\n";
            gameInfo += $"EventDate: {EventDate}\n";
            gameInfo += $"Round: {Round}\n";
            gameInfo += $"White: {White}\n";
            gameInfo += $"Black: {Black}\n";
            gameInfo += $"WhiteElo: {WhiteElo}\n";
            gameInfo += $"BlackElo: {BlackElo}\n";
            gameInfo += $"Result: {Result}\n";
            gameInfo += $"Moves: {Moves}\n";

            return gameInfo;
        }

    }

    public class PGNReader
    {
        public List<ChessGame> read(string filename)
        {
            var chessGames = new List<ChessGame>();
            ChessGame chessGame = null;

            string[] lines = System.IO.File.ReadAllLines(filename);

            Regex rx = new Regex(@"\[(?<tag>\w+).+""(?<value>.+)""]",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);

            bool inMovesSection = false;


            foreach (string line in lines)
            {
                // Find matches.
                MatchCollection matches = rx.Matches(line);

                string tag = "";
                string value = "";


                if (matches.Count == 1)
                {
                    GroupCollection groups = matches[0].Groups;
                    tag = groups["tag"].Value;
                    value = groups["value"].Value;
                } else
                {
                    if (line.Length == 0)
                    {
                        inMovesSection = !inMovesSection;
                    }
                    else if (line.Length > 0 && inMovesSection)
                    {
                        if (chessGame.Moves == null)
                        {
                            chessGame.Moves = "";
                        }

                        chessGame.Moves += line;
                    }

                }

                switch (tag) {
                    case "Event":
                        if (chessGame != null)
                        {
                            chessGames.Add(chessGame);
                        }

                        chessGame = new ChessGame();
                        chessGame.Event = value;
                        break;
                    case "Site":
                        chessGame.Site = value;
                        break;
                    case "EventDate":
                        chessGame.EventDate = DateTime.Parse(value);
                        break;
                    case "Round":
                        chessGame.Round = value;
                        break;
                    case "White":
                        chessGame.White = value;
                        break;
                    case "Black":
                        chessGame.Black = value;
                        break;
                    case "WhiteElo":
                        chessGame.WhiteElo = uint.Parse(value);
                        break;
                    case "BlackElo":
                        chessGame.BlackElo = uint.Parse(value);
                        break;
                    case "Result":
                        switch (value)
                        {
                            case "1-0":
                                chessGame.Result = 'W';
                                break;
                            case "0-1":
                                chessGame.Result = 'B';
                                break;
                            case "1/2-1/2":
                                chessGame.Result = 'D';
                                break;
                        }
                        break;
                    default:
                        break;
                }

            }

            if (chessGame != null)
            {
                chessGames.Add(chessGame);
            }


            return chessGames;
        }
    }
}
