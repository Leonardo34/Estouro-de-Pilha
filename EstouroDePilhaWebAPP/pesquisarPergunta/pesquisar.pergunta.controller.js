angular.module('EstouroPilhaApp').controller('pesquisarPerguntaController', function ($scope, authService, pesquisarPerguntaService){

  $scope.pesquisar = pesquisar;
  $scope.anterior = anterior;
  $scope.proxima = proxima;
  var paginaAtual = 0;
  var pergunta;

  function anterior(){
    if (paginaAtual == 0)
    {
      return;
    }
    paginaAtual = paginaAtual -1;
    pesquisasPaginada ();
  }

  function proxima(){
    if ((10 * paginaAtual)/$scope.numeroDeResultadosDaPesquisa > 0)
    {
      return;
    }
    paginaAtual = paginaAtual +1;
    pesquisasPaginada ();
  }

  function pesquisar (perguntaPesquisada){
    paginaAtual = 0;
    pergunta = perguntaPesquisada;
    numeroDeResultadosDaPesquisa (pergunta);
    pesquisarPerguntaService.pesquisar(pergunta, paginaAtual).then(function (response){
      $scope.perguntasPesquisadas = response.data.result;
    })
  }

  function pesquisasPaginada (perguntaPesquisada){
    pesquisarPerguntaService.pesquisasPaginada(pergunta, paginaAtual).then(function (response){
      $scope.perguntasPesquisadas = response.data.result;
    })
  }

  function numeroDeResultadosDaPesquisa (pergunta){
    pesquisarPerguntaService.numeroDeResultadosDaPesquisa(pergunta).then(function (response){
      $scope.numeroDeResultadosDaPesquisa = response.data.result;
      if ($scope.numeroDeResultadosDaPesquisa > 10)
      {
        $scope.mostrarPaginacao = true;
      }
      else
      {
        $scope.mostrarPaginacao = false;
      }
    })
  }
});
