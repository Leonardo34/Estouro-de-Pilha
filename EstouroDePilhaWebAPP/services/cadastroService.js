app.service('cadastroService', function($http) {

    this.cadastrarUsuario = function(RegistrarUsuarioModel) {
        return $http.post('http://localhost:50573/api/usuarios/registrar', RegistrarUsuarioModel);
    }

});
