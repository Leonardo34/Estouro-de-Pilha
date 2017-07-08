angular.module('EstouroPilhaApp').controller('pesquisarPerguntaController', function ($scope, authService, pesquisarPerguntaService){

  $scope.pesquisar = pesquisar;

  function pesquisar (perguntaPesquisada){
    pesquisarPerguntaService.pesquisar(perguntaPesquisada).then(function (response){
        console.log(response.data);
      $scope.perguntasPesquisadas = response.data.result;

    })
  }

});
