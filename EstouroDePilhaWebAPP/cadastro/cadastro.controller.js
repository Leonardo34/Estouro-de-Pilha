angular.module('EstouroPilhaApp').controller('cadastroController',
                      function($scope, $routeParams, $location, cadastroService, authService) {

  $scope.cadastrarUsuario = function(novoUsuario) {
    cadastroService.cadastrarUsuario(novoUsuario)
      .then(response => {
        alert('Cadastrado com sucesso, agora te loga, Tchê')
      }, error => {
        alert('Alguma coisa deu errada, tenta de novo, Tchê!');
      })
  }
});
