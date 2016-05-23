using FluentAssertions;
using Imposto.Core.Domain.Calculos;
using NUnit.Framework;
using System;

namespace Imposto.Test.Domain
{
    [TestFixture]
    public class CalculoCfopFactoryTest
    {
        [Test]
        public void DeveInstanciarCorretamente()
        {
            var strategy = new CalculoCfopFactory().Criar("MG", "MG");

            strategy.Should().NotBeNull();
        }

        [Test]
        public void DeveLancarExceptionCasoNaoExistaImplementacaoParaDestinoOrigem()
        {
            new CalculoCfopFactory().Invoking(x => x.Criar("AA", "BB")).ShouldThrow<Exception>();
        }
    }
}
