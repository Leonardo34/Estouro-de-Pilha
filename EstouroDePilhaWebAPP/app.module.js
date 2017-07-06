var app = angular.module("EstouroPilhaApp", ['ngRoute', 'auth']);

angular.module('EstouroPilhaApp').constant('authConfig', {

    // Obrigatória - URL da API que retorna o usuário
    urlUsuario: '...',

    // Obrigatória - URL da aplicação que possui o formulário de login
    urlLogin: '/login',

    // Opcional - URL da aplicação para onde será redirecionado (se for informado) após o LOGIN com sucesso
    urlPrivado: '/home',

    // Opcional - URL da aplicação para onde será redirecionado (se for informado) após o LOGOUT
    urlLogout: '/login'
});
