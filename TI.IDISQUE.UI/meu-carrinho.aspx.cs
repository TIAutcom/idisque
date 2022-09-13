using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TI.OBJETO.TRANSFERENCIA;
using TI.REGRA.NEGOCIOS;

namespace TI.IDISQUE.UI
{
    public partial class meu_carrinho : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string SalvarPedidoDelivery(Pedidos pedido)
        {
            try
            {
                PedidoRegraNegocios pedidoRegraNegocios = new PedidoRegraNegocios();
                return pedidoRegraNegocios.Salvar(pedido);
            }
            catch (Exception ex)
            {
                return "Detalhes do Error: " + ex.Message + "\n\nErro ao Cadastrar Pedido...";
            }
        }
    }
}