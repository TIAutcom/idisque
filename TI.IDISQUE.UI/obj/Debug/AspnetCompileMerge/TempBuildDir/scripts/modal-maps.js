const Maps = {
	open() {
		// Abrir Modal
		// Adicionar a classe active ao Modal
		document.getElementById('maps').classList.add('active');
	},
	close() {
		// Fechar o Modal
		//Remover a classe active do Modal
		document.querySelector('.modal-maps').classList.remove('active');
	},
};