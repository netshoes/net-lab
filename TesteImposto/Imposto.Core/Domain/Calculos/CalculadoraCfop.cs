namespace Imposto.Core.Domain.Calculos
{
    public class CalculadoraCfop
    {
        private readonly ICalculoCfopStrategy _strategy;

        public CalculadoraCfop(ICalculoCfopStrategy strategy)
        {
            _strategy = strategy;
        }

        public string Calcular()
        {
            return _strategy.Calcular();
        }
    }
}
