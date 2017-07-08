angular.module('EstouroPilhaApp').service("perfilService", perfilService);

function perfilService ($http){
    let url = "http://localhost:53986/api/usuarios/"

    var pegarUsuario = (id) => $http.get(`${url}/${id}`, id);

    return {
        pegarUsuario: pegarUsuario
    }
};