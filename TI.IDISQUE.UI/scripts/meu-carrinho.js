var tbListaVenda = localStorage.getItem("tbListaVenda");
var tbListaEmpresa = localStorage.getItem("tbListaEmpresa");
var numeroPedido = 0;
var qtde = 0;
var id = 0;
var descricao = "";
var prod = "";
var preco = "";
var qt = 0;
var obs = "";
var total = 0;
//
var _pedido = "";
var _tipopagamento;
var _pagto = "";
var _troco = 0;
var _taxaEntrega = 0;
var _celularCliente = "";
var _nomecliente = "";
var _text = "";
var _total = 0;
var _subtotal = 0;
var _idEmpresa = 0;
var _fretirada = 0;
var _textoendereco;
var _referencia;
var _endereco;
var _tempoestimado;
var tot;

//EMPRESA
var idpessoa;
var _celularemitente;
var _nomeEmpresaZ;
var listaprodutos;
var statusEmpresa;
var _cidade;
var _cep;
var _numero;
var _chavepix;

//TAXA
var taxainicial;
var freteporKm;
var desDistancia;
var _somatotal;
var totnumber;
var freteEntrega;

// Converte string para objeto
tbListaVenda = JSON.parse(tbListaVenda);

gerarnumpedido();

listarVendas();

listarUsuario();

listarEmpresa();

gerarnumpedido();

getidempresa();

function listarVendas() {

    if (tbListaVenda != null) {

        var sub = 0;

        listaprodutos = "";

        for (var i = 0; i < tbListaVenda.length; i++) {

            var produts = JSON.parse(tbListaVenda[i]);

            id = produts.Id;
            descricao = produts.Descricao;
            prod = produts.Descricao;
            preco = produts.Preco;
            qt = produts.Quantidade;
            obs = produts.Observacao;
            total = produts.Total;

            if (prod != null) {

                // exibe os dados da lista dentro da div itens
                document.getElementById("itens").innerHTML += "<button class='btn btn-danger btn-xs btnExcluir' data-title='Delete' onclick='Excluir(" + i + ")'><span class='glyphicon glyphicon-trash'></span></button>&nbsp;&nbsp;";
                document.getElementById("itens").innerHTML += qt + " X - ";
                document.getElementById("itens").innerHTML += descricao;
                document.getElementById("itens").innerHTML += " ";

                //LISTA DE PRODUTOS PARA VENDA
                if (produts.Observacao == "") {
                    listaprodutos += produts.Quantidade + "X " + produts.Descricao.padStart(10, " -").toUpperCase() + " - R$: " + produts.Preco + ".%0A";
                } else {
                    listaprodutos += produts.Quantidade + "X " + produts.Descricao.padStart(10, " -").toUpperCase() + " - R$: " + produts.Preco + "%0A*OBS:%20" + produts.Observacao.toUpperCase().trim() + "*.%0A%0A";
                }

                if (obs === "") {
                    document.getElementById("itens").innerHTML += preco.replace("R$:", "").replace(".", ",") + "<br />R$ TOTAL: " + total + "<hr>";
                } else {
                    document.getElementById("itens").innerHTML += preco.replace(".", ",") + "<br />R$ TOTAL: " + total + "<br /><strong>OBS: " + obs + "</strong><hr>";
                }

                // calcula o total dos recheios
                var totals = parseFloat(total.replace("R", "").replace("$", "").replace(":", "").replace(" ", "").replace(",", ".")); // arredonda para 2 casas decimais com o .toFixed(2)

                sub += parseFloat(totals);
            }
        }

        window.document.getElementById("btnfecharvenda").innerHTML = "Fechar Venda R$:" + sub.toFixed(2).toString().replace(".", ",");
    } else {
        history.go(-1);
    }
}

function somaTotal() {
    for (var i = 0; i < numeros.length; i++) {
        total += numeros[i];
    }
}

function listaqtdeitens() {
    qtde = tbListaVenda.length;

    if (qtde > 0) {
        html = '<i class="fa fa-cart-plus size car" aria-hidden="true" onclick="fecharpedido()"></i>' + '<p class="circle"><strong class="car-number">' + (qtde) + '</strong>';
    } else {
        html = '<i class="fa fa-cart-plus size car" aria-hidden="true" onclick="fecharpedido()"></i>' + '<p class="circle"><strong class="car-number">0</strong></p>';
    }

    window.document.getElementById('qtdemeucarrinho').innerHTML = html;
}

