using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dijkstra_s_Algorithm
{

    //This node will stores two name of location and stores the cost traveling between them.
    //Mainly used to save the instance relationship between two city(City that it can go to and the cost).
    public class NodeDistance
    {
        //First City
        public String startName;
        //Another City
        public String endName;
        //Distance between the two
        public int cost;
   

        //Constructor
        public NodeDistance(String StartName, String EndName, int Distance)
        {
            startName = StartName;
            endName = EndName;
            cost = Distance;
          
        }
    }

    //This node will stores information of single city.
    //
    public class NodeLocation
    {
        public String location;
        //The cost of going to another city that it can access to
        public List <NodeDistance> nextDistance;
        //Location that the current city can go to
        public List<NodeLocation> nextLocations;

        //Constructor
        public NodeLocation(String Location)
        {
            this.location = Location;
            nextDistance = new List<NodeDistance>();
            nextLocations = new List<NodeLocation>();
 
        }
    }

    //This is used to calculate and conclude the total distance and city that the starting city travel through.

    public class DistanceTaken
    {
        //Location traveled and where they at now
        public String location;
        //Total cost at current
        public int Distance;
        //Existing next location that the person can go to
        public NodeLocation nextCity;

        //Constructor
        public DistanceTaken(String Location, int Distance,NodeLocation city) 
        {
            this.location = Location;
            this.Distance = Distance;
            this.nextCity = city;
        }
    }
   
}
