using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeANDTea.Data
{
    public class Drink
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DrinkCategoriesId { get; set; } 
        public DrinkCategory DrinkCategories { get; set; }
        public int Coffein { get; set; }
        public bool Bio { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        public DateTime DateUpdate { get; set; } = DateTime.Now; 
        public ICollection<Order> Orders { get; set; }
       
    }
}
