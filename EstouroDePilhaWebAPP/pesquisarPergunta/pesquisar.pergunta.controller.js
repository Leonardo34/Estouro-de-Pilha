angular.module('EstouroPilhaApp').controller('pesquisarPerguntaController', function ($scope, authService, pesquisarPerguntaService){

  $scope.pesquisar = pesquisar;

  function pesquisar (perguntaPesquisada){
    pesquisarPerguntaService.pesquisar(perguntaPesquisada).then(function (response){
      $scope.perguntasPesquisadas = response.data.result;
    })
  }

  function paginacao (pagina){
    pesquisarPerguntaService.paginacao(pagina).then(function (response)

  )}
  
});
