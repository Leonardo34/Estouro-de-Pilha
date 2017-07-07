angular.module('EstouroPilhaApp').service('cadastroService', function ($http) {

  this.cadastrarUsuario = function(usuario) {
    return $http.post('http://localhost:53986/api/usuarios/registrar', usuario);
  }

})
