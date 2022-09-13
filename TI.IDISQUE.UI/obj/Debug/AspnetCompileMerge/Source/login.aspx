<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="TI.IDISQUE.UI.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Idisque</title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="csss/csss-login/log.css" rel="stylesheet" />
    <script src="assets/js/jquery-3.1.1.min.js"></script>
</head>
<body>
    <div class="limiter">
        <div class="container-login100">
            <div class="wrap-login100">
                <div class="login100-pic js-tilt" data-tilt="data-tilt">
                    <img src="img/login/images.png" alt="IMG" />
                </div>

                <form id="form1" runat="server" class="login100-form validate-form" />
                <span class="login100-form-title">Meu Primeiro Acessos
                </span>

                <div class="wrap-input100 validate-input" data-validate="Valid email is required: ex@abc.xyz">
                    <input class="input100" id="nome" type="text" placeholder="Nome Usuário" />
                    <span class="focus-input100"></span>
                    <span class="symbol-input100">
                        <i class="fa fa-user" aria-hidden="true"></i>
                    </span>
                </div>

                <div class="wrap-input100 validate-input" data-validate="Password is required">
                    <%--  <input class="input100" id="celular" type="text" name="pass" placeholder="Nº Celular" />--%>
                    <input class="input100" type="text" name="telefone" id="telefone" placeholder="(XX) XXXX-XXXXX" maxlength="15" onkeypress="mask(this, mphone);" onblur="mask(this, mphone);" />
                    <span class="focus-input100"></span>
                    <span class="symbol-input100">
                        <i class="fa fa-phone" aria-hidden="true"></i>
                    </span>
                </div>

                <div class="container-login100-form-btn">
                    <button class="login100-form-btn" onclick="Adicionar()">
                        Login
                   
                    </button>
                </div>

                <div class="text-center p-t-12">
                    <span class="txt1">Forgot
                    </span>
                    <a class="txt2" href="#">Username / Password?
                    </a>
                </div>

                <div class="text-center p-t-136">
                    <a class="txt2" href="#">Create your Account
						
                        <i class="fa fa-long-arrow-right m-l-5" aria-hidden="true"></i>
                    </a>
                </div>
                <script src="scripts/login.js" type="text/javascript"></script>
            </div>
        </div>
    </div>

</body>
</html>
