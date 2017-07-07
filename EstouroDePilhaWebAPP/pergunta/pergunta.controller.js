angular.module('EstouroPilhaApp').controller('perguntaController', function ($scope, authService, perguntaService){

  buscarPerguntaPorId();
  buscarRespostaPorIdDaPergunta();
  function buscarPerguntaPorId(id){
    perguntaService.buscarPerguntaPorId(id).then(function (response){

      $scope.pergunta = response.data.result;
      console.log($scope.pergunta);
    })
  }

  function buscarRespostaPorIdDaPergunta(id){
    perguntaService.buscarRespostaPorIdDaPergunta(id).then(function (response){

      $scope.respostas = response.data.result;
          console.log($scope.respostas);
    })
  }

});
