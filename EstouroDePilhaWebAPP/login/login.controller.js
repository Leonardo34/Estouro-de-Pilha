angular.module('EstouroPilhaApp').controller('loginController', function($scope, $location, authService) {

  $scope.login = function() {
      authService.login($scope.usuario).then(response => {
          alert('Ta logado Tchê!');
      }, error => {
          console.log(error);
          alert('Desculpa tchê, mas alguma coisa ta errada!');
      })
      console.log($scope.usuario);
  }
});
