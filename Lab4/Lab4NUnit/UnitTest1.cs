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
            // Arange - Action
            Population population = Solver.Solve();
            //Assert
            Assert.LessOrEqual(population.Chromosomes[^1].GetWeight(), 250);

        }
        [Test]
        public void TestCorrectRecordValue()
        {
            // Arange 
            Population population = Solver.Solve();
            //Action
            Chromosome? Record = Solver.Record;
            //Assert
            Assert.That(Record?.GetValue(), Is.Not.EqualTo(population.GetTheWorstChromosome().GetValue()));
        }
        [Test]
        public void TestImprovement()
        {
            // Arange
            BackPack backPack = new BackPack();
            Population population = new Population(backPack);
            population.GeneratePopulation();
            //Action
            int ChromosomeValueBeforeImprovement = population.Chromosomes[1].GetValue();
            Chromosome improvement = Solver.LocalImprovement(population.Chromosomes[1]);
            int ChromosomeValueAfterImprovement = improvement.GetValue();
            //Assert
            Assert.Greater(ChromosomeValueAfterImprovement, ChromosomeValueBeforeImprovement);
        }
    }
}