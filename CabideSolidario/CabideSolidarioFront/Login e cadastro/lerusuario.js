const express = require('express');
const cors = require('cors');
const bodyParser = require('body-parser');

const app = express();
app.use(cors());
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));

let usuarios = [];

app.post('/cadastro', (req, res) => {
    const novoUsuario = {
        username: req.body.newUsername,
        email: req.body.newEmail,
        password: req.body.newPassword,
        endereco: {
            cep: req.body.cep,
            cidade: req.body.cidade,
            bairro: req.body.bairro,
            rua: req.body.rua,
            numero: req.body.numero,
            complemento: req.body.complemento
        },
        telefone: req.body.telefone,
        tipoCadastro: req.body.tipoCadastro,
        cpf: req.body.tipoCadastro === 'doador' ? req.body.cpf : null,
        cnpj: req.body.tipoCadastro === 'instituicao' ? req.body.cnpj : null
    };
    usuarios.push(novoUsuario);
    res.json({ message: 'Cadastro realizado com sucesso!' });
});

const PORT = 3000;
app.listen(PORT, () => {
    console.log(`Servidor rodando na porta ${PORT}`);
});
