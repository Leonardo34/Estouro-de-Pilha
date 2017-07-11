angular.module('EstouroPilhaApp').controller('homeController', function ($scope, authService, perguntaService){
  $scope.pagina = 0;
  $scope.itensPagina = 5;
  $scope.logout = authService.logout;
  carregarPerguntas();
  carregarTotalPerguntas();
  $scope.estaLogado = authService.isAutenticado();
  $scope.proximaPagina = function() {
    $scope.pagina += 1;
    carregarPerguntas();
  }

  $scope.paginaAnterior = function() {
    $scope.pagina -= 1;
    carregarPerguntas();
  }

  function carregarPerguntas() {
    let skip = $scope.pagina * $scope.itensPagina;
    perguntaService.buscarPerguntasPaginadas(skip, $scope.itensPagina).then(res => {
      $scope.perguntas = res.data.result;
    })
  }

  function carregarTotalPerguntas() {
    perguntaService.buscarTotalPerguntasCadastradas().then(res => {
      $scope.totalPerguntas = res.data.result;
    })
  }
});
