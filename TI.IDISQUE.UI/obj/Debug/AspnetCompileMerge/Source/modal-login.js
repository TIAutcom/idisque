const Login = {
	open() {
		// Abrir Modal
		// Adicionar a classe active ao Modal
		document.getElementById('modal-login').classList.add('active');
	},
	close() {
		// Fechar o Modal
		//Remover a classe active do Modal
		document.querySelector('.modal-login-overlay').classList.remove('active');
	},
};