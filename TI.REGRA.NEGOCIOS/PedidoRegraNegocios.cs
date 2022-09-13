using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TI.ACESSO.DADOS;
using TI.OBJETO.TRANSFERENCIA;

namespace TI.REGRA.NEGOCIOS
{
    public class PedidoRegraNegocios
    {
        CONEXAO conexaoSqlServer = new CONEXAO();


        public string Salvar(Pedidos pedido)
        {
            try
            {
                string dataBarsilia = ObterHorarioBrasilia().ToString();
                conexaoSqlServer.LimparParametros();

                conexaoSqlServer.LimparParametros();

                conexaoSqlServer.AdicionarParametros("@NUMERO", pedido.numeroPedido.Trim());
                conexaoSqlServer.AdicionarParametros("@ID_EMPRESA", pedido.idEmpresa);
                conexaoSqlServer.AdicionarParametros("@TIPO_PGTO", pedido.tipoPagamento);
                conexaoSqlServer.AdicionarParametros("@DATA", Convert.ToDateTime( dataBarsilia).ToString("yyyy-MM-dd HH:mm:ss"));
                conexaoSqlServer.AdicionarParametros("@TAXA_ENTREGA", pedido.taxaEntrega);
                conexaoSqlServer.AdicionarParametros("@TROCO_PARA", pedido.trocoPara);
                conexaoSqlServer.AdicionarParametros("@OBS_PEDIDO", "");
                conexaoSqlServer.AdicionarParametros("@FECHADO", false);
                conexaoSqlServer.AdicionarParametros("@STATUS_PEDIDO", "PEDIDO");
                conexaoSqlServer.AdicionarParametros("@SOMA_TOTAL", pedido.total);
                conexaoSqlServer.AdicionarParametros("@TAMANHO", "");
                conexaoSqlServer.AdicionarParametros("@TELEFONE_CLIENTE", pedido.telefone.Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "").Trim());
                conexaoSqlServer.AdicionarParametros("@RECIBO", pedido.pedidotexto);
                conexaoSqlServer.AdicionarParametros("@TIPO_ENTREGA", pedido.formaRetirada.ToUpper());

                return conexaoSqlServer.ExecutarManipulacao(CommandType.StoredProcedure, "uspPedidosSalvar").ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DateTime ObterHorarioBrasilia()
        {
            DateTime DateTimeUtc = DateTime.UtcNow;

            TimeZoneInfo TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");//(GMT-03:00) Brasília

            DateTime DateTimeBrasilia = TimeZoneInfo.ConvertTimeFromUtc(DateTimeUtc, TimeZoneInfo);

            return DateTimeBrasilia;
        }

    }
}
