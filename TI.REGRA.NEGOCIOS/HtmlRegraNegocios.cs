using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TI.REGRA.NEGOCIOS
{
    public class HtmlRegraNegocios
    {
        PessoaRegraNegocios pessoaRegraNegocios;

        public string stringProdutoRet = "";
        public string stringProdutos = "";
        public string stringScriptModal = "";
        public string stringScriptContador = "";
        public string variavelHora = "";


        public string GerarLayoutRazaoEmpresa(string fantasia, string urlImgLogo, string horaabertura, string horafechamento)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                variavelHora = Convert.ToDateTime(ObterHorarioBrasilia()).ToString("HH:mm");

                if (Convert.ToDateTime(horaabertura) > Convert.ToDateTime(variavelHora))
                {
                    sb.Append("<a href=\"#\">");
                    sb.Append("<img class=\"logo\" src=\"" + urlImgLogo + "\">");
                    sb.Append("</a>");
                    sb.Append("<div class=\"info\">");
                    sb.Append("<h2 class=\"name-logo\" id=\"name-logo\">" + fantasia + "</h2>");
                    sb.Append("<button style=\"color: red; background - color: red; border: 3px solid red\" class=\"flex button button1\">");
                    sb.Append("<p>Ops!!! Estabelicimento Fechado.</p>");
                    sb.Append("<i class=\"fa fa-clock-o left-timer\" aria-hidden=\"true\"></i>");
                    sb.Append("</button>");
                    sb.Append("</div>");
                }
                else
                {
                    if (Convert.ToDateTime(variavelHora) < Convert.ToDateTime(horafechamento))
                    {
                        sb.Append("<a href=\"#\">");
                        sb.Append("<img class=\"logo\" src=\"" + urlImgLogo + "\">");
                        sb.Append("</a>");
                        sb.Append("<div class=\"info\">");
                        sb.Append("<h2 class=\"name-logo\" id=\"name-logo\">" + fantasia + "</h2>");
                        sb.Append("<button class=\"flex button button1\">");
                        sb.Append("<p>Estabelicimento Aberto.</p>");
                        sb.Append("<i class=\"fa fa-clock-o left-timer\" aria-hidden=\"true\"></i>");
                        sb.Append("</button>");
                        sb.Append("</div>");
                    }
                    else
                    {
                        sb.Append("<a href=\"#\">");
                        sb.Append("<img class=\"logo\" src=\"" + urlImgLogo + "\">");
                        sb.Append("</a>");
                        sb.Append("<div class=\"info\">");
                        sb.Append("<h2 class=\"name-logo\" id=\"name-logo\">" + fantasia + "</h2>");
                        sb.Append("<button style=\"color: red; background - color: red; border: 3px solid red\" class=\"flex button button2\">");
                        sb.Append("<p>Ops!!! Estabelicimento Fechado.</p>");
                        sb.Append("<i class=\"fa fa-clock-o left-timer\" aria-hidden=\"true\"></i>");
                        sb.Append("</button>");
                        sb.Append("</div>");
                    }
                }

                return sb.ToString();
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string GerarScriptlistajson(string _horaabertura, string _horafechamento, string _fantasia, string _urlImagemLogo)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("<script type=\"text/javascript\">");

                sb.Append("function getdadosEmpresa() {");
                sb.Append("var tbLista = localStorage.getItem(\"tbListaEmpresa\");");

                sb.Append("var usuario = JSON.stringify({");
                sb.Append("Fantasia: " + _fantasia + ",");
                sb.Append("Hora_Entrada: " + _horaabertura + ",");
                sb.Append("Hora_Saida: " + _horafechamento);

                sb.Append("});");

                sb.Append("tbLista.push(usuario);");
                sb.Append("localStorage.setItem(\"tbListaEmpresa\", JSON.stringify(tbLista));");

                sb.Append("}");

                sb.Append("</script>");

                return sb.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DateTime ObterHorarioBrasilia()
        {
            DateTime DateTimeUtc = DateTime.UtcNow;

            TimeZoneInfo TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");//(GMT-03:00) Brasília

            DateTime DateTimeBrasilia = TimeZoneInfo.ConvertTimeFromUtc(DateTimeUtc, TimeZoneInfo);

            return DateTimeBrasilia;
        }

        public string GerarHorarioFuncionamento(string empresa, DataTable dadosTabela)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("<tbody>");

                StringBuilder sbdia = new StringBuilder();

                for (int i = 0; i < dadosTabela.Rows.Count; i++)
                {
                    sbdia.Append("<tr>");
                    sbdia.Append("<th scope=\"row\">Dom</th>");
                    sbdia.Append("<td colspan=\"2\">07:30</td>");
                    sbdia.Append("<td>13:00</ td >");
                    sbdia.Append("</tr>");
                }

                sb.Append(sbdia);

                sb.Append("</tbody>");

                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string GerarDepartamentosIdEmpresa(DataTable dadosTabela)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < dadosTabela.Rows.Count; i++)
                {
                    string dep = dadosTabela.Rows[i]["DEPARTAMENTO_DESC"].ToString().Trim();

                    sb.Append("<a href=\"#" + dep + "\">" + dep + "</a>");
                    sb.Append("\n");

                    string x = sb.ToString();
                }

                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public string GerarProdutosDepartamentosIdEmpresa(DataTable dadosTabela)
        {
            string htmls = "";

            try
            {
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < dadosTabela.Rows.Count; i++)
                {
                    int idDep = Convert.ToInt32(dadosTabela.Rows[i]["ID"].ToString());
                    string dep = dadosTabela.Rows[i]["DEPARTAMENTO_DESC"].ToString().Trim();
                    string obs = dadosTabela.Rows[i]["OBS"].ToString().Trim();

                    htmls += PesquisarProdutoIdDepartamentos(idDep, dep, obs);
                }

                return htmls;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string PesquisarProdutoIdDepartamentos(int idDep, string dep, string obs)
        {
            try
            {
                DataTable dadosTabelaProdutos = new DataTable();

                pessoaRegraNegocios = new PessoaRegraNegocios();

                dadosTabelaProdutos = pessoaRegraNegocios.PesquisarProdutoIdDepartameto(idDep);

                StringBuilder sb = new StringBuilder();

                int id = 0;
                string preco = "";
                string url = "";
                string produto = "";
                string detalhes = "";
                string obsProd = "";

                if (dadosTabelaProdutos.Rows.Count > 0)
                {
                    for (int i = 0; i < dadosTabelaProdutos.Rows.Count; i++)
                    {
                        id = Convert.ToInt32(dadosTabelaProdutos.Rows[i]["ID"].ToString());
                        preco = dadosTabelaProdutos.Rows[i]["PRECO"].ToString().Trim();
                        url = dadosTabelaProdutos.Rows[i]["URL"].ToString().Trim();
                        produto = dadosTabelaProdutos.Rows[i]["DESCRICAO"].ToString().Trim();
                        obsProd = dadosTabelaProdutos.Rows[i]["OBS"].ToString().Trim();
                        detalhes = dadosTabelaProdutos.Rows[i]["DETALHES"].ToString().Trim();

                        stringProdutos += GerarModalProdutos(id, url, produto, detalhes, preco, obsProd, obs);
                        stringScriptModal += GerarScriptModal(id);

                        stringScriptContador += GerarSomaQuantidade(id);

                        if (i == 0)
                        {
                            sb.Append("<section class=\"food-savory\">");
                            sb.Append("<div class=\"savory\" id=\"" + dep + "\">");

                            sb.Append("<a onclick=\"Products" + id + ".open()\">");

                            sb.Append("<div class=\"card make-it-slow make-it-fast\">");
                            sb.Append("<div class=\"card-content\">");
                            sb.Append("<div class=\"title\">");
                            sb.Append("<span id=\"produtos" + id + "\">" + produto + "</span>");
                            sb.Append("<br />");
                            sb.Append("<strong id=\"preco" + id + "\" class=\"price\">R$" + preco + "</strong>");

                            if (obsProd != "")
                            {
                                sb.Append("<p id=\"observacao" + id + "\" class=\"obs\"><i class=\"fal fa-file-alt\"></i></i>" + obsProd.ToLower() + "</p>");
                            }

                            sb.Append("</div>");

                            sb.Append("<img class=\"img-content\" src=\"" + url + "\" alt=\"" + obsProd.ToLower() + "\"/>");

                            sb.Append("</div>");
                            sb.Append("</div>");
                            sb.Append("</a>");
                        }
                        else
                        {
                            sb.Append("<a onclick=\"Products" + id + ".open()\">");

                            sb.Append("<div class=\"card make-it-slow make-it-fast\">");
                            sb.Append("<div class=\"card-content\">");
                            sb.Append("<div class=\"title\">");
                            sb.Append("<span>" + produto + "</span>");
                            sb.Append("<br />");
                            sb.Append("<strong class=\"price\">R$" + preco + "</strong>");

                            if (obsProd != "")
                            {
                                sb.Append("<p class=\"obs\"><i class=\"fal fa-file-alt\"></i></i>" + obsProd.ToLower() + "</p>");
                            }

                            sb.Append("</div>");
                            sb.Append("<img class=\"img-content\" src=\"" + url + "\" alt=\"" + obsProd.ToLower() + "\"/>");
                            sb.Append("</div>");
                            sb.Append("</div>");
                            sb.Append("</a>");
                        }
                    }


                    sb.Append("</div>");
                    sb.Append("</section>");



                    stringProdutoRet = sb.ToString();
                }
                else
                {
                    stringProdutoRet = "";
                }

                return stringProdutoRet;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string GerarScriptModal(int id)
        {
            try
            {
                if (id > 0)
                {
                    StringBuilder sb = new StringBuilder();

                    sb.Append("<script>");

                    sb.Append(" const Products" + id + " = {");

                    sb.Append("open() {");
                    // Abrir Modal
                    // Adicionar a classe active ao Modal
                    sb.Append("document.getElementById('products" + id + "').classList.add('active');");
                    sb.Append("},");

                    sb.Append("close() {");
                    // Fechar o Modal
                    //Remover a classe active do Modal
                    sb.Append("document.getElementById('products" + id + "').classList.remove('active');");
                    sb.Append("}");
                    sb.Append("};");

                    sb.Append("</script>");

                    return sb.ToString();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string GerarSomaQuantidade(int id)
        {
            try
            {
                StringBuilder sb = new StringBuilder();



                sb.Append("<script type=\"text/javascript\">");

                sb.Append("function processar" + id + "(quant) {");

                sb.Append("var qtdes = parseInt(document.getElementById(\"quant" + id + "\").value);");

                sb.Append("var preco = document.getElementById(\"preco" + id + "\").innerHTML.replace(\"R\", \"\").replace(\"$\", \"\").replace(\":\", \"\").replace(\",\", \".\");"); ;

                sb.Append("qtdes += quant;");

                sb.Append("if (qtdes < 1)");
                sb.Append("{");
                sb.Append("document.getElementById(\"quant" + id + "\").value = 1;");
                sb.Append("}");
                sb.Append("else");
                sb.Append("{");
                sb.Append("document.getElementById(\"quant" + id + "\").value = qtdes;");
                sb.Append("}");
                sb.Append("var qtde = parseFloat(qtdes);");
                sb.Append("var pco = parseFloat(preco);");
                sb.Append("let total = (qtde * pco);");
                sb.Append("window.document.getElementById(\"total" + id + "\").innerHTML = \"R$: \" + total.toFixed(2).toString().replace(\".\", \",\").trim();");
                sb.Append("}");

                sb.Append("</script>");

                return sb.ToString();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string GerarModalProdutos(int id, string imagem, string descricao, string detalhes, string preco, string obsProd, string obsDep)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                if (id > 0)
                {
                    sb.Append("<div class=\"modal-products\" id=\"products" + id + "\">");

                    sb.Append("<div class=\"login\">");
                    sb.Append("<div class=\"content-product\">");
                    sb.Append("<br/>");
                    sb.Append("<div class=\"row\">");
                    sb.Append("<div class=\"col-lg-4 col-md-4 col-sm-4\">");
                    sb.Append("<div class=\"modal-header\">");
                    sb.Append("<h5 class=\"modal-title\"></h5>");
                    sb.Append("<button type=\"button\" class=\"close-x\" data-bs-dismiss=\"modal\" aria-label=\"Close\" onclick=\"Products" + id + ".close()\">");
                    sb.Append("<span aria-hidden=\"true\"></span>");
                    sb.Append("<i class=\"fa fa-arrow-left\" aria-hidden=\"true\"></i>");
                    sb.Append("</button>");
                    sb.Append("<h3></h3>");
                    sb.Append("</div>");
                    sb.Append("<div>");
                    sb.Append("<img src=\"" + imagem + "\" class=\"img-fluid\"/>");
                    sb.Append("</div>");
                    sb.Append("<div class=\"description\">");
                    sb.Append("<h2>" + descricao + "</h2>");
                    sb.Append("<strong>" + detalhes + "</strong>");
                    sb.Append("<address>" + obsProd + "</address>");
                    sb.Append("</div>");
                    sb.Append("<div class=\"content-info\">");
                    sb.Append("<div>");
                    sb.Append("<div>");
                    sb.Append("<div class=\"buttons\">");

                    sb.Append("<div data-app=\"product.quantity\" id=\"quantidade" + id + "\">");

                    sb.Append("<label>Quantidade: </label>");
                    sb.Append("<input style=\"color: white; background-color: red; width:40px;\" type =\"button\" id=\"plus" + id + "\" value='-' onclick=\"processar" + id + "(-1)\"/>");
                    sb.Append("<input style=\"text-align:center;\" id =\"quant" + id + "\" name=\"quant\" class=\"text-break\" type=\"number\" value=\"1\" min=\"1\" max=\"10\"/>");
                    sb.Append("<input style=\"color: white; background-color: darkgreen; width:40px;\" type=\"button\" id=\"minus" + id + "\" value='+' onclick=\"processar" + id + "(1)\"/>");

                    sb.Append("</div>");

                    sb.Append("</div>");
                    sb.Append("</div>");
                    sb.Append("</div>");

                    sb.Append("<div>");
                    sb.Append("<h4>R$:<strong id=\"preco" + id + "\">" + preco + "</strong></h4>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                    sb.Append("<br/>");

                    sb.Append("<div>");
                    sb.Append("<textarea id=\"obsDep" + id + "\" placeholder=\"" + obsDep + "\" name=\"\" cols=\"10\" rows=\"4\"></textarea>");
                    sb.Append("</div>");

                    sb.Append("</div>");
                    sb.Append("<div class=\"content-button\">");
                    sb.Append("<button class=\"add-button\" onclick=\"AdicionarItemVenda(" + id + ", '" + descricao + "', '" + preco + "', '" + detalhes + "', '" + imagem + "')\">");
                    sb.Append("<i class=\"fa fa-shopping-cart\" aria-hidden=\"true\"></i>");
                    sb.Append("<p>Adicionar ao Pedido</p>");
                    sb.Append("<p id=\"total" + id + "\">R$ " + preco + "</p>");
                    sb.Append("</button>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                    sb.Append("</div>");

                    string x = sb.ToString();
                }

                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
