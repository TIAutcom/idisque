var html = "";
var qtde = 0;
var statusEmpresa;

var tbListaVenda = localStorage.getItem("tbListaVenda");

if (tbListaVenda == null)
{
    localStorage.removeItem("tbListaVenda");
}
    tbListaVenda = [];

function AdicionarItemVenda(id, desc, preco, detalhes, url) {

    var client = JSON.parse(tbListaEmpresa);

    var z = getdadosempresa();

    if (client === null) {
        limpar();
        alert('App não Localizou endereço digital da empresa Selecionada!!!.Por Favor Inserir Produto Desejado novamente.');
 
    } else {
        if (statusEmpresa == 1) {

            var total = window.document.getElementById('total' + id).innerHTML;
            var qtde = window.document.getElementById('quant' + id);
            var obs = window.document.getElementById('obsDep' + id);

            let somaTotal = 0;

            var vendas = JSON.stringify({
                Id: id,
                Descricao: desc,
                Preco: preco,
                Total: total,
                Quantidade: qtde.value,
                Url: url,
                Observacao: obs.value,
                Detalhes: detalhes,
            });

            tbListaVenda.push(vendas);
            localStorage.setItem("tbListaVenda", JSON.stringify(tbListaVenda));

            alert(qtde.value + "X -" + desc + " Adicionado com Sucesso");

            listaqtdeitens();

            document.getElementById('products' + id).classList.remove('active');

            total = window.document.getElementById('total' + id).innerHTML = preco;
            qtde = window.document.getElementById('quant' + id).value = "1";
            obs = window.document.getElementById('obsDep' + id).value = "";

            return true;

        } else {
            alert('Ops!!! Estabelecimento Fechado');
          
        }
    }
}

function getdadosempresa() {
    var client = JSON.parse(tbListaEmpresa);

    if (client != null) {
        var respJson = JSON.parse(client);

        hora_entrada = respJson.Hora_Entada;
        hora_saida = respJson.Hora_Saida;
        variavelHora = getdatabrasilia();

        if (hora_entrada == null) {
            hora_entrada = variavelHora;
        }

        if (hora_entrada > variavelHora) {
            statusEmpresa = 0;
        }
        else {
            if (variavelHora < hora_saida) {
                statusEmpresa = 1;
            }
            else {
                statusEmpresa = 0;
            }
        }
    }

    return statusEmpresa;
}

function listaqtdeitens() {
    var tbListaVenda = localStorage.getItem("tbListaVenda");

    tbListaVenda = JSON.parse(tbListaVenda);
    qtde = tbListaVenda.length;

    if (qtde > 0) {
        html = '<i class="fa fa-cart-plus size car" aria-hidden="true" onclick="fecharpedido()"></i>' + '<p class="circle"><strong class="car-number">' + (qtde) + '</strong>';
    } else {
        html = '<i class="fa fa-cart-plus size car" aria-hidden="true"></i>' + '<p class="circle"><strong class="car-number">0</strong></p>';
    }

    window.document.getElementById('qtdemeucarrinho').innerHTML = html;
}

function statusfechado() {
    alert('Ops!!! Estabelecimento Fechado');
}

function getdatabrasilia() {
    let d = moment.tz('America/brasilia');
    var x = d.format();
    return x.toString().substring(11, 25).substring(0, 5).trim();
}

function limpar()
{
    localStorage.removeItem("tbListaVenda");
    location.reload();
}