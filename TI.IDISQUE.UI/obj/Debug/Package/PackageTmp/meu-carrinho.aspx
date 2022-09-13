<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="meu-carrinho.aspx.cs" Inherits="TI.IDISQUE.UI.meu_carrinho" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=1.0, minimum-scale=1.0, maximum-scale=1.0" />
    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous"/>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <link href="csss/landing.css" rel="stylesheet" />
    <link href="csss/modal-login.css" rel="stylesheet" />
    <link href="csss/modal-maps.css" rel="stylesheet" />
    <%--   <link href="csss/modal-products.css" rel="stylesheet" />--%>
    <link href="csss/forma-pagamento.css" rel="stylesheet" />
    <link href="csss/css-mc/bootstrap.min.css" rel="stylesheet" />
    <link href="csss/css-mc/bootstraptable.min.css" rel="stylesheet" />
    <link href="csss/css-mc/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="csss/css-mc/jquery.growl.css" rel="stylesheet" />
    <link href="csss/css-mc/style.css" rel="stylesheet" />
    <link href="csss/meu-carrinho.css" rel="stylesheet" />

    <script src="//cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script src="sweetalert2.all.min.js"></script>
    <script src="sweetalert2.min.js"></script>
    <link rel="stylesheet" href="sweetalert2.min.css" />
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>

    <script src="https://momentjs.com/downloads/moment.min.js"></script>
    <script src="https://momentjs.com/downloads/moment-timezone-with-data.min.js"></script>

    <title>Idisque App-meu carrinho</title>
    <style>
        .modal-body {
            position: relative;
            padding: 0px;
        }

        .modal-header .close {
            padding: 0rem 1rem;
            margin: -1rem -1rem -1rem auto;
        }

        .form-group {
            margin-bottom: 20px;
        }

        .col-lg-1, .col-lg-10, .col-lg-11, .col-lg-12, .col-lg-2, .col-lg-3, .col-lg-4, .col-lg-5, .col-lg-6, .col-lg-7, .col-lg-8, .col-lg-9, .col-md-1, .col-md-10, .col-md-11, .col-md-12, .col-md-2, .col-md-3, .col-md-4, .col-md-5, .col-md-6, .col-md-7, .col-md-8, .col-md-9, .col-sm-1, .col-sm-10, .col-sm-11, .col-sm-12, .col-sm-2, .col-sm-3, .col-sm-4, .col-sm-5, .col-sm-6, .col-sm-7, .col-sm-8, .col-sm-9, .col-xs-1, .col-xs-10, .col-xs-11, .col-xs-12, .col-xs-2, .col-xs-3, .col-xs-4, .col-xs-5, .col-xs-6, .col-xs-7, .col-xs-8, .col-xs-9 {
            position: relative;
            min-height: 1px;
            padding-right: 9px;
            padding-left: 9px;
            margin-top: 4px;
        }

        .modal-header {
            padding: 4px;
            border-bottom: 1px solid #e5e5e5;
        }

        .modal-footer {
            display: contents;
            flex-wrap: wrap;
            flex-shrink: 0;
            align-items: center;
            justify-content: flex-start;
            padding: 1.75rem;
            border-top: 1px solid #dee2e6;
            border-bottom-right-radius: calc(.3rem - 1px);
            border-bottom-left-radius: calc(.3rem - 1px);
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <nav>
            <div class="topnav" style="color: white">
                <a class="fa fa-cart-plus size car">&nbsp;&nbsp;Carrinho de Compras</a>
            </div>
        </nav>
        <main class="container">
            <div class="footer">
                <p style="text-align: center">Lista de Itens do meu carrinho</p>
                <div class="col-md-12">
                    <hr />
                    <div id="itens" class="itens">
                    </div>
                    <div class="classtotal"><strong id="total"><span></span></strong></div>
                    <div id="npedido">Pedido Nº:0000000000</div>

                    <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#staticBackdrop" onclick="Products.open()" style="width: 100%"><span class='glyphicon glyphicon-usd'></span><a id="btnfecharvenda">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Fechar Venda R$: 0,00</a></button>
                    <br />
                    <br />
                    <button type="button" onclick="limparCarrinho();" class="btn btn-danger" style="width: 100%"><span class='glyphicon glyphicon-trash'></span>&nbsp;Limpar Carrinho</button>
                </div>
            </div>

        </main>

        <footer>
            <div class="foot-car">
                <div>
                    <a><i class="fa fa-home home" aria-hidden="true" onclick="voltarPagina()"></i></a>
                </div>
                <div>
                </div>
                <div>
                    <i class="fa fa-map-marker size maker" aria-hidden="true"></i>
                </div>
            </div>
        </footer>
    </form>

    <!-- the modal products -->
    <div class="modal-products" id="products">
        <div class="login">

            <div class="content-product">

                <div class="row">
                    <div class="col-lg-4 col-md-3 col-sm-4">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="exampleModalLabel"><span class='glyphicon glyphicon-th'></span>&nbsp;&nbsp;&nbsp;&nbsp;TELA DE PAGAMENTO</h5>

                                    <button type="button" class="close" data-dismiss="modal" aria-label="Fechar" onclick="Products.close()">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div></div>
                                    <div class="form-group">
                                        <label for="tipoos" class="col-md-12 control-label"><span class='glyphicon glyphicon-usd'></span>&nbsp;&nbsp;&nbsp;&nbsp;Selecione forma de Pagamento</label>
                                        <div class="col-md-12">
                                      <%--      <select class="form-control" id="tipopagamento" name="tipopagamento" data-placeholder="* Selecione Tipo de Pagamento">
                                                <option value=""></option>
                                                <option value="1">1-Dinheiro</option>
                                                <option value="2">2-Crédito</option>
                                                <option value="3">3-Débito</option>
                                                <option value="4">4-PIX: Telefone - 16997824414</option>
                                            </select>--%>
                                            <div id="selectpagamento"></div>
                                        </div>

                                        <label for="tipoos" class="col-md-12 control-label"><span class='glyphicon glyphicon-lock'></span>&nbsp;&nbsp;&nbsp;&nbsp;* Selecione forma de Retirada</label>
                                        <div class="col-md-12">
                                            <select class="form-control" id="retirada" name="retirada" data-placeholder="Selecione a cidade" onclick="cliquebalcaoFormapagamento()">
                                                <option value=""></option>
                                                <option value="1">1-Balcão</option>
                                                <option value="2">2-Delivery</option>
                                            </select>
                                        </div>

                                        <label for="tipoos" class="col-md-12  control-label"><span class='glyphicon glyphicon-usd'></span>&nbsp;&nbsp;&nbsp;&nbsp;Informe R$ Valor em Troco</label>
                                        <div class="col-md-12">
                                            <input class="form-control" id="valor" style="text-align: right" type="text" name="valor" placeholder="0,00" onkeypress="return(moeda(this,'.',',',event))" />
                                        </div>

                                        <div class="col-md-12">
                                            <form id="form2">
                                                <div class="form-group">
                                                    <label for="email"><span class='glyphicon glyphicon-home'></span>&nbsp;&nbsp;&nbsp;&nbsp;* Informe Endereço com Nº do Local.</label>
                                                    <input type="text" class="form-control" id="endereco" placeholder="Ex. Avenida 7 de setembro, 123" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="pwd"><span class='glyphicon glyphicon-road'></span>&nbsp;&nbsp;&nbsp;&nbsp;Informe uma Referencia:</label>
                                                    <input type="text" class="form-control" id="referencia" placeholder="Ao lado posto combustivel" />
                                                </div>
                                                <div id="map">
                                                    <script
                                                        src="https://maps.googleapis.com/maps/api/js?key=AIzaSyAbCOxmy2z-qt331qw2evTQA0ZcmhY0RgQ&callback=buscarcepapi&libraries=places=weekly" async="async">
                                                    </script>
                                                </div>
                                                <%--     <div id="right-panel"></div>--%>

                                                <p class="precotaxa" id="precotaxa">R$ Taxa de Entrega: 0,00</p>

                                                <div class="modal-footer">

                                                    <h5 class="valorTotal">
                                                        <strong id="total-compra">0,00<span></span></strong>
                                                    </h5>
                                                    <a class="btn btn-success" style="width: 100%" id="buscar-cep" onclick="buscarcep()"><span class='glyphicon glyphicon-map-marker'></span>&nbsp;&nbsp;&nbsp;&nbsp;Calcular Entrega por CEP</a>
                                                    <a class="btn btn-primary" style="width: 100%" onclick="salvarServProc()"><span class='glyphicon glyphicon-ok'></span><span id="mensagem-sucesso">Finalizar Pagamento&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></a>
                                                    <button type="button" style="width: 100%" class="btn btn-secondary" onclick="novaformapagamento()"><span class='glyphicon glyphicon-repeat'></span>&nbsp;&nbsp;&nbsp;&nbsp;Nova Forma Pagamento&nbsp;&nbsp;&nbsp;</button>
                                                </div>

                                                <strong id="pedido-fp" style="text-align: center; float: unset;">Numero do seu Pedido: 000000000000</strong>

                                            </form>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        var somacompra = window.document.getElementById("btnfecharvenda");
        var numpedido = window.document.getElementById("npedido");

        const Products = {
            open() {
                // Abrir Modal
                // Adicionar a classe active ao Modal
                document.getElementById('products').classList.add('active');
                window.document.getElementById("pedido-fp").innerHTML = "Nº seu pedido: " + numpedido.innerHTML.toString().trim();
                window.document.getElementById("total-compra").innerHTML = somacompra.innerHTML.replace("Fechar Venda", "").toString().trim();

                document.getElementById("tipopagamento").value = "";
                window.document.getElementById('retirada').value = "";
                window.document.getElementById('valor').value = "";
                window.document.getElementById('endereco').value = "";
                window.document.getElementById('referencia').value = "";
                window.document.getElementById('precotaxa').value = "R$ Taxa de Entrega: 0,00";

                document.getElementById("valor").style.backgroundColor = 'White';
                document.getElementById("endereco").style.backgroundColor = 'White';
                document.getElementById("referencia").style.backgroundColor = 'White';
                window.document.getElementById('valor').value = "0,00";
                window.document.getElementById("precotaxa").innerHTML = "Distância em KM: 0.000 - Entrega R$: 0.00.";
                window.document.getElementById('mensagem-sucesso').innerHTML = "&nbsp;&nbsp;&nbsp;&nbsp;Finalizar Pagamento&nbsp;" + somacompra.innerHTML.replace("Fechar Venda", "").replace("R$:", "R$: ").toString().trim();
            },
            close() {
                // Fechar o Modal
                //Remover a classe active do Modal
                document.querySelector('.modal-products').classList.remove('active');
            },
        };

        function voltarPagina() {
           var idpessoa = sessionStorage.getItem('idpessoa');

            window.location.assign('index.aspx?id=' + idpessoa)
        }

    </script>

    <script src="scripts/meu-carrinho.js" type="text/javascript"></script>

</body>
</html>
