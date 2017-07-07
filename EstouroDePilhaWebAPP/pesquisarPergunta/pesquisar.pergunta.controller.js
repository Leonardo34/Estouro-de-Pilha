app.controller('pesquisarPerguntaController', function ($scope, authService, pesquisarPerguntaService){

  $scope.pesquisar = pesquisar;

  function pesquisar (perguntaPesquisada){
    pesquisarPerguntaService.pesquisar(perguntaPesquisada).then(function (response){
      $scope.perguntasPesquisadas = response;
    })
  }
  
});
