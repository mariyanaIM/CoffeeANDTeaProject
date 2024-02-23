using Azure.Identity;

namespace CoffeeANDTea.Data
{
    public class Order
    {
        public int Id { get; set; }
        public string ClientsId { get; set; }
        public Client Clients { get; set; }
        public int DrinksId { get; set; }
        public Drink Drinks { get; set;}
        public int Quantity { get; set; }
        public DateTime DateUpdate { get; set; }
        
    }
}
