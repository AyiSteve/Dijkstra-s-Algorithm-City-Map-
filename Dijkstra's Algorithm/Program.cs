using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dijkstra;
using Dijkstra_s_Algorithm;

class Program
  {
    public static void Main()
    {
        //Declare Relationship of a city to another

        NodeDistance seattleToMarcerIsland = new NodeDistance("Seattle", "MarcerIsland", 1);
        NodeDistance seattleToTacoma = new NodeDistance("Seattle", "Tacoma", 2);
        NodeDistance seattleToBothell = new NodeDistance("Seattle", "Bothell", 4);

        NodeDistance bothellToKirkland = new NodeDistance("Bothell", "Kirkland", 2);
        NodeDistance bothellToRedmond = new NodeDistance("Bothell", "Redmond", 3);
        NodeDistance kirklandToRedmond = new NodeDistance("Kirkland", "Redmond", 1);
        NodeDistance kirklandToBellevue = new NodeDistance("Kirkland", "Bellevue", 4);
        NodeDistance redmondToBellevue = new NodeDistance("Redmond", "Bellevue", 6);

        NodeDistance marcerIslandToBellevue = new NodeDistance("MarcerIsland", "Bellevue", 2);

        NodeDistance bellevueToRenton = new NodeDistance("Bellevue", "Renton", 2);
        NodeDistance bellevueToKent = new NodeDistance("Bellevue", "Kent", 2);

        NodeDistance tacomaToKent = new NodeDistance("Tacoma","Kent", 1);
        NodeDistance KenttoRenton = new NodeDistance("Kent", "Renton", 1);

        //Declare individual Location
        NodeLocation Bothell = new NodeLocation("Bothell");
        NodeLocation Tacoma = new NodeLocation("Tacoma");
        NodeLocation Kent = new NodeLocation("Kent");
        NodeLocation Renton = new NodeLocation("Renton");
        NodeLocation Redmond = new NodeLocation("Redmond");
        NodeLocation MarcerIsland = new NodeLocation("MarcerIsland");
        NodeLocation Seattle = new NodeLocation("Seattle");
        NodeLocation Kirkland = new NodeLocation("Kirkland");
        NodeLocation Bellevue = new NodeLocation("Bellevue");

        //Put all NodeLocations information in to one list
        NodeLocation[] nodeLocations = { Seattle, Bothell, Tacoma, Kent, Renton, Redmond, MarcerIsland, Bellevue,Kirkland };
        
        //Put all NodeDistance information in to one list
        NodeDistance[] nodeDistances = 
            { seattleToMarcerIsland , seattleToTacoma, seattleToBothell, bothellToKirkland, 
            bothellToRedmond, redmondToBellevue, kirklandToBellevue, marcerIslandToBellevue,
        bellevueToRenton,tacomaToKent,KenttoRenton,bellevueToKent,kirklandToRedmond};

        //Give it to our DijkstraAlgorithm Class, it will help us build the relation ship map of all city!!!
        //However, it's not visual in image but it help us to graph all city and connect them in to a map(Back End stuff).
        DijkstraAlgorithm shorestPath = new DijkstraAlgorithm(nodeDistances, nodeLocations);

        //Test this out to see some surprise!
        //The shortestpath method will taken in (Start City, End City)

        //Run some test here
        Console.WriteLine("=================Test 1=================");
        Console.WriteLine("Traveling from Bellevue to Redmond");
        Console.WriteLine("\n");
        Console.WriteLine("Call Dijkstra Algorithm and Result is: ");
        Console.WriteLine(shorestPath.ShortestPath("Bellevue", "Redmond"));
        Console.WriteLine("\n");

        Console.WriteLine("What We Expect???");
        Console.WriteLine("Expected: City Traveled: Bellevue - > KirkLand -> Redmond" + "\nExpected: Total Distance Taken: 5");
        Console.WriteLine("\n");
        Console.WriteLine("\n");

        Console.WriteLine("=================Test 2=================");
        Console.WriteLine("Traveling from Seattle to Renton");
        Console.WriteLine("\n");
        Console.WriteLine("Call  Dijkstra Algorithm and Result is: ");
        Console.WriteLine(shorestPath.ShortestPath("Seattle", "Kent"));
        Console.WriteLine("\n");

        Console.WriteLine("What We Expect???");
        Console.WriteLine("Expected: City Traveled: Seattle - > Tacoma - > Kent" + "\nExpected: Total Distance Taken: 3");
        Console.WriteLine("\n");
        Console.WriteLine("\n");

        Console.WriteLine("=================Test 3=================");
        Console.WriteLine("Traveling from Bothell to Kent");
        Console.WriteLine("\n");
        Console.WriteLine("Call  Dijkstra Algorithm and Result is: ");
        Console.WriteLine(shorestPath.ShortestPath("Bothell", "Kent"));
        Console.WriteLine("\n");

        Console.WriteLine("What We Expect???");
        Console.WriteLine("Expected: City Traveled: Bothell - > Seattle - > Tacoma - > Kent" + "\nExpected: Total Distance Taken: 7");
    }



}

