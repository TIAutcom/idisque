using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TI.OBJETO.TRANSFERENCIA
{
    public class Pedidos
    {
        public int id { get; set; }
        public string tipoPagamento { get; set; }
        public string total { get; set; }
        public string numeroPedido { get; set; }
        public int idEmpresa { get; set; }
        public string pedidotexto { get; set; }
        public string data { get; set; }
        public string trocoPara { get; set; }
        public string taxaEntrega { get; set; }
        public string observacao { get; set; }
        public bool fechado { get; set; }
        public string stastusPedido { get; set; }
        public string somaTotal { get; set; }
        public string tamanho { get; set; }
        public string telefone { get; set; }
        public string recibo { get; set; }
        public string formaRetirada { get; set; }
        public string valorFrete { get; set; }
        public string km { get; set; }
    }
}
