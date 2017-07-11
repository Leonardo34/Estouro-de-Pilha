angular.module('EstouroPilhaApp').controller('pesquisarPerguntaController', function ($scope, authService, pesquisarPerguntaService){
  $scope.logout = authService.logout;
  $scope.pesquisar = pesquisar;
  $scope.anterior = anterior;
  $scope.proxima = proxima;
  $scope.estaLogado = authService.isAutenticado();
  $scope.pagina = 0;
  var perguntaBuscada;

  function anterior(){
    if ($scope.pagina == 0)
    {
      return;
    }
    $scope.pagina = $scope.pagina -1;
    pesquisarTrazerResultados(perguntaBuscada);
  }

  function proxima(){
    if ((10 * ($scope.pagina +1))/$scope.numeroDeResultadosDaPesquisa >= 1) {
      return;
    }
    $scope.pagina = $scope.pagina +1;
    pesquisarTrazerResultados(perguntaBuscada);
  }

  function pesquisar (busca){
    $scope.pagina = 0;
    perguntaBuscada = busca;
    if (typeof perguntaBuscada === 'undefined')
    {
       return;
    }
    if (perguntaBuscada.tags === '')
    {
       perguntaBuscada.tags = undefined;
    }
    if (perguntaBuscada.conteudo === '')
    {
       perguntaBuscada.conteudo = undefined;
    }
    pesquisarTrazerResultados(perguntaBuscada);
    numeroDeResultadosDaPesquisa (perguntaBuscada);
    $scope.mostrarPaginacao = false;
  }

  function pesquisarTrazerResultados(perguntaBuscada) {
    pesquisarPerguntaService.pesquisarTrazerResultados(
      $scope.pagina, perguntaBuscada.conteudo, perguntaBuscada.tags).then(function (response){
        $scope.perguntasPesquisadas = response.data.result;
        $scope.busca =undefined;
    })
  }

  function numeroDeResultadosDaPesquisa (perguntaBuscada){
    pesquisarPerguntaService.numeroDeResultadosDaPesquisa(perguntaBuscada.conteudo, perguntaBuscada.tags).then(function (response){
      $scope.numeroDeResultadosDaPesquisa = response.data.result;
      if ($scope.numeroDeResultadosDaPesquisa > 10)
       {
          $scope.mostrarPaginacao = true;
       }
    })
  }
});
