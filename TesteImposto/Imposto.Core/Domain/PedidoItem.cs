﻿using System;

namespace Imposto.Core.Domain
{
    [Serializable()]
    public class PedidoItem
    {
        public string NomeProduto { get; set; }
        public string CodigoProduto { get; set; }
        public double ValorItemPedido { get; set; }
        public bool Brinde { get; set; }
    }
}
