var variavelHora;
var hora_entrada;
var hora_saida;
var nome_Empresa;
var url_imagem;
var chave_pix;
var seguimento;
var telefone;
var taxafixa;
var endereco;
var numero;
var cep;
var cidade;
var html;
var status;
var tempostimado
var htmlstatus;
var idpessoa;

var tbLista = localStorage.getItem("tbListaUsuario");
var tbListaEmpresa = localStorage.getItem("tbListaEmpresa");

// Converte string para objeto
tbLista = JSON.parse(tbLista);

getdadosEmpresa();

postidempresa();

// Caso não haja conteúdo, iniciamos um vetor vazio
if (tbLista == null) {
    tbLista = [];
    window.location.href = "login.aspx?id=" + idpessoa;
}

function logaut() {
    localStorage.removeItem("tbListaUsuario");
    window.location.href = "login.aspx?id=" + idpessoa;

    var nome = window.document.getElementById("nome").focus();
}

function getdadosEmpresa() {

    //if (tbListaEmpresa == null) {

    $.ajax({
        type: "POST",
        url: "index.aspx/GetDadosEmpresa",
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "text",
        success: function (response) {

            var tbListaEmpresa = localStorage.getItem("tbListaEmpresa");

            var responses = JSON.parse(response).d;
            var respJson = JSON.parse(responses);

            tbListaEmpresa = [];

            idpessoa = respJson[0].id,
            hora_entrada = respJson[0].horaabertura;
            hora_saida = respJson[0].horafechamento;
            nome_Empresa = respJson[0].fantasia;
            url_imagem = respJson[0].urlimagem;
            chave_pix = respJson[0].chavePix;
            seguimento = respJson[0].seguimento;
            telefone = respJson[0].telefone;
            taxafixa = respJson[0].taxafixa;
            endereco = respJson[0].endereco;
            numero = respJson[0].numero;
            cep = respJson[0].cep;
            cidade = respJson[0].cidade;
            tempostimado = respJson[0].tempopreparo;

            var empresa = JSON.stringify({
                Id: idpessoa,
                Fantasia: nome_Empresa,
                Hora_Entada: hora_entrada,
                Hora_Saida: hora_saida,
                Urlimagem: url_imagem,
                Chave_Pix: chave_pix,
                Seguimento: seguimento,
                Telefone: telefone,
                TaxaFixa: taxafixa,
                Endereco: endereco,
                Numero: numero,
                Cep: cep,
                Cidade: cidade,
                Tempo_Estimado: tempostimado
            });

            tbListaEmpresa.push(empresa);
            localStorage.setItem("tbListaEmpresa", JSON.stringify(tbListaEmpresa));

            printstatus(hora_entrada, hora_saida, url_imagem, nome_Empresa);
        },
        error: function () {
            alert("Falha ao Carregar dados da Base de Dados");
        }
    });
}

function getdatabrasilia() {
    let d = moment.tz('America/brasilia');
    var x = d.format();
    return x.toString().substring(11, 25).substring(0, 5).trim();
}

function printstatus(hora_entrada, hora_saida, url_imagem, nome_Empresa) {
    var tbListaVenda = localStorage.getItem("tbListaVenda");

    tbListaVenda = JSON.parse(tbListaVenda);

    if (tbListaVenda == null) {
        qtde = 0;
    } else {
        qtde = tbListaVenda.length;
    }

    variavelHora = getdatabrasilia();

    if (hora_entrada > variavelHora) {
        status = 0;
        html = '<a onclick="limpar()"><img class="logo" src="' + url_imagem + '"/></a><div class="info"><h2 class="name-logo" id="name-logo">' + nome_Empresa + '</h2><button style="color: red; background - color: red; border: 3px solid red" class="flex button button2"><p>Ops!!! Estabelecimento Fechado.</p><i class="fa fa-clock-o left-timer" aria-hidden="true"></i></button></div>';
        htmlstatus = '<i class="fa fa-cart-plus size car" aria-hidden="true" onclick="statusfechado()"></i>' + '<p class="circle"><strong class="car-number">0</strong></p>';
    }
    else {
        if (variavelHora < hora_saida) {
            status = 1;
            html = html = '<a onclick="limpar()"><img class="logo" src="' + url_imagem + '"/></a><div class="info"><h2 class="name-logo" id="name-logo">' + nome_Empresa + '</h2><button style="color: grenn; background-color: grenn; border: 3px solid grenn" class="flex button button1"><p>Estabelecimento Aberto até ' + hora_saida + '.</p><i class="fa fa-clock-o left-timer" aria-hidden="true"></i></button></div>';

            if (qtde > 0) {
                htmlstatus = '<i class="fa fa-cart-plus size car" aria-hidden="true" onclick="fecharpedido()"></i>' + '<p class="circle"><strong class="car-number">' + (qtde) + '</strong>';
            } else {
                htmlstatus = '<i class="fa fa-cart-plus size car" aria-hidden="true"></i>' + '<p class="circle"><strong class="car-number">0</strong>';
            }
        }
        else {
            status = 0;
            html = '<a onclick="limpar()"><img class="logo" src="' + url_imagem + '"/></a><div class="info"><h2 class="name-logo" id="name-logo">' + nome_Empresa + '</h2><button style="color: red; background - color: red; border: 3px solid red" class="flex button button2"><p>Ops!!! Estabelecimento Fechado.</p><i class="fa fa-clock-o left-timer" aria-hidden="true"></i></button></div>';
            htmlstatus = '<i class="fa fa-cart-plus size car" aria-hidden="true" onclick="statusfechado()"></i>' + '<p class="circle"><strong class="car-number">' + (qtde) + '</strong></p>';
        }
    }

    window.document.getElementById('idEstabelecimento').innerHTML = html;
    window.document.getElementById('qtdemeucarrinho').innerHTML = htmlstatus;
}

function statusfechado() {
    alert('Ops!!! Estabelecimento Fechado');
}

function postidempresa() {
    idpessoa = window.location.search.replace("?", "").replace("id", "").replace("=", "");

    sessionStorage.setItem('idpessoa', idpessoa);
}
