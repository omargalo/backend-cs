namespace Backend.Services
{
    public class RandomService : IRandomService
    {
        private readonly int _value;
        public int Value
        {
            get => _value;
        }

        //Constructor
        public RandomService()
        {
            _value = new Random().Next(1000);
        }
    }
}
