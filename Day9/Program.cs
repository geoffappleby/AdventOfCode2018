using System;
using System.Collections.Generic;
using System.Linq;
using AdventUtilities;

namespace Day9
{
    class Program
    {
        static void Main(string[] args)
        {

            //other examples
            // 9 players; last marble is worth   25 points: high score is 32
            //10 players; last marble is worth 1618 points: high score is 8317
            //13 players; last marble is worth 7999 points: high score is 146373
            //17 players; last marble is worth 1104 points: high score is 2764
            //21 players; last marble is worth 6111 points: high score is 54718
            //30 players; last marble is worth 5807 points: high score is 37305
            var games = new List<(long p, long m, long s)>
            {
                (9, 25, 32),
                (10, 1618, 8317),
                (13, 7999, 146373),
                (17, 1104, 2764),
                (21, 6111, 54718),
                (30, 5807, 37305)
            };
            //Goal to answer: 416 players; last marble is worth 71617 points

            bool doprint = true;
            foreach (var valueTuple in games)
            {
                Console.WriteLine($"Running game with values {valueTuple.p} players, {valueTuple.m} final marble, expecting a high score of {valueTuple.s}");
                var (player, highScore) = PlayGame(valueTuple.p, valueTuple.m, doprint);
                Console.WriteLine($"The high score was {highScore}, won by player {player}.");
                if (highScore != valueTuple.s)
                {
                    var oldColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR. THIS DOES NOT MATCH.");
                    Console.ForegroundColor = oldColor;
                }

                doprint = false;
            }

            Console.WriteLine($"And now for the final one....");
            var (playerx, highScorex) = PlayGame(416, 7161700, doprint);
            Console.WriteLine($"The high score was {highScorex}, won by player {playerx}.");

            Helper.Pause();
            
        }

        /// <summary>
        /// Play the game
        /// </summary>
        /// <param name="playerCount">The number of players playing</param>
        /// <param name="lastMarble">How many marbles</param>
        /// <param name="printMoves">flag to say ig it should print each move or not</param>
        /// <returns>The highest score of any of the players once the game has completed</returns>
        public static (long player, long score) PlayGame(long playerCount, long lastMarble, bool printMoves = false)
        {
            //let's start with marble 0; it's a single marble circle.
            var currentMarble = new Marble(0);
            var firstMarble = currentMarble;
            var players = CreatePlayers(playerCount);
            long currentPlayer = 0;

            if (printMoves)
            {
                printCircle(null, firstMarble, currentMarble);
            }

            //for each marble, in order
            for (long marbleNumber = 1; marbleNumber <= lastMarble; marbleNumber++)
            {
                //get the next marble
                var nextMarble = new Marble(marbleNumber);
                if (nextMarble.IsScoringMarble)
                {
                    //do the scoring logic
                    players[currentPlayer].OwnedMarbles.Add(nextMarble);
                    nextMarble = GetMarblesBack(currentMarble, 7);
                    currentMarble = nextMarble.Next;
                    nextMarble = nextMarble.Previous.RemoveNext();
                    players[currentPlayer].OwnedMarbles.Add(nextMarble);
                }
                else
                {
                    currentMarble.Next.Insert(nextMarble);
                    currentMarble = nextMarble;
                }

                if (printMoves)
                {
                    printCircle(players[currentPlayer], firstMarble, currentMarble);
                }

                //find the next player
                currentPlayer = GetNextPlayerIndex(currentPlayer, playerCount);
            }

            //game over, who's got the highest score?
            var highscore =  players.Max(x => x.OwnedMarbles.Sum(y => y.Score));
            var winner = players.First(x => x.MarbleScore == highscore).Number;

            return (winner, highscore);

        }

        /// <summary>
        /// print what the circle looks like right now
        /// </summary>
        /// <param name="player">The player who just made a turn</param>
        /// <param name="firstMarble">The first marble in the circle.</param>
        /// <param name="currentMarble">The current active marble</param>
        private static void printCircle(Player player, Marble firstMarble, Marble currentMarble)
        {
            if (player == null)
            {
                Console.Write("[---] ");
            }
            else
            {
                Console.Write($"[{player.Number:000}] ") ;
            }

            var printingMarble = firstMarble;

            do
            {
                if (printingMarble == currentMarble)
                {
                    Console.Write($"({printingMarble.Score})");
                }
                else
                {
                    Console.Write($" {printingMarble.Score} ");
                }

                printingMarble = printingMarble.Next;
            } while (printingMarble != firstMarble);

            Console.WriteLine(String.Empty);
        }

        /// <summary>
        /// Return the marble a certain number of marbles previous to this one
        /// </summary>
        /// <param name="currentMarble">The marble to count back from</param>
        /// <param name="thisManyBack">The number of marbles to count back</param>
        /// <returns>The marble</returns>
        private static Marble GetMarblesBack(Marble currentMarble, long thisManyBack)
        {
            Marble ret = currentMarble;
            for (long i = 0; i < thisManyBack; i++)
            {
                ret = ret.Previous;
            }

            return ret;
        }

        //increment which player is playing. reset back to zero when at the max
        private static long GetNextPlayerIndex(long lastPlayerIndex, long playerCount)
        {
            lastPlayerIndex++;
            if (lastPlayerIndex == playerCount)
            {
                lastPlayerIndex = 0;
            }

            return lastPlayerIndex;
        }

        /// <summary>
        /// Generate the required set of players.
        /// </summary>
        /// <param name="playerCount">The number of players to create</param>
        /// <returns>The players</returns>
        private static Player[] CreatePlayers(long playerCount)
        {
            var players = new Player[playerCount];
            for (long i = 0; i < playerCount; i++)
            {
                players[i] = new Player(i + 1);
            }

            return players;
        }
    }
}
