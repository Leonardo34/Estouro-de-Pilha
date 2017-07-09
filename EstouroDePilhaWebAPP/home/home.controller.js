angular.module('EstouroPilhaApp').controller('homeController', function ($scope, authService, perguntaService){
  $scope.pagina = 0;
  $scope.itensPagina = 5;
  carregarPerguntas();

  function carregarPerguntas() {
    let skip = $scope.pagina * $scope.itensPagina;
    perguntaService.buscarPerguntasPaginadas(skip, $scope.itensPagina).then(res => {
      $scope.perguntas = res.data.result;
      console.log($scope.perguntas);
      $scope.pagina += 1;
    })
  }
});
