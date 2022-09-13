var idpessoa = 0;

pesquisaridpesso();

function pesquisaridpesso() {

    //idpessoa = window.location.search.replace("?", "");

    //alert(idpesso);

    var html = '<p>A página que você está procurando pode ter sido removida devido a mudança de nome ou está temporariamente indisponível. <a href="index.html">Voltar à página inicial</a></p>';

    window.document.getElementById('error').innerHTML = html;
}