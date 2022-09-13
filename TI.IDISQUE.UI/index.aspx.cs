using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using TI.REGRA.NEGOCIOS;
using TI.OBJETO.TRANSFERENCIA;
using System.Web.Services;

namespace TI.IDISQUE.UI
{
    public partial class rumos_tec : System.Web.UI.Page
    {
        public static int idPessoa = 0;
        public string layoutRazaoEmpresa = "";
        public static string _fantasia, _urlImagemLogo, _telefone, _endereco, _numero, _cep, _cidade = "";
        public bool statusEstabelecimento;
        public string layoutHorarioFuncionamentos = "";
        public string layoutDepartamentos = "";
        public string layoutProdutosDepartamentos = "";
        public string layoutProduto = "";
        public string layoutContador = "";
        public static string _horaabertura, _horafechamento, _chavepix, _seguimento, _taxainicial, _tempopreparo = "";
        public bool statusfechado;
        public string javascripttempresa = "";

        PessoaRegraNegocios pessoaRegraNegocios;
        HtmlRegraNegocios htmlRegraNegocios;

        List<HORARIO_FUNCIONAMENTO> listaHora;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                idPessoa = Convert.ToInt32(Page.Request.QueryString["id"]);
            }
            catch 
            {
                idPessoa = 0;
            }

