<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pagina-nao-encontrado.aspx.cs" Inherits="TI.IDISQUE.UI.pagina_nao_encontrado" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->

    <title>404 - Pagina não localizada</title>

    <link href="https://fonts.googleapis.com/css?family=Montserrat:400" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=Chango" rel="stylesheet" />
    <link href="csss/pagina-nao-encontrado.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">

        <div id="notfound">

            <div class="notfound">

                <div>

                    <div class="notfound-404">
                        <h1>!</h1>
                    </div>

                    <h2 style="color: #ff6a00;">Error<br />
                        404</h2>
                </div>
                <br />
                <div id="error"></div>
            </div>
        </div>

    </form>
    <script src="scripts/pagina-nao-encontrado.js" type="text/javascript"></script>
</body>
</html>
