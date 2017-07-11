angular.module('EstouroPilhaApp').controller('loginController', function($scope, $location, authService) {

  $scope.login = function() {
      authService.login($scope.usuario).then(response => {
          new Noty({
              type: 'success',
              timeout: 2000,
              text: 'Login efetuado com sucesso!'
          }).show();
      }, fail => {
          new Noty({
                type: 'error',
                timeout: 2000,
                text: 'Usuário ou senha errado, Tchê!'                
            }).show();          
      })
  } 
});
