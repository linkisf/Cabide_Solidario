document.addEventListener('DOMContentLoaded', function () {
    const doadorRadio = document.getElementById('doador');
    const instituicaoRadio = document.getElementById('instituicao');
    const cpfField = document.getElementById('cpfField');
    const cnpjField = document.getElementById('cnpjField');

    doadorRadio.addEventListener('change', function () {
        if (this.checked) {
            cpfField.style.display = 'block';
            cnpjField.style.display = 'none';
        }
    });

    instituicaoRadio.addEventListener('change', function () {
        if (this.checked) {
            cpfField.style.display = 'none';
            cnpjField.style.display = 'block';
        }
    });
});


function processarCadastro(event) {
    event.preventDefault();


    let formData = [
        {
            "Nome": document.getElementById("idNovoUsuario").value,
            "Email": document.getElementById("idEmail").value,
            "Password": document.getElementById("idSenha").value,
            "Endereco": {
                "Cep": document.getElementById("idCep").value,
                "Cidade": document.getElementById("idCidade").value,
                "Bairro": document.getElementById("idBairro").value,
                "Rua": document.getElementById("idRua").value,
                "Numero": document.getElementById("idNumero").value,
                "Complemento": document.getElementById("idComplemento").value,
                "Ativo": true
            },
            "CPF": document.getElementById("idCpf").value,
            "Telefone": document.getElementById("idTelefone").value
        }
    ];


    console.log(JSON.stringify(formData))

    var request = new Request('https://localhost:7015/doador')
    request.body = formData;

    //const token = '';

    fetch('https://localhost:7015/doador', {
        method: 'POST',        
        headers: {
            'Access-Control-Allow-Origin': '*',
            // 'Content-type': 'application/json',
            // 'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify(formData)
    }).then(response => response.json())
    .then(data => {
        alert("Cadastro realizado com sucesso!");
        window.location.href = 'login.html';
    })
    .catch(error => {
        console.error('Erro ao realizar cadastro:', error);
        alert("Erro ao realizar cadastro. Por favor, tente novamente mais tarde.");
    });

}