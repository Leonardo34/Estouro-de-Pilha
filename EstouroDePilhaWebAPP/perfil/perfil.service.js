angular.module('EstouroPilhaApp').service("perfilService", perfilService);

function perfilService ($http){
    let url = "http://localhost:53986/api/usuarios/"

    var pegarUsuario = (id) => $http.get(`${url}/${id}`, id);

    var editarUsuario = (usuario) => $http.put(`${url}`, usuario);

    return {
        pegarUsuario: pegarUsuario,
        editarUsuario: editarUsuario
    }
};