function gerarnumpedido() {
    var data = new Date();
    var dia = String(data.getDate()).padStart(2, '0');
    var mes = String(data.getMonth() + 1).padStart(2, '0');
    var ano = data.getUTCFullYear();
    var hora = String(data.getHours()).padStart(2, '0');
    var min = String(data.getMinutes()).padStart(2, '0');
    var seg = String(data.getSeconds()).padStart(2, '0');

    _pedido = ("P" + ano + mes + dia + hora + min + seg);

    var numero = window.document.getElementById("npedido").innerHTML = _pedido;
}

function limparCarrinho() {
    history.go(-1);
    localStorage.removeItem("tbListaVenda");
}

function Excluir(indice) {
    tbListaVenda.splice(indice, 1);
    localStorage.setItem("tbListaVenda", JSON.stringify(tbListaVenda));

    qtde = tbListaVenda.length;

    if (qtde == 0) {
        limparCarrinho();
    } else {
        // Recarrega a página atual sem usar o cache
        document.location.reload(true);
    }
}

function salvarServProc() {

    _tipopagamento = window.document.getElementById("tipopagamento").value;
    _fretirada = window.document.getElementById("retirada").value.toUpperCase();
    _troco = window.document.getElementById("valor").value;
    _taxaEntrega = window.document.getElementById("precotaxa").innerHTML.replace("R$", "").replace("Taxa de Entrega:", "").trim();
    _total = window.document.getElementById("total-compra").innerHTML.replace("R$:", "").trim();
    _textoendereco = window.document.getElementById("endereco").innerHTML.trim().toUpperCase();
    _referencia = window.document.getElementById("referencia").innerHTML.trim().toUpperCase();
    _pagto = window.document.getElementById("tipopagamento").value.trim().toUpperCase();

    var end = window.document.getElementById("endereco").value.trim().toUpperCase();
    var tx = window.document.getElementById("precotaxa").innerHTML.replace("Distância em KM: ", "").replace("- Entrega R$: 0.00.", "").trim();

    textoZap = "";

    if (_tipopagamento == "") {
        alert("Campo Tipo de Pagamento não pode ser Vázio");
        window.document.getElementById("tipopagamento").focus();
    }
    else if (_fretirada == "") {
        alert("Campo Tipo de Retirada não pode ser Vázio");
        window.document.getElementById("retirada").focus();
    }
    else if (_fretirada == 2 && end == "") {
        alert("Para Forma de Retirada Delivery Campo Endereço não pode ser vázio");
        window.document.getElementById("endereco").focus();
    } else if (_fretirada == 2 && tx == "0.000") {
        alert("Clique Botão Calcular Taxa de Entrega por CEP...");
        window.document.getElementById("endereco").focus();
    }
    else {

        enviarMessagemZap();

        var data = {
            pedido: {
                numeroPedido: _pedido,
                tipoPagamento: _tipopagamento,
                trocoPara: _troco,
                taxaEntrega: _taxaEntrega,
                telefone: _celularCliente,
                pedidotexto: textoZap,
                total: _total,
                idEmpresa: 1,
                formaRetirada: _fretirada
            }
        };

        $.ajax({
            url: "meu-carrinho.aspx/SalvarPedidoDelivery",
            data: JSON.stringify(data),
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (resultado) {

                if (resultado.d == "Pedido foi Realizado com Sucesso...") {
                    swal({
                        title: "Venda Realizada com Sucesso!",
                        text: "Clique Ok para Enviar Venda também por WattsZapp!",
                        icon: "success",
                        button: "OK!",
                    });

                    document.querySelector('.modal-products').classList.remove('active');

                    var valor = document.querySelector('.swal-overlay').onclick = function () {

                        let a = document.createElement('a');

                        a.target = '_blank';
                        a.href = textoZap.trim();
                        a.click();

                        localStorage.removeItem("tbListaVenda");

                        window.location.href = "index.aspx?id=" + idpessoa;
                    };

                    return true;

                } else {
                    fecharmodal();

                    swal({
                        title: "Erro no salvar pedido!",
                        text: "Erro ao Salvar seu pedido!",
                        icon: "warning",
                        button: "Error!",
                    });

                    return false;
                }
            }
        });

        function enviarMessagemZap() {

            textoZap = "";

            if (_pagto == 1) {
                _pagto = "DINHEIRO.";
            } else if (_pagto == 2) {
                _pagto = "2-CRÉDITO.";
            } else if (_pagto == 3) {
                _pagto = "3-DÉBITO.";
            } else {
                _pagto = "4-PIX.";
            }

            if (_fretirada == 1) {

                _taxaEntrega = "0,00";
                _fretirada = "BALCÃO";

                textoZap = ("https://api.whatsapp.com/send?phone=55" + _celularemitente + "&text=*" + _pedido + "*%20%20%0A%0A%0A" + _nomeEmpresaZ + "%20AGRADECE%20SUA%20PREFERENCIA%0A%0A*----------------------------*%0A%0A*Meu%20Nome%20é*%20*" + _nomecliente + "*%0A*Contato:*%20*" + _celularCliente + "*%0A%0A%0A" + listaprodutos.replace("undefined", " ").replace("R$:", " R$:").trim().trim() + "%0A%0A%0A*Subtotal%3A*%20R%24%20" + _total + "%0A*Taxa%20de%20entrega%3A*%20" + _taxaEntrega + "%0A*Taxa%20de%20embalagem%3A*%20Gr%C3%A1tis%0A%0A*Total:*%20R%24%20" + _total + "%0A%0A----------------------------%0A*Tipo%20Retirada:*%20" + _fretirada + _textoendereco + "%0A%0A*Pagamento%3A*%20" + _pagto + "%0A%0A%20%20%20%20%20%20%20%20%20%20%20%20SEU%20PEDIDO%20FOI%20REALIZADO%20COM%20SUCESSO%20%20%0A%0A%0A%0A*" + _tempoestimado + "*%0A%0AAcompanhe%20seu%20App%20%20em%3A%0A%20:%20http://idisque.com.br/" + _nomeEmpresaZ.toString().toLowerCase().replace(" ", "-") + ".aspx").replace("<hr>", "%0A%0A").replace("<strong>", "*");
            } else {
                _total = "0,00";
                _subtotal = "0,00";
                _fretirada = "DELIVERY";
                _taxaEntrega = freteEntrega.toFixed(2).replace(".", ",");
                _endereco = window.document.getElementById('endereco').value;
                _referencia = window.document.getElementById('referencia').value;
                _subtotal = window.document.getElementById('total-compra').innerHTML;
                _total = window.document.getElementById('mensagem-sucesso').innerHTML.replace("&nbsp;&nbsp;&nbsp;&nbsp;", "").replace("Finalizar Pagamento", "").replace("R$: ", "").trim();

                _textoendereco = "%0A%0A*ENDEREÇO%20ENTREGA:*%20%20" + _endereco.toUpperCase() + "%20.%0A*REFERENCIA:*%20" + _referencia.toUpperCase() + "%0A*TROCO%20PARA:*%20R$:%20" + _troco;

                textoZap = ("https://api.whatsapp.com/send?phone=55" + _celularemitente + "&text=*" + _pedido + "*%20%20%0A%0A%0A" + _nomeEmpresaZ + "%20AGRADECE%20SUA%20PREFERENCIA%0A%0A*----------------------------*%0A%0A*Meu%20Nome%20é*%20*" + _nomecliente + "*%0A*Contato:*%20*" + _celularCliente + "*%0A%0A%0A" + listaprodutos.replace("undefined", " ").replace("R$:", " R$:").trim().trim() + "%0A%0A%0A*Subtotal%3A*%20R%24%20" + _subtotal + "%0A*Taxa%20de%20entrega%3A*%20R$:%20" + _taxaEntrega + "%0A*Taxa%20de%20embalagem%3A*%20Gr%C3%A1tis%0A%0A*Total:*%20R%24%20" + _total + "%0A%0A----------------------------%0A*Tipo%20Retirada:*%20" + _fretirada + _textoendereco + "%0A%0A*Pagamento%3A*%20" + _pagto + "%0A%0A%20%20%20%20%20%20%20%20%20%20%20%20SEU%20PEDIDO%20FOI%20REALIZADO%20COM%20SUCESSO%20%20%0A%0A%0A%0A*" + _tempoestimado + "*%0A%0AAcompanhe%20seu%20App%20%20em%3A%0A%20:%20http://idisque.com.br/" + _nomeEmpresaZ.toString().toLowerCase().replace(" ", "-") + ".aspx").replace("<hr>", "%0A%0A").replace("<strong>", "*");
            }
        }

        //function sleepFor(sleepDuration) {
        //    var now = new Date().getTime();
        //    while (new Date().getTime() < now + sleepDuration) { /* Do nothing */ }
        //}
    }
}

