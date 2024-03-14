using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using Dijkstra_s_Algorithm;


namespace Dijkstra
{

    class DijkstraAlgorithm
    {
        //Technically, an array that hold all individual city's information
        public List<NodeLocation> relationshipMap;

        //Queue that's used to calculate the quickest path.
        Queue<DistanceTaken> path;

        //Set up the relationshipMap (Constructor)
        //Used to set up the nodelocation node(Individual city's information and relationship to another)
        public DijkstraAlgorithm(NodeDistance[] nodeDistances, NodeLocation[] nodeLocations) 
        { 
            //Initialize path and relationshipmap
            path = new Queue<DistanceTaken>();
            relationshipMap = new List<NodeLocation>();

            //get all the existing city
            for (int i = 0; i < nodeLocations.Length; i++)
            {
                relationshipMap.Add(nodeLocations[i]);
            }

            /*
             * Triple for loop, this increase a lot of run time, 
             * but this is used to sort the relationship of the location.
           */

            for (int ii =0; ii < nodeDistances.Length; ii++)
            {
                for (int jj =0; jj< relationshipMap.Count; jj++)
                {
                    //Find if there existing relationship of a specific city to another one (Compare current city with startname of distance information)
                    //Since nodeDistances stores two name, I need to check each one individual to know it's a proper informaiton to push in

                    if (relationshipMap[jj].location.Equals(nodeDistances[ii].startName))
                    {
                        //Save the cost relationship of one city to another (distances)
                        relationshipMap[jj].nextDistance.Add(nodeDistances[ii]);

                        for (int zz=0; zz<relationshipMap.Count; zz++)
                        {
                            //Save the exact relationship of one city to another city (NodeLocation; which let the system know this city can access to another one)
                            if (relationshipMap[zz].location.Equals(nodeDistances[ii].endName))
                            {
                                relationshipMap[jj].nextLocations.Add(relationshipMap[zz]);
                            }
                        }
                    }

                    //Find if there existing relationship of a specific city to another one (Compare current city with endname of distance information)
                    //Since nodeDistances stores two name, I need to check each one individual to know it's a proper informaiton to push in
                    else if (relationshipMap[jj].location.Equals(nodeDistances[ii].endName))
                    {
                        relationshipMap[jj].nextDistance.Add(nodeDistances[ii]);
                        //Save the cost relationship of one city to another (distances)
                        for (int zz = 0; zz < relationshipMap.Count; zz++)
                        {
                            //Save the exact relationship of one city to another city (NodeLocation; which let the system know this city can access to another one)
                            if (relationshipMap[zz].location.Equals(nodeDistances[ii].startName))
                            {
                                relationshipMap[jj].nextLocations.Add(relationshipMap[zz]);
                            }
                        }
                    }
                }
            }
        }

        //Find the shortestPath of start location to endlocation
        public String ShortestPath(String startLocation,String endLocation)
        {
            path = new Queue<DistanceTaken>();


            //Get the index location of 'startlocation' in my relationshipmap
            int startLocationIndex = findCurrentLocationIndex(startLocation);

            //Get the memory address out to a variable (so it's easier to access)
            NodeLocation headLocation = relationshipMap[startLocationIndex];
      
            //Push the first couple city that start city can access to into the Queue
            for (int ii = 0; ii < headLocation.nextLocations.Count; ii++)
            {
                //Set up the path that it will go to
                String pathName = headLocation.location + " -> ";
                int lengthTaken = 0;

                /*
                Save information as a distancetaken to queue
                Repeating, distancetaken is used to calculate the total city that it been through and
                the current total cost and next existing city that it can go to.
                */
                this.path.Enqueue(new DistanceTaken(pathName + headLocation.nextLocations[ii].location, lengthTaken + findDistanceToNext(startLocationIndex, headLocation.nextLocations[ii].location), headLocation.nextLocations[ii]));

            }

            //Sort the Queue, in Dijkstra's algorithm, it always want to expands search on path that's total cost is the minimum out of others.
            sortQueue();

            //Call our helper method to do recursive loop!
            //and we will end up having the total city it been to and total cost taken to get to another location
            DistanceTaken distanceTaken = Shortest(endLocation);

            //Break total cost and total city travel in to a sentence that's more understandable
            String s = "Traveling from " + startLocation + " to " + endLocation + ": ";
             s = s +"\nCity Traveled: " + distanceTaken.location + "\n Total Distance Taken: " + distanceTaken.Distance;

            return s;
        }


