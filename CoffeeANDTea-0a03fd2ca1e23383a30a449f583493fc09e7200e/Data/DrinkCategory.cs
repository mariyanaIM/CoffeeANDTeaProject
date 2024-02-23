namespace CoffeeANDTea.Data
{
    public class DrinkCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateUpdate { get; set; } = DateTime.Now;
        public ICollection<Drink> Drinks { get; set; }
    }
}