function listarUsuario() {
    var tbLista = localStorage.getItem("tbListaUsuario");

    // Converte string para objeto
    tbLista = JSON.parse(tbLista);

    var user = JSON.parse(tbLista[0]);

    _celularCliente = user.Celular;
    _nomecliente = user.Nome;

    _nomeEmpresaZ = $(".name-logo").text();
}

function fecharmodal() {
    const Products = {
        close() {
            // Fechar o Modal
            //Remover a classe active do Modal
            document.querySelector('.modal-products').classList.remove('active');
        },
    };
}

function cliquebalcaoFormapagamento() {

    var tipopagamento = window.document.getElementById('tipopagamento').value;
    var formaretirada = window.document.getElementById('retirada').value;

    if (formaretirada == 1) {

        window.document.getElementById("endereco").value = "";
        window.document.getElementById('referencia').value = "";

        document.getElementById("endereco").disabled = true;
        window.document.getElementById('referencia').disabled = true;

        document.getElementById("valor").style.backgroundColor = 'LemonChiffon';
        window.document.getElementById('valor').value = "0,00";

        document.getElementById("endereco").style.backgroundColor = 'White';
        document.getElementById("referencia").style.backgroundColor = 'White';
    } else if (formaretirada == 2) {
        document.getElementById("endereco").disabled = false;
        window.document.getElementById('referencia').disabled = false;
        window.document.getElementById('buscar-cep').disabled = false;
        document.getElementById("valor").style.backgroundColor = 'LemonChiffon';
        document.getElementById("endereco").style.backgroundColor = 'LemonChiffon';
        document.getElementById("referencia").style.backgroundColor = 'LemonChiffon';
        window.document.getElementById('valor').value = "0,00";


    } else {
        document.getElementById("endereco").disabled = false;
        window.document.getElementById('referencia').disabled = true;
        window.document.getElementById('buscar-cep').disabled = true;
        document.getElementById("valor").style.backgroundColor = 'White';
        document.getElementById("endereco").style.backgroundColor = 'White';
        document.getElementById("referencia").style.backgroundColor = 'White';
        window.document.getElementById('valor').value = "0,00";
    }
};

