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
    pesquisarTrazerResultados();
  }

  function proxima(){
    if ((10 * paginaAtual)/$scope.numeroDeResultadosDaPesquisa > 0)
    {
      return;
    }
    paginaAtual = paginaAtual +1;
    pesquisarTrazerResultados();
  }

  function pesquisar (perguntaPesquisada){
    paginaAtual = 0;
    pergunta = perguntaPesquisada;
    pesquisarTrazerResultados();
    numeroDeResultadosDaPesquisa ();
    $scope.mostrarPaginacao = false;
  }

  function pesquisarTrazerResultados() {
    pesquisarPerguntaService.pesquisarTrazerResultados(pergunta, paginaAtual).then(function (response){
      $scope.perguntasPesquisadas = response.data.result;
    })
  }

  function numeroDeResultadosDaPesquisa (){
    pesquisarPerguntaService.numeroDeResultadosDaPesquisa(pergunta).then(function (response){
      $scope.numeroDeResultadosDaPesquisa = response.data.result;
      if ($scope.numeroDeResultadosDaPesquisa > 10)
      {
        $scope.mostrarPaginacao = true;
      }
    })
  }
});
