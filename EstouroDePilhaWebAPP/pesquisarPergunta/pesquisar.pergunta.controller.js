angular.module('EstouroPilhaApp').controller('pesquisarPerguntaController', function ($scope, authService, pesquisarPerguntaService){

  $scope.pesquisar = pesquisar;
  $scope.anterior = anterior;
  $scope.proxima = proxima;
  var paginaAtual = 0;
  var pergunta;

  function anterior(){
    if (paginaAtual == 0)
    {
      return
    }
    paginaAtual = paginaAtual -1;
    pesquisasPaginada ();
  }

  function proxima(){
    paginaAtual = paginaAtual +1;
    pesquisasPaginada ();
  }

  function pesquisar (perguntaPesquisada){
    paginaAtual = 0;
    pergunta = perguntaPesquisada;
    pesquisarPerguntaService.pesquisar(pergunta, paginaAtual).then(function (response){
      $scope.perguntasPesquisadas = response.data.result;
    })
  }

  function pesquisasPaginada (perguntaPesquisada){
    pesquisarPerguntaService.pesquisasPaginada(pergunta, paginaAtual).then(function (response){
      $scope.perguntasPesquisadas = response.data.result;
    })
  }

});
