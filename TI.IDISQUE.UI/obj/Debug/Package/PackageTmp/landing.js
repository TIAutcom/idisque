const Landing = {
	open() {
		// Abrir Modal
		// Adicionar a classe active ao Modal
		document.getElementById('modal-ls').classList.add('active');
	},
	close() {
		// Fechar o Modal
		//Remover a classe active do Modal
		document.querySelector('.modal-overlay').classList.remove('active');
	},
};

document.addEventListener("DOMContentLoaded", function (event) {
  Landing.open();
  setTimeout(() => {
    Landing.close();
  }, 0)
});