namespace Lab4
{
    public class Item
    {
        public int Value { get; set; }

        public int Weight { get; set; }

        public Item()
        {
            Random random = new Random();
            Value = random.Next(2, 31);
            Weight = random.Next(1, 26);
        }
    }
}
