using Lab4;
namespace Lab4NUnit
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestCorrectResultWeight()
        {
            // Arange 
            Population population = Solver.Solve();
            //Action
            int MaxWeight = int.MinValue;
            foreach (var item in population.Chromosomes)
            {
                MaxWeight = Math.Max(MaxWeight, item.GetWeight);
            }
            //Assert
            Assert.LessOrEqual(MaxWeight, 250);

        }
        [Test]
        public void TestCorrectRecordValue()
        {
            // Arange 
            Population population = Solver.Solve();
            //Action
            Chromosome? Record = Solver.Record;
            //Assert
            Assert.That(Record?.GetValue, Is.Not.EqualTo(population.GetTheWorstChromosome.GetValue));
        }
        [Test]
        public void TestImprovement()
        {
            // Arange
            BackPack backPack = new BackPack();
            Population population = new Population(backPack);
            population.GeneratePopulation();

            Chromosome FirstParent = population.GetBestChromosome;
            Chromosome SecondParent;
            while (true)
             {
                Random random = new Random();
                int index = random.Next(0, 100);
                if (population.Chromosomes[index] != FirstParent)
                {
                    SecondParent = population.Chromosomes[index];
                    break;
                }
            }
            Chromosome Offspring = Solver.CrossOver(FirstParent, SecondParent);
            Chromosome mutation = Solver.Mutation(Offspring);
            int ChromosomeValueBeforeImprovement = mutation.GetValue;
            //Action
            Chromosome improvement = Solver.LocalImprovement(mutation);
            int ChromosomeValueAfterImprovement = improvement.GetValue;
            //Assert
            Assert.Greater(ChromosomeValueAfterImprovement, ChromosomeValueBeforeImprovement);
        }
        [Test]
        public void TestCrossOver()
        {
            //Arange
            BackPack backPack = new BackPack();
            Population population = new Population(backPack);
            population.GeneratePopulation();

            Chromosome FirstParent = population.GetBestChromosome;
            Chromosome SecondParent;
            while (true)
            {
                Random random = new Random();
                int index = random.Next(0, 100);
                if (population.Chromosomes[index] != FirstParent)
                {
                    SecondParent = population.Chromosomes[index];
                    break;
                }
            }
            //Action
            Chromosome Offspring = Solver.CrossOver(FirstParent, SecondParent);
            int flag = 1;
            bool correct = true;
            for (int i = 0; i < Offspring.Gene.Count; i++)
            {
                if (i % 25 == 0 && i != 0) flag *= -1;
                if (flag == 1 && Offspring.Gene[i] != FirstParent.Gene[i])
                {
                    correct = false;
                    break;
                }
                if(flag == -1 && Offspring.Gene[i] != SecondParent.Gene[i])
                {
                    correct = false;
                    break;
                }
            }
            //Assert
            Assert.IsTrue(correct);
        }
    }
}