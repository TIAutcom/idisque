const Login = {
	open() {
		// Abrir Modal
		// Adicionar a classe active ao Modal
		document.getElementById('login-ls').classList.add('active');
	},
	close() {
		// Fechar o Modal
		//Remover a classe active do Modal
		document.querySelector('.modal-login').classList.remove('active');
	},
};