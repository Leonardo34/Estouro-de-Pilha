app.controller('cadastroController', function($scope, $location, authService) {
  $scope.url.path = $location.path();

  $scope.cadastrarUsuario = function() {
       cadastroService.salvarUsuario($scope.usuario).then(response => {
           window.alert('foi');
           delete $scope.usuario;
       })
   };
});
