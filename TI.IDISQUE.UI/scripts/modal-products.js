const Products = {
    open() {
        // Abrir Modal
        // Adicionar a classe active ao Modal
        document.getElementById('').classList.add('active');
    },
    close() {
        // Fechar o Modal
        //Remover a classe active do Modal
        document.getElementById('').classList.remove('active');
    }
};

var value = document.getElementById("quant").value = 1;

function processar(quant) {

    var qtdes = parseInt(document.getElementById("quant").value);
    var preco = parseFloat(document.getElementById("preco").innerHTML.replace("$", ""));

    qtdes += quant;

    if (value < 1) {
        document.getElementById("quant").value = 1;
    } else {
        document.getElementById("quant").value = qtdes;
    }

    var qtde = parseFloat(qtdes);
    var pco = parseFloat(preco);
    let total = (qtde * pco);

    window.document.getElementById("total").innerHTML = "Adicionar ao Carrinho R$ " + total.toFixed(2).toString().replace(".", ",").trim();
}