function buscarcep() {

    var formaretirada = window.document.getElementById('retirada').value;

    if (formaretirada == 2) {
        var endereco = window.document.getElementById("endereco").value;
        var referencia = window.document.getElementById("retirada").value;

        if (endereco == "") {
            alert("Para Forma de Retirada Delivery Campo Endereço não pode ser vázio");
            window.document.getElementById("endereco").focus();
        } else {
            buscarcepapi();
        }
    } else {
        alert("Buscar CEP somente para Forma de Retirada Delivey.");
    }
}

function buscarcepapi() {

    var endereco = window.document.getElementById("endereco").value;

    if (endereco != "") {

        const map = new google.maps.Map(document.getElementById("map"), {
            zoom: 4,
            center: { lat: -21.7946, lng: -48.1766 }, // Araraquara.
        });

        const directionsService = new google.maps.DirectionsService();

        const directionsRenderer = new google.maps.DirectionsRenderer({
            draggable: true,
            map,
            panel: document.getElementById("right-panel"),
        });

        directionsRenderer.addListener("directions_changed", () => {
            computeTotalDistance(directionsRenderer.getDirections());
        });

        var araraquara = _cidade;
        var endcepemitente = (araraquara + ", " + _endereco + ", " + _numero);

        displayRoute(
          origin = endcepemitente,
          destination = araraquara + endereco,
          directionsService,
          directionsRenderer
        );

        return araraquara + endereco;
    }
}

function displayRoute(origin, destination, service, display) {
    if (destination != "") {
        service
         .route({
             origin: origin,
             destination: destination,
             // waypoints: [
             //   { location: "Adelaide, SA" },
             //   { location: "Broken Hill, NSW" },
             // ],
             travelMode: google.maps.TravelMode.DRIVING,
             avoidTolls: true,
         })
         .then((result) => {
             display.setDirections(result);
         })
         .catch((e) => {
             alert("Could not display directions due to: " + e);
         });
    }
}

