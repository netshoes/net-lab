using System;
using System.Linq;
using System.Reflection;

namespace Imposto.Core.Domain.Calculos
{
    public class CalculoCfopFactory
    {
        public ICalculoCfopStrategy Criar(string origem, string destino)
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                var type = assembly.GetTypes().First(t => t.Name == string.Format("Cfop{0}Para{1}Strategy", origem, destino));

                return (ICalculoCfopStrategy)Activator.CreateInstance(type);
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Cfop inexistente para destino: {0} e origem: {1}", origem, destino), exception);
            }
        }
    }
}