            if (!IsPostBack)
            {
                PesquisarDepartamentos();
            }
        }

        [WebMethod(true)]
        public static string GetDadosEmpresa()
        {
            try
            {
                string sJSON = "";
                DataTable dadosTabela = new DataTable();
                PessoaRegraNegocios pessoaRegraNegocios = new PessoaRegraNegocios();
                HtmlRegraNegocios htmlRegraNegocios = new HtmlRegraNegocios();
                dadosTabela = pessoaRegraNegocios.PesquisarPessoaId(idPessoa);

                if (dadosTabela.Rows.Count > 0)
                {
                    _fantasia = dadosTabela.Rows[0]["FANTASIA"].ToString().Trim();
                    _urlImagemLogo = dadosTabela.Rows[0]["LOGO_IMG"].ToString().Trim();
                    _horaabertura = dadosTabela.Rows[0]["HORA_INICIO"].ToString().Trim();
                    _horafechamento = dadosTabela.Rows[0]["HORA_FIM"].ToString().Trim();
                    _chavepix = dadosTabela.Rows[0]["CHAVE_PIX"].ToString().Trim();
                    _seguimento = dadosTabela.Rows[0]["SEGUIMENTO"].ToString().Trim();
                    _telefone = dadosTabela.Rows[0]["TELEFONE"].ToString().Trim();
                    _taxainicial = dadosTabela.Rows[0]["TAXA"].ToString().Trim();
                    _endereco = dadosTabela.Rows[0]["LOGRADOURO"].ToString().Trim();
                    _numero = dadosTabela.Rows[0]["NUMERO"].ToString().Trim();
                    _cep = dadosTabela.Rows[0]["CEP"].ToString().Trim();
                    _cidade = dadosTabela.Rows[0]["CIDADE"].ToString().Trim();
                    _tempopreparo = dadosTabela.Rows[0]["TEMPO_PREPARO"].ToString().Trim();

                    List<HORARIO_FUNCIONAMENTO> listaHora = new List<HORARIO_FUNCIONAMENTO>();

                    listaHora.Add(new HORARIO_FUNCIONAMENTO()
                    {
                        id = idPessoa,
                        horafechamento = _horafechamento,
                        horaabertura = _horaabertura,
                        fantasia = _fantasia,
                        urlimagem = _urlImagemLogo,
                        chavePix = _chavepix,
                        seguimento = _seguimento,
                        telefone = _telefone,
                        taxainicial = _taxainicial,
                        endereco = _endereco,
                        numero = _numero,
                        cep = _cep,
                        cidade = _cidade,
                        tempopreparo = _tempopreparo
                    });


                    //Serialize events to string
                    System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                    sJSON = oSerializer.Serialize(listaHora);
                }

                return sJSON;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void PesquisarDadosPessoaId()
        {
            try
            {
                DataTable dadosTabela = new DataTable();
                pessoaRegraNegocios = new PessoaRegraNegocios();
                htmlRegraNegocios = new HtmlRegraNegocios();
                dadosTabela = pessoaRegraNegocios.PesquisarPessoaId(idPessoa);

                if (dadosTabela.Rows.Count > 0)
                {
                    layoutRazaoEmpresa = "";


                    _fantasia = dadosTabela.Rows[0]["FANTASIA"].ToString().Trim();
                    _urlImagemLogo = dadosTabela.Rows[0]["LOGO_IMG"].ToString().Trim();
                    _horaabertura = dadosTabela.Rows[0]["HORA_INICIO"].ToString().Trim();
                    _horafechamento = dadosTabela.Rows[0]["HORA_FIM"].ToString().Trim();
                    _chavepix = dadosTabela.Rows[0]["CHAVE_PIX"].ToString().Trim();

                    layoutRazaoEmpresa = htmlRegraNegocios.GerarLayoutRazaoEmpresa(_fantasia, _urlImagemLogo, _horaabertura, _horafechamento);

                    //iFrameEstabelecimento.Controls.Add(new LiteralControl(layoutRazaoEmpresa));

                    addListaJson();
                }
            }
            catch (Exception ex)
            {
                Response.Redirect(ex.Message, true);
            }
        }

        public void addListaJson()
        {
            try
            {
                listaHora = new List<HORARIO_FUNCIONAMENTO>();

                listaHora.Add(new HORARIO_FUNCIONAMENTO()
                {
                    horafechamento = _horafechamento,
                    horaabertura = _horaabertura,
                    fantasia = _fantasia,
                    urlimagem = _urlImagemLogo
                });

                //Serialize events to string
                System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                string sJSON = oSerializer.Serialize(listaHora);

                htmlRegraNegocios = new HtmlRegraNegocios();
                javascripttempresa = htmlRegraNegocios.GerarScriptlistajson(_horaabertura, _horafechamento, _fantasia, "");

                //    iFrameScriptlistajson.Controls.Add(new LiteralControl(javascripttempresa));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void PesquisarDepartamentos()
        {
            try
            {
                DataTable dadosTabela = new DataTable();

                HtmlRegraNegocios htmlRegraNegocios = new HtmlRegraNegocios();

                PessoaRegraNegocios pessoaRegraNegocios = new PessoaRegraNegocios();

                dadosTabela = pessoaRegraNegocios.PesquisarDepartametoIdEmpresa(idPessoa);

                string desc = "";
                string modal = "";

                if (dadosTabela.Rows.Count > 0)
                {
                    layoutDepartamentos = htmlRegraNegocios.GerarDepartamentosIdEmpresa(dadosTabela);
                    layoutProdutosDepartamentos = htmlRegraNegocios.GerarProdutosDepartamentosIdEmpresa(dadosTabela);
                    layoutContador = htmlRegraNegocios.stringScriptContador;

                    iFrameDepartamentos.Controls.Add(new LiteralControl(layoutDepartamentos));
                    iFrameProdutoDepartamentos.Controls.Add(new LiteralControl(layoutProdutosDepartamentos));

                    desc = htmlRegraNegocios.stringProdutos;
                    modal = htmlRegraNegocios.stringScriptModal;

                }
                else
                {
                    desc = htmlRegraNegocios.stringProdutos;
                    modal = htmlRegraNegocios.stringScriptModal;
                    layoutContador = "";
                }


                iFrameProdutos.Controls.Add(new LiteralControl(desc));
                iFrameScriptProdutos.Controls.Add(new LiteralControl(modal));
                iFrameScriptContador.Controls.Add(new LiteralControl(layoutContador));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}