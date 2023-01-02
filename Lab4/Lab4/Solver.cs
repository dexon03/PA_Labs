using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    public static class Solver
    {
        public static BackPack backPack = new BackPack();
        static Population population = new Population(backPack);
        public static Chromosome? Record;

        public static Population Solve()
        {
            population.GeneratePopulation();
            Record = population.GetTheWorstChromosome();
            int iterations = 0;
            while (iterations <= 1000)
            {
                (Chromosome FirstParent, Chromosome SecondParent) = GetParents();
                Chromosome Offspring = CrossOver(FirstParent, SecondParent);
                Chromosome mutation = Mutation(Offspring);
                Chromosome improved = LocalImprovement(mutation);
                if(improved.GetValue() > Record.GetValue())
                {
                    Record = improved;
                }
                Chromosome worstChromo = population.GetTheWorstChromosome();
                if (improved.GetWeight() <= backPack.Capacity)
                {
                    
                    population.Chromosomes.Remove(worstChromo);
                    population.Chromosomes.Add(improved);
                }
                else
                {
                    if(Offspring.GetWeight() <= backPack.Capacity)
                    {
                        population.Chromosomes.Remove(worstChromo);
                        population.Chromosomes.Add(Offspring);
                    }
                    else
                    {
                        continue;
                    }
                }
                if(iterations%20==0)
                {
                    WriteRecordToFile(iterations, Record.GetValue());
                }
                iterations++;
            }
            return population;
        }

        public static  (Chromosome, Chromosome) GetParents()
        {
            Chromosome FirstParent = population.GetBestChromosome();
            Chromosome SecondParent;
            while (true)
            {
                Random random  = new Random();
                int index = random.Next(0, 100);
                if (population.Chromosomes[index] != FirstParent)
                {
                    SecondParent = population.Chromosomes[index];
                    break;
                }
            }

            return (FirstParent, SecondParent);
        }

        public static Chromosome CrossOver(Chromosome FirstParent,Chromosome SecondParent) 
        {
            List<int> mergedList = new List<int>(100);
            int flag = 1;
            for (int i = 0; i < 100; i++)
            {
                if (i % 25 == 0 && i!= 0) flag *= -1;
                if(flag > 0)
                {
                    mergedList.Add(FirstParent.Gene[i]);
                }
                else
                {
                    mergedList.Add(SecondParent.Gene[i]);
                }
            }
            Chromosome Offspring = new Chromosome(mergedList);
            return Offspring;
        }

        public static Chromosome Mutation(Chromosome chromosome)
        {
            Chromosome mutation = new Chromosome(chromosome.Gene);
            Random random = new Random();
            int chance = random.Next(1, 101);
            if(chance <= 5)
            {
                int index_1 = random.Next(0, mutation.Gene.Count);
                int index_2;
                while (true)
                {
                    index_2 = random.Next(0, mutation.Gene.Count);
                    if (index_2 != index_1) break;
                }
                (mutation.Gene[index_1], mutation.Gene[index_2]) = (mutation.Gene[index_2], mutation.Gene[index_1]);
            }
            return mutation;
        }

        public static Chromosome LocalImprovement(Chromosome chromosome)
        {
            Chromosome improved = new Chromosome(chromosome.Gene);
            Random random = new Random();
            while (true)
            {
                int index = random.Next(0,improved.Gene.Count);
                if (improved.Gene[index] == 0)
                {
                    improved.Gene[index] = 1;
                    break;
                }
            }
            return improved;
        }
        private static void WriteRecordToFile(int iteration,int value)
        {
            using(var stream = new StreamWriter("records.txt", true))
            {
                stream.WriteLine(value);
            }
        }
    }
}
