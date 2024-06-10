function login() {
    const username = document.getElementsByName('username')[0].value;
    const password = document.getElementsByName('password')[0].value;
    console.log("Usuário:", username);
    console.log("Senha:", password);
    // Aqui você pode adicionar lógica para fazer login, como enviar os dados para o servidor
}

function cadastro() {
    const newUsername = document.getElementsByName('newUsername')[0].value;
    const newPassword = document.getElementsByName('newPassword')[0].value;
    const confirmNewPassword = document.getElementsByName('confirm_newPassword')[0].value;
    console.log("Novo Usuário:", newUsername);
    console.log("Nova Senha:", newPassword);
    console.log("Confirmar Nova Senha:", confirmNewPassword);
    // Aqui você pode adicionar lógica para cadastro, como enviar os dados para o servidor
}