app.controller('loginController', function($scope, $location, authService) {
  $scope.url.path = $location.path();

  $scope.login = function() {
      authService.login($scope.usuario).then(response => {
          console.log(response);
          alert('Ta logado Tchê!');
      }, error => {
          console.log(error);
          alert('Desculpa tchê, mas alguma coisa ta errada!');
      })
  }
});
