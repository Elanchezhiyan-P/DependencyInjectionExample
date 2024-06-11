namespace DependencyInjectionExample
{
    public interface IRandomNumberGenerator
    {
        int GetRandomNumber();
    }

    public class RandomNumberGenerator : IRandomNumberGenerator
    {
        private readonly int _randomNumber;

        public RandomNumberGenerator()
        {
            _randomNumber = new Random().Next();
        }

        public int GetRandomNumber()
        {
            return _randomNumber;
        }
    }
}
