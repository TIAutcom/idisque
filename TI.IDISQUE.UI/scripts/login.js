var idpessoa;

var tbLista = localStorage.getItem("tbListaUsuario");

// Converte string para objeto
tbLista = JSON.parse(tbLista);

function postidempresa() {
    idpessoa = window.location.search.replace("?", "").replace("id", "").replace("=", "");

    sessionStorage.setItem('idpessoa', idpessoa);
}


// Caso não haja conteúdo, iniciamos um vetor vazio
if (tbLista == null) {
    tbLista = [];
} else {
    window.location.href = "index.aspx";
}

function Adicionar() {
    var nome = window.document.getElementById("nome").value;
    var celular = window.document.getElementById("telefone").value;

    if (nome === "") {
        alert("Campo Nome do Usuário é Obrigatório");
    } else if (celular === "") {
        alert("Campo Celular do Usuário é Obrigatório");
    } else if (celular.replace(" ", "").replace("(", "").replace(")", "").replace("-", "").trim().length < 11) {
        alert("Campo Numero do Celular é Incorreto.");
        var celular = window.document.getElementById("telefone").focus();
    }
    else {
        var usuario = JSON.stringify({
            Nome: nome,
            Celular: celular
        });

        tbLista.push(usuario);
        localStorage.setItem("tbListaUsuario", JSON.stringify(tbLista));

        var nome = window.document.getElementById("nome").value = "";
        var celular = window.document.getElementById("telefone").value = "";

        if (tbLista == null) {
            return false;
        } else {
            localStorage.removeItem("tbListaVenda");
            localStorage.removeItem("tbListaEmpresa");

            location.reload();

            idpessoa = window.location.search.replace("?", "").replace("id", "").replace("=", "");
            sessionStorage.setItem('idpessoa', idpessoa);

            window.location.href = "index.aspx?id=" + idpessoa;
            return true;
        }
    }
}

function ClearLocalStorage() {
    localStorage.removeItem("tbListaUsuario");
    window.location.href = "login.aspx";
}

function mask(o, f) {
    setTimeout(function () {
        var v = mphone(o.value);
        if (v != o.value) {
            o.value = v;
        }
    }, 1);
}

function mphone(v) {
    var r = v.replace(/\D/g, "");
    r = r.replace(/^0/, "");
    if (r.length > 10) {
        r = r.replace(/^(\d\d)(\d{5})(\d{4}).*/, "($1) $2-$3");
    } else if (r.length > 5) {
        r = r.replace(/^(\d\d)(\d{4})(\d{0,4}).*/, "($1) $2-$3");
    } else if (r.length > 2) {
        r = r.replace(/^(\d\d)(\d{0,5})/, "($1) $2");
    } else {
        r = r.replace(/^(\d*)/, "($1");
    }
    return r;
}