function computeTotalDistance(result) {
    let totalDist = 0;
    const myroute = result.routes[0];

    if (!myroute) {
        return;
    }

    for (let i = 0; i < myroute.legs.length; i++) {
        totalDist += myroute.legs[i].distance.value;
    }

    totalDist = (totalDist / 1000);

    tot = document.getElementById("total-compra").innerHTML;

    taxainicial = 6.00;
    freteporKm = 0.85;
    desDistancia = 0.00;
    _somatotal = 0.00;
    totnumber = 0.00;
    freteEntrega = 0.00;

    console.log(taxainicial);
    console.log(freteporKm);
    console.log(totalDist);
    console.log(tot);

    if (totalDist.toFixed(3) <= 1.000) {
        freteEntrega = taxainicial;
        totnumber = Number(tot.replace(",", "."))
        _somatotal = (Number(totnumber) + Number(freteEntrega));

    } else {

        totnumber = Number(tot.replace(",", "."))
        freteEntrega = ((Number(totalDist - 1.000) * freteporKm) + taxainicial);
        _somatotal = (Number(freteEntrega) + Number(tot.replace(",", ".")));
    }

    desDistancia = ("Distância em KM: " + totalDist.toFixed(3) + " - ") + "Entrega R$: " + (freteEntrega.toFixed(2) + ".");

    document.getElementById("precotaxa").innerHTML = desDistancia;

    document.getElementById("total-compra").innerHTML = tot.replace(".", ",");

    var t = parseFloat(freteEntrega);
    var tt = tot.replace("R", "").replace("$", "").replace(":", "").replace(".", ",").trim();
    var x = (parseFloat(t) + parseFloat(tt));

    document.getElementById("mensagem-sucesso").innerHTML = "&nbsp;&nbsp;&nbsp;&nbsp;Finalizar Pagamento R$: " + x.toFixed(2).toString().replace(".", ",");
}

function novaformapagamento() {

    var valorcompra = window.document.getElementById("total-compra").innerHTML;

    document.getElementById("tipopagamento").value = "";
    window.document.getElementById('retirada').value = "";
    window.document.getElementById('valor').value = "";
    window.document.getElementById('endereco').value = "";
    window.document.getElementById('referencia').value = "";
    window.document.getElementById('precotaxa').value = "R$ Taxa de Entrega: 0,00";
    window.document.getElementById('mensagem-sucesso').innerHTML = "&nbsp;&nbsp;&nbsp;&nbsp;Finalizar Pagamento&nbsp;" + valorcompra.replace("R$:", "R$: ");

    document.getElementById("valor").style.backgroundColor = 'White';
    document.getElementById("endereco").style.backgroundColor = 'White';
    document.getElementById("referencia").style.backgroundColor = 'White';
    window.document.getElementById('valor').value = "0,00";
    window.document.getElementById("precotaxa").innerHTML = "Distância em KM: 0.000 - Entrega R$: 0.00.";

    document.getElementById("endereco").disabled = true;
    window.document.getElementById('referencia').disabled = true;
    window.document.getElementById('buscar-cep').disabled = false;
}

function gettelainicial() {
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

    document.getElementById("endereco").disabled = true;
    window.document.getElementById('referencia').disabled = true;
    window.document.getElementById('buscar-cep').disabled = false;
}

function listarEmpresa() {
    var client = JSON.parse(tbListaEmpresa);
    var respJson = JSON.parse(client);

    _idEmpresa = respJson.Id;
    _tempoestimado = respJson.Tempo_Estimado.replace("", "%20");
    _celularemitente = respJson.Telefone;
    _nomeEmpresaZ = respJson.Fantasia;
    _cidade = respJson.Cidade;
    _cep = respJson.Cep;
    _endereco = respJson.Endereco;
    _numero = respJson.Numero;
    _chavepix = respJson.Chave_Pix;

    var htmlformapagamntos = '<select class="form-control" id="tipopagamento" name="tipopagamento" data-placeholder="* Selecione Tipo de Pagamento">' +
                                          '<option value=""></option>' +
                                          '<option value="1">1-Dinheiro</option>' +
                                          '<option value="2">2-Crédito</option>' +
                                          '<option value="3">3-Débito</option>' +
                                          '<option value="4">4-PIX: ' + _chavepix + '</option>' +
                                          '</select>';

    window.document.getElementById("selectpagamento").innerHTML = htmlformapagamntos;
}

function getidempresa() {
    if (idpessoa == null) {
        idpessoa = sessionStorage.getItem('idpessoa');
    } else {
        idpessoa = _idEmpresa;
    }
}