        /*
          Helper Method of ShortestPath
          Recursive loop that constantly expanding the search of cities with a total cost that's the minimum out of all by
            dequeue a queue(Which eventually gaves us a path with, currently, shorest total distance that it had already travel )
        */
        private DistanceTaken Shortest(String endLocation)
        {
            //Get the head of the queue and store it in the headLocation
            DistanceTaken headLocation = this.path.Dequeue();

            //Check if we had already reach the endlocation or not
            //Return the total cost and total city travel if find
            if (headLocation.nextCity.location.Equals(endLocation))
            {
                return headLocation;
            }
            
            //Expand the headLocation by searching it's existing reachable next city
            for (int ii = 0; ii < headLocation.nextCity.nextLocations.Count; ii++)
            {
                //Get information of the headLocation
                String pathName = headLocation.location;
                int lengthTaken = headLocation.Distance;
                int startLocationIndex = findCurrentLocationIndex(headLocation.nextCity.location);

                /*
               Save information as a distancetaken to queue
               Repeating, distancetaken is used to calculate the total city that it been through and
               the current total cost and next existing city that it can go to.
               */
                this.path.Enqueue(new DistanceTaken(pathName + " -> " +  headLocation.nextCity.nextLocations[ii].location, lengthTaken + findDistanceToNext(startLocationIndex, headLocation.nextCity.nextLocations[ii].location), headLocation.nextCity.nextLocations[ii]));
                
                //Sort information in queue to lowest total cost to highest total cost
                //(By comparing the total cost calculated in distanceTaken)
                sortQueue();
            }
          
            //Recursive loop, keep expanding the next lowest total cost path
            return Shortest(endLocation);
           
        }

        /*
         Helper Method
        Return the cost of a city (relationshipMap[locationIndex]) to another(NeighborsName).
        */
        private int findDistanceToNext(int locationIndex, String NeighborsName)
        {
            //Try to find the information of specific city and another inrelationshipMap(Of one specific city)
            for (int ii = 0; ii < relationshipMap[locationIndex].nextDistance.Count; ii++)
            {
                //Since there is a startName and endName in the NodeDistance node thus I need to check both of them
                //if they are the name I am looking for.
                //If find, return the cost of specific city(relationshipMap[locationIndex]) to NeighborName
                if (relationshipMap[locationIndex].nextDistance[ii].startName.Equals(NeighborsName))
                {
                    
                    return relationshipMap[locationIndex].nextDistance[ii].cost;
                }

                if (relationshipMap[locationIndex].nextDistance[ii].endName.Equals(NeighborsName))
                {
                    return relationshipMap[locationIndex].nextDistance[ii].cost;
                }
            }

            //Throw exception when there is invalid input
            throw new Exception("Find Distance Not Found");

            
        }

        /*
        Helper Method
        Find the index/location of specific city(CurrentName) in the relationshipMap
        */
        private int findCurrentLocationIndex(String CurrentName)
        {
            for (int zz = 0; zz < relationshipMap.Count; zz++)
            {
                //If find the index of the currentName in relationshipmap, return the current index
                if (CurrentName.Equals(relationshipMap[zz].location))
                {
                    return zz;
                }
            }

            //Throw exception when there is invalid input
            throw new Exception("Find Neighbors Not Found");
        }

        //Helper Method! (Method below is all about Sorting Queue (By comparing the total cost calculated in distanceTaken))
        //Can't find the priority queue in c# so I implement it with sortQueue
        //which will help my queue function like a c++/java priority queue

        //Sort Queue by converting the queue to a list then call the quick sort method on it
        private void sortQueue()
        {
            int queueSize = path.Count;
            DistanceTaken[] tempList = new DistanceTaken[queueSize];
       
            //Convert queue to list
            for (int i =0; i< queueSize; i++)
            {
                tempList[i] = path.Dequeue();
            }
           
            //Call the quickSort
            quickSort(tempList);

            //Enqueue information in adjusted list back to original queue 
            for (int i = 0; i < tempList.Length; i++)
            {
                this.path.Enqueue(tempList[i]);
            }

        }

        /*
          Partition will sort item in to two part; one part is bigger than pivot, one is smaller.
          return highest index of the low partition
         */

        private static int partition(DistanceTaken[] data, int low, int high)
        {
            //Find the pivot(Which is in the middle of given range in array)
            int midIndex = (high + low) / 2;
            int pivot = data[midIndex].Distance;
            Boolean allPass = false;

            //Loop until it's sorted with two part, one part is bigger than pivot, another is smaller.
            while (!allPass)
            {
                //Increment low while data[low] is smaller then pivot
                while (data[low].Distance < pivot)
                {
                    low++;
                }

                //Decrement high while data[high] is bigger then pivot
                while (data[high].Distance > pivot)
                {
                    high--;
                }


                //Everything is sorted if one or zero items is remaining
                if (low >= high)
                {
                    allPass = true;
                }

                //
                else
                {
                    /*
                    The while loop that increment low and decrement high will definitely end up
                    to place where there is value on the left side is bigger than pivot and
                    number on the right side is smaller than pivot; thus we swap their position.
                     */
                    swap(data, low, high);
                    low++;
                    high--;
                }
            }

            //return the highest index of the low partition
            return high;
        }



        /*
         Repeatedly sort the array to low and high part with pivot
         Last, it wil recursively sorts those parts to smallest to highest.
         */
        public static void quickSort(DistanceTaken[] data)
        {
            quickSort(data, 0, data.Length - 1);
        }


  
        /*
         Helper method of quicksort.
         This method will recursively sort the array to smallest
         to highest with the help of partition method.
         */
        private static void quickSort(DistanceTaken[] data, int min, int max)
        {
            if (min >= max)
            {
                return;
            }
            int j = partition(data, min, max);

            quickSort(data, min, j);
            quickSort(data, j + 1, max);
        }

        /*
         Swap two individual value of distancetaken
         */
        public static void swap(DistanceTaken[] data, int a, int b)
        {
            DistanceTaken temp = data[a];
            data[a] = data[b];
            data[b] = temp;
        }
    }
}
