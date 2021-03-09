namespace BlazingPizza
{
    public record PizzaTopping
    {
        public Topping Topping { get; set; }

        public int ToppingId { get; set; }

        public int PizzaId { get; set; }
    }
}
