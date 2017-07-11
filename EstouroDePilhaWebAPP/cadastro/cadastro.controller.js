angular.module('EstouroPilhaApp').controller('cadastroController',
 function($scope, $routeParams, $location, cadastroService, authService) {
  $scope.logout = authService.logout;

  $scope.cadastrarUsuario = function(novoUsuario) {
    cadastroService.cadastrarUsuario(novoUsuario)
      .then(response => {
        var usuario = {
          "email": novoUsuario.Email,
          "senha": novoUsuario.Senha
        }
        new Noty({
            type: 'success',
            timeout: 2000,
            text: 'Usuario cadastrado!'         
        }).show();
        authService.login(usuario)
          .then(response => {
             $location.path('/home');
          })
      }, fail => {
        fail.data.errors.forEach(erro => {
          new Noty({
              type: 'error',
              timeout: 2000,
              text: erro         
          }).show();
        })
      })
  }
});
