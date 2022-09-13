<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="TI.IDISQUE.UI.rumos_tec" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=1.0, minimum-scale=1.0, maximum-scale=1.0" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>  
    <link href="//cdnjs.cloudflare.com/ajax/libs/jqueryui/1.12.1/themes/cupertino/jquery-ui.min.css" rel="stylesheet" />
    <link href="//cdnjs.cloudflare.com/ajax/libs/fullcalendar/3.1.0/fullcalendar.min.css" rel="stylesheet" />
    <link href="//cdnjs.cloudflare.com/ajax/libs/qtip2/3.0.3/jquery.qtip.min.css" rel="stylesheet" />

    <script src="https://momentjs.com/downloads/moment.min.js"></script>
    <script src="https://momentjs.com/downloads/moment-timezone-with-data.min.js"></script>

    <script src="JQuery.js" type="text/javascript"></script>

    <link href="csss/landing.css" rel="stylesheet" />
    <link href="csss/main.css" rel="stylesheet" />
    <link href="csss/modal-login.css" rel="stylesheet" />

    <title>Idisque App</title>

    <style>
        element.style {
        }

        .img-fluid {
            width: 360px;
            height: 200px;
            border-radius: 0px 0px 0px 0px;
        }

        .close-x {
            float: left;
            font-size: 1.5rem;
            font-weight: 700;
            line-height: 1;
            color: #000;
            text-shadow: 0 1px 0 #e92929;
            opacity: .5;
            color: red;
        }

        .fa fa-arrow-left {
            /*content: "\f060";*/
            background-color: red;
        }

    </style>
</head>

<body>
    <header>
        <div id="idEstabelecimento"></div>
    </header>

    <nav>
        <div class="topnav scrollmenu">
            <asp:PlaceHolder ID="iFrameDepartamentos" runat="server" />
        </div>
    </nav>

    <main class="container">
        <asp:PlaceHolder ID="iFrameProdutoDepartamentos" runat="server" />
    </main>

    <br />

    <footer>
        <div class="foot-car">
            <div>
                <a onclick="logaut()">
                    <i class="fa fa-home home" aria-hidden="true"></i>
                </a>
            </div>
            <div>
                <div id="qtdemeucarrinho"></div>
            </div>
            <div>
                <a onclick="Maps.open()">
                    <i class="fa fa-map-marker size maker" aria-hidden="true"></i>
                </a>
            </div>
        </div>

        <asp:PlaceHolder ID="iFrameScriptContador" runat="server" />

    </footer>

    <!-- the modal landing -->
    <div class="modal-overlay" id="modal-ls">
        <div class="landing">
            <img src="https://cdn.dribbble.com/users/645440/screenshots/3266490/loader-2_food.gif" alt="" />
            <p class="logo-name">Idisque App</p>
            <p class="empresa-name">TI Corporation © - versão 08.2021</p>
        </div>
    </div>


    <%-- the modal login --%>
    <div class="modal-login" id="login-lss">
        <div class="login">
            <div class="login-wrap">
                <div class="login-html">
                    <input id="tab-1" type="radio" name="tab" class="sign-in" checked="checked" /><label for="tab-1" class="tab">Sign In</label>
                    <input id="tab-2" type="radio" name="tab" class="sign-up" /><label for="tab-2" class="tab">Sign Social</label>
                    <div class="login-form">
                        <div class="sign-in-htm">
                            <div class="group">
                                <label for="user" class="label">Name</label>
                                <input id="user" type="text" class="input" />
                            </div>
                            <div class="group">
                                <label for="pass" class="label">WhatsApp</label>
                                <input id="pass" type="password" class="input" data-type="password" />
                            </div>

                            <div class="group">
                                <input type="submit" class="button" value="Sign In" />
                            </div>
                            <div class="hr"></div>
                            <div class="foot-lnk">
                                <a href="#forgot" onclick="Login.close()">sair</a>
                            </div>
                        </div>
                        <div class="sign-up-htm">
                            <div class="group">
                                <a href="#" class="social-button" id="google-connect"><span>Connect with Google</span></a>
                            </div>
                            <div class="group">
                                <a href="#" class="social-button" id="facebook-connect"><span>Connect with Facebook</span></a>
                            </div>
                            <div class="hr"></div>
                            <div class="foot-lnk">
                                <a href="index.aspx" onclick="Login.close()">sair</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <asp:PlaceHolder ID="iFrameProdutos" runat="server" />
    <asp:PlaceHolder ID="iFrameScriptProdutos" runat="server" />

    <script>
        function fecharpedido() {
            window.location.href = "meu-carrinho.aspx";
        };
    </script>

    <script src="scripts/index.js" type="text/javascript"></script>
    <link href="csss/modal-products.css" rel="stylesheet" />
    <script src="scripts/vendas.js" type="text/javascript"></script>

</body>

</html>
