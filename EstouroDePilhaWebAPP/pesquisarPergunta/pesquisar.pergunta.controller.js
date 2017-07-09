angular.module('EstouroPilhaApp').controller('pesquisarPerguntaController', function ($scope, authService, pesquisarPerguntaService){

  $scope.pesquisar = pesquisar;
  $scope.anterior = anterior;
  $scope.proxima = proxima;
  var paginaAtual = 0;
  var perguntaBuscada;

  function anterior(){
    if (paginaAtual == 0)
    {
      return;
    }
    paginaAtual = paginaAtual -1;
    pesquisarTrazerResultados(perguntaBuscada);
  }

  function proxima(){
    if ((10 * paginaAtual)/$scope.numeroDeResultadosDaPesquisa > 0) {
      return;
    }
    paginaAtual = paginaAtual +1;
    pesquisarTrazerResultados(perguntaBuscada);
  }

  function pesquisar (busca){
    paginaAtual = 0;
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
      paginaAtual, perguntaBuscada.conteudo, perguntaBuscada.tags).then(function (response){
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
