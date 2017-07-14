angular.module('EstouroPilhaApp').service("perfilService", perfilService);

function perfilService ($http, authService){
    let url = "http://localhost:53986/api/usuarios/"

    var pegarUsuario = (id) => $http.get(`${url}${id}`, id);

    var editarUsuario = (usuario) => $http.put(`${url}`, usuario);

    var idUsuarioAtivo = 0;

    var pegarIdUsuarioAtivo = () => {
      if(authService.isAutenticado()) {
        idUsuarioAtivo = authService.getUsuario().Id;
        return idUsuarioAtivo;
      }
    };



    return {
        pegarIdUsuarioAtivo: pegarIdUsuarioAtivo,
        pegarUsuario: pegarUsuario,
        editarUsuario: editarUsuario
    }
};
