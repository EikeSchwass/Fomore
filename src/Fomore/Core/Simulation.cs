using System;
using System.Collections.Generic;
using System.Text;
using Core.Training;
using Core.Training.Evolution;

namespace Core
{
    public class Simulation
    {
        public Simulation()
        {

        }

        /// <summary>
        /// This method runs the simulation. It uses the creature structure to generate the populations,
        /// and carries out the evolutionary algorithm to reach a certain goal.
        /// </summary>
        public void RunSimulation()
        {
            //Generate Terrain
            TerrainGenerate();

            //Acquire Creature  Structure;
            AcquireStructure();

            /*
            //initialize population.
            EvolutionaryAlgorithm<>.InitializePopulation(Population);

            //Sort Population by their fitness
            SelectionMethods.Best<>(Population);
            
            while (int i < Goal)
            {
                //Run the Evolutionary Algorithm
            }
            */

        }

        /// <summary>
        /// This method records the videos of the simulation. I'm not sure yet if it would be possible
        /// to include videos of certain iterations or of the entire simulation.
        /// </summary>
        public void CaptureVideo()
        {

        }

        /// <summary>
        /// This method would be used to show the visuals of the simulation that is being undertaken.
        /// </summary>
        public void ShowSimulation()
        {
            //Needs Farseer Integration
        }

        /// <summary>
        /// This method would be used to store and visualize the statistics of the simulation. This
        /// includes the progress made towards the goal at each iteration, time taken to achieve the
        /// goal, and the visual representation of the learning curve (i.e progress toward the goal at
        /// each iteration for all iterations). Any other statistics could also be added here.
        /// </summary>
        public void StoreStatistics()
        {

        }

        private double[] TerrainGenerate()
        {
            terrain = TerrainGenerator.GenerateTerrain(Environment);
            return terrain;
        }

        private CreatureStructure AcquireStructure()
        {
            structure = Creature.CreatureStructure;
            return structure;
        }

        public Creature Creature;
        public Environment Environment;
        public MovementPattern Movement_Pattern;
        public double Goal;
        public int Population;

        private double[] terrain = new double[100];
        private CreatureStructure structure;
    }
}
