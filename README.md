# Market
	Sistema WEB que faz todo gerenciamento de um restaurante, tais como: controle de estoque, venda balcão e web, gerenciamento de entregas, controle de pedidos e clientes. Desenvolvido em C#, utilizando MVC core 2.0, banco dados SQL Server, JavaScript e Bootstrap.
		
	Para execução do sistema é necessário 1º criar o banco,
	-> Add-Migration InitialCreate
	-> Update-Database
	
	-Remover [Authorize] do controller (Administrativa) para criar primeiro acesso.
	-Ao rodar o sistema, ir até usuários e criar um novo.
	
	-Senha do usuário por padrão é o próprio CPF
