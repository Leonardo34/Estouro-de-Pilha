angular.module('EstouroPilhaApp').controller('perguntaController', function ($scope, $routeParams, authService, perguntaService){

  var idDaPergunta = $routeParams.id;

  buscarPerguntaPorId(idDaPergunta);
  buscarRespostaPorIdDaPergunta(idDaPergunta);

  function buscarPerguntaPorId(idDaPergunta){
    perguntaService.buscarPerguntaPorId(idDaPergunta).then(function (response){
      $scope.pergunta = response.data.result;
    })
  }

  function buscarRespostaPorIdDaPergunta(idDaPergunta){
    perguntaService.buscarRespostaPorIdDaPergunta(idDaPergunta).then(function (response){
      $scope.respostas = response.data.result;
    })
  }
});
