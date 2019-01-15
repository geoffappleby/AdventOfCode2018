using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using AdventUtilities;

namespace Day13
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = Helper.LoadAllFromFile(@"C:\Users\geoff.appleby\source\repos\AdventOfCode\Day13\inputs.txt");

            var (tracks, carts) = LoadData(data);
            Helper.Pause();
            var generation = 1;
            var cartCount = carts.Count;
            var nextCartCount = -1;
            while (true)
            {
                var (x, y) = RunTheMadness(tracks, carts, generation++);
                nextCartCount = carts.Count;
                if (nextCartCount != cartCount)
                {
                    cartCount = nextCartCount;
                    Console.WriteLine($"There are {cartCount} carts left...");
                }

                if (generation % 10000 == 0)
                {
                    Console.WriteLine($"Moved {generation} times....");
                    //PrintTracks(tracks);
                }
                //PrintTracks(tracks);
                if (x != null)
                {
                    PrintTracks(tracks);
                    Console.WriteLine($"Last one at ({y},{x})");
                    break;
                }
            }
            Helper.Pause();

        }

        public static (int?, int?) RunTheMadness(Track[,] tracks, List<Cart> carts, int generation, bool oneMore = false)
        {
            (int?, int?) collidedAt = (null, null);

            for (var x = 0; x < tracks.GetLength(0); x++)
            {
                for (var y = 0; y < tracks.GetLength(1); y++)
                {

                    var track = tracks[x, y];
                    var cart = tracks[x, y]?.Cart;
                    if (track != null && cart != null && cart.Generation != generation)
                    {
                        Track nextTrack = null;
                        switch (cart.CurrentDirection)
                        {
                            //moving which way? possible: right left up down
                            case Direction.Right:
                                //if we're facing right we can only be on an orientation of horizontal, upleft, upright, intersection
                                //in all cases we move to the next track the same. but depending on what type it is, we might do something different after that
                                nextTrack = tracks[x, y + 1];

                                if (nextTrack.Cart != null && (nextTrack.Cart.Generation == generation || (nextTrack.Cart.Generation == generation-1 && nextTrack.Cart.CurrentDirection == Direction.Left)))
                                {
                                    Cart nextCart = nextTrack.Cart;

                                    //instead of recording the collision, delete the carts
                                    if (carts.Contains(cart))
                                    {
                                        carts.Remove(cart);
                                    }
                                    cart.track = null;
                                    track.Cart = null;
                                    cart = null;

                                    if (carts.Contains(nextCart))
                                    {
                                        carts.Remove(nextCart);
                                    }
                                    nextCart.track = null;
                                    nextTrack.Cart = null;
                                    nextCart = null;
                                    continue;
                                }

                                nextTrack.Cart = cart;
                                cart.track = nextTrack;
                                track.Cart = null;

                                switch (nextTrack.Orientation)
                                {
                                    case TrackOrientation.UpLefty:
                                        cart.CurrentDirection = Direction.Down;
                                        break;
                                    case TrackOrientation.UpRighty:
                                        cart.CurrentDirection = Direction.Up;
                                        break;
                                    case TrackOrientation.Intersection:
                                        //workout which way to turn. no change if it's straight.
                                        switch (cart.NextTurn)
                                        {
                                            case Turn.Left:
                                                cart.CurrentDirection = Direction.Up;
                                                break;
                                            case Turn.Right:
                                                cart.CurrentDirection = Direction.Down;
                                                break;
                                        }

                                        cart.MakeTurn();
                                        break;

                                }

                                break;
                            case Direction.Left:
                                //if we're facing left we can only be on an orientation of horizontal, uploeft, upright, intersection
                                //in all cases we move to the next track the same. but depending on what type it is, we might do something different after that
                                nextTrack = tracks[x, y - 1];

                                if (nextTrack.Cart != null && (nextTrack.Cart.Generation == generation || (nextTrack.Cart.Generation == generation - 1 && nextTrack.Cart.CurrentDirection == Direction.Right)))
                                {
                                    Cart nextCart = nextTrack.Cart;

                                    //instead of recording the collision, delete the carts
                                    if (carts.Contains(cart))
                                    {
                                        carts.Remove(cart);
                                    }
                                    cart.track = null;
                                    track.Cart = null;
                                    cart = null;

                                    if (carts.Contains(nextCart))
                                    {
                                        carts.Remove(nextCart);
                                    }
                                    nextCart.track = null;
                                    nextTrack.Cart = null;
                                    nextCart = null;
                                    continue;
                                }

                                nextTrack.Cart = cart;
                                cart.track = nextTrack;
                                track.Cart = null;
                                switch (nextTrack.Orientation)
                                {
                                    case TrackOrientation.UpLefty:
                                        cart.CurrentDirection = Direction.Up;
                                        break;
                                    case TrackOrientation.UpRighty:
                                        cart.CurrentDirection = Direction.Down;
                                        break;
                                    case TrackOrientation.Intersection:
                                        //workout which way to turn. no change if it's straight.
                                        switch (cart.NextTurn)
                                        {
                                            case Turn.Left:
                                                cart.CurrentDirection = Direction.Down;
                                                break;
                                            case Turn.Right:
                                                cart.CurrentDirection = Direction.Up;
                                                break;
                                        }

                                        cart.MakeTurn();
                                        break;

                                }

                                break;
                            case Direction.Up:
                                nextTrack = tracks[x - 1, y];

                                if (nextTrack.Cart != null && (nextTrack.Cart.Generation == generation || (nextTrack.Cart.Generation == generation - 1 && nextTrack.Cart.CurrentDirection == Direction.Down)))
                                {
                                    Cart nextCart = nextTrack.Cart;

                                    //instead of recording the collision, delete the carts
                                    if (carts.Contains(cart))
                                    {
                                        carts.Remove(cart);
                                    }
                                    cart.track = null;
                                    track.Cart = null;
                                    cart = null;

                                    if (carts.Contains(nextCart))
                                    {
                                        carts.Remove(nextCart);
                                    }
                                    nextCart.track = null;
                                    nextTrack.Cart = null;
                                    nextCart = null;
                                    continue;
                                }
                                nextTrack.Cart = cart;
                                cart.track = nextTrack;
                                track.Cart = null;
                                switch (nextTrack.Orientation)
                                {
                                    case TrackOrientation.UpLefty:
                                        cart.CurrentDirection = Direction.Left;
                                        break;
                                    case TrackOrientation.UpRighty:
                                        cart.CurrentDirection = Direction.Right;
                                        break;
                                    case TrackOrientation.Intersection:
                                        //workout which way to turn. no change if it's straight.
                                        switch (cart.NextTurn)
                                        {
                                            case Turn.Left:
                                                cart.CurrentDirection = Direction.Left;
                                                break;
                                            case Turn.Right:
                                                cart.CurrentDirection = Direction.Right;
                                                break;
                                        }

                                        cart.MakeTurn();
                                        break;

                                }

                                break;
                            case Direction.Down:
                                nextTrack = tracks[x + 1, y];

                                if (nextTrack.Cart != null && (nextTrack.Cart.Generation == generation || (nextTrack.Cart.Generation == generation - 1 && nextTrack.Cart.CurrentDirection == Direction.Up)))
                                {
                                    Cart nextCart = nextTrack.Cart;

                                    //instead of recording the collision, delete the carts
                                    if (carts.Contains(cart))
                                    {
                                        carts.Remove(cart);
                                    }
                                    cart.track = null;
                                    track.Cart = null;
                                    cart = null;

                                    if (carts.Contains(nextCart))
                                    {
                                        carts.Remove(nextCart);
                                    }
                                    nextCart.track = null;
                                    nextTrack.Cart = null;
                                    nextCart = null;
                                    continue;
                                }

                                nextTrack.Cart = cart;
                                cart.track = nextTrack;
                                track.Cart = null;
                                switch (nextTrack.Orientation)
                                {
                                    case TrackOrientation.UpLefty:
                                        cart.CurrentDirection = Direction.Right;
                                        break;
                                    case TrackOrientation.UpRighty:
                                        cart.CurrentDirection = Direction.Left;
                                        break;
                                    case TrackOrientation.Intersection:
                                        //workout which way to turn. no change if it's straight.
                                        switch (cart.NextTurn)
                                        {
                                            case Turn.Left:
                                                cart.CurrentDirection = Direction.Right;
                                                break;
                                            case Turn.Right:
                                                cart.CurrentDirection = Direction.Left;
                                                break;
                                        }

                                        cart.MakeTurn();
                                        break;

                                }

                                break;

                        }
                        cart.Generation = generation;

                    }
                }
            }

            //foreach (var c in carts)
            //{
            //    if (c.track == null)
            //    {
            //        Console.WriteLine("null track");
            //    }
            //    else if (c.track.Cart == null)
            //    {
            //        Console.WriteLine("null cart on track");
            //    }
            //    else if (c.track.Cart != c)
            //    {
            //        Console.WriteLine("mismatched cart");
            //    }
            //}

            if (carts.Count == 1)
            {
                for (var x = 0; x < tracks.GetLength(0); x++)
                {
                    for (var y = 0; y < tracks.GetLength(1); y++)
                    {

                        if (tracks[x, y] != null && tracks[x, y].Cart != null)
                        {
                            return (x, y);
                        }
                    }
                }
            }

            return (null, null);

        }

        public static (Track[,] t, List<Cart> c) LoadData(List<string> data)
        {

            var tracks = new Track[data.Count, data[0].Length];

            var carts = new List<Cart>();
            Cart cart;
            for (var x = 0; x < data.Count; x++)
            {
                var y = 0;
                foreach (var c in data[x])
                {
                    switch (c)
                    {
                        case ' ':
                            tracks[x, y] = null;
                            break;
                        case '-':
                            tracks[x, y] = new Track
                            {
                                Orientation = TrackOrientation.Horizontal,
                                X = x,
                                Y = y
                            };
                            break;
                        case '|':
                            tracks[x, y] = new Track
                            {
                                Orientation = TrackOrientation.Vertical,
                                X = x,
                                Y = y
                            };
                            break;
                        case '/':
                            tracks[x, y] = new Track
                            {
                                Orientation = TrackOrientation.UpRighty,
                                X = x,
                                Y = y
                            };
                            break;
                        case '\\':
                            tracks[x, y] = new Track
                            {
                                Orientation = TrackOrientation.UpLefty,
                                X = x,
                                Y = y
                            };
                            break;
                        case '+':
                            tracks[x, y] = new Track
                            {
                                Orientation = TrackOrientation.Intersection,
                                X = x,
                                Y = y
                            };
                            break;
                        case '<':
                            cart = new Cart();
                            cart.CurrentDirection = Direction.Left;
                            tracks[x, y] = new Track
                            {
                                Orientation = TrackOrientation.Horizontal,
                                X = x,
                                Y = y,
                                Cart = cart
                            };
                            cart.track = tracks[x, y];
                            cart.Id = Convert.ToChar(((char)((int)'a') + carts.Count)).ToString();
                            carts.Add(cart);
                            break;
                        case '>':
                            cart = new Cart();
                            cart.CurrentDirection = Direction.Right;
                            tracks[x, y] = new Track
                            {
                                Orientation = TrackOrientation.Horizontal,
                                X = x,
                                Y = y,
                                Cart = cart
                            };
                            cart.track = tracks[x, y];
                            cart.Id = Convert.ToChar(((char)((int)'a') + carts.Count)).ToString();
                            carts.Add(cart);
                            break;
                        case '^':
                            cart = new Cart();
                            cart.CurrentDirection = Direction.Up;
                            tracks[x, y] = new Track
                            {
                                Orientation = TrackOrientation.Vertical,
                                X = x,
                                Y = y,
                                Cart = cart
                            };
                            cart.track = tracks[x, y];
                            cart.Id = Convert.ToChar(((char)((int)'a') + carts.Count)).ToString();
                            carts.Add(cart);
                            break;
                        case 'v':
                            cart = new Cart();
                            cart.CurrentDirection = Direction.Down;
                            tracks[x, y] = new Track
                            {
                                Orientation = TrackOrientation.Vertical,
                                X = x,
                                Y = y,
                                Cart = cart
                            };
                            cart.track = tracks[x, y];
                            cart.Id = Convert.ToChar(((char)((int)'a') + carts.Count)).ToString();
                            carts.Add(cart);
                            break;
                    }
                    y++;
                }
            }

            PrintTracks(tracks);

            //Helper.Pause();
            return (tracks, carts);
        }

        private static List<Cart> PrintTracks(Track[,] tracks, bool actuallyPrint = true)
        {
            var foundCarts = new List<Cart>();
            var oldColor = Console.BackgroundColor;
            for (var x = 0; x < tracks.GetLength(0); x++)
            {
                for (var y = 0; y < tracks.GetLength(1); y++)
                {
                    if (x == 96 && y == 147)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                    }
                    if (tracks[x, y] == null)
                    {
                        if (actuallyPrint) Console.Write(' ');
                    }
                    else if (tracks[x, y].Cart != null)
                    {
                        foundCarts.Add(tracks[x, y].Cart);
                        if (tracks[x, y].Cart.Collided)
                        {
                            var oldC = Console.ForegroundColor;
                            Console.ForegroundColor = ConsoleColor.Black;
                            var oldB = Console.BackgroundColor;
                            Console.BackgroundColor = ConsoleColor.Red;
                            if (actuallyPrint) Console.Write('X');
                            Console.ForegroundColor = oldC;
                            Console.BackgroundColor = oldB;
                        }
                        else
                        {

                            var oldC = Console.ForegroundColor;
                            Console.ForegroundColor = ConsoleColor.Black;
                            var oldB = Console.BackgroundColor;
                            Console.BackgroundColor = ConsoleColor.White;
                            if (actuallyPrint) Console.Write(tracks[x,y].Cart.Id);
                            //switch (tracks[x, y].Cart.CurrentDirection)
                            //{
                            //    case Direction.Left:
                            //        if (actuallyPrint) Console.Write('<');
                            //        break;
                            //    case Direction.Right:
                            //        if (actuallyPrint) Console.Write('>');
                            //        break;
                            //    case Direction.Up:
                            //        if (actuallyPrint) Console.Write('^');
                            //        break;
                            //    case Direction.Down:
                            //        if (actuallyPrint) Console.Write('v');
                            //        break;
                            //    default:
                            //        if (actuallyPrint) Console.Write('@');
                            //        break;
                            //}

                            Console.ForegroundColor = oldC;
                            Console.BackgroundColor = oldB;
                        }
                    }
                    else
                    {
                        switch (tracks[x, y].Orientation)
                        {
                            case TrackOrientation.Horizontal:
                                if (actuallyPrint) Console.Write('-');
                                break;
                            case TrackOrientation.Vertical:
                                if (actuallyPrint) Console.Write('|');
                                break;
                            case TrackOrientation.UpRighty:
                                if (actuallyPrint) Console.Write('/');
                                break;
                            case TrackOrientation.UpLefty:
                                if (actuallyPrint) Console.Write('\\');
                                break;
                            case TrackOrientation.Intersection:
                                if (actuallyPrint) Console.Write('+');
                                break;
                        }
                    }
                    if (x == 96 && y == 147)
                    {
                        Console.BackgroundColor = oldColor;
                    }
                }
                if (actuallyPrint) Console.WriteLine(string.Empty);
            }

            return foundCarts;
        }
    }
}
