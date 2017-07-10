angular.module('EstouroPilhaApp').controller('perguntaController', function ($scope, $routeParams, authService, perguntaService) {

  var idDaPergunta = $routeParams.id;

  buscarPerguntaPorId(idDaPergunta);
  buscarRespostaPorIdDaPergunta(idDaPergunta);
  $scope.upvoteResposta = upvoteResposta;
  $scope.downvoteResposta = downvoteResposta;

  function buscarPerguntaPorId(idDaPergunta){
    perguntaService.buscarPerguntaPorId(idDaPergunta).then(function (response) {
      $scope.pergunta = response.data.result;
    })
  }

  function buscarRespostaPorIdDaPergunta(idDaPergunta) {
    perguntaService.buscarRespostaPorIdDaPergunta(idDaPergunta).then(function (response) {
      $scope.respostas = response.data.result;
    })
  }

  function upvoteResposta(resposta) {
    perguntaService.upvoteResposta(resposta.Id);
    resposta.QuantidadeUpVotes += 1;
  }

  function downvoteResposta(resposta) {
    perguntaService.downvoteResposta(resposta.Id);
    resposta.QuantidadeDownVotes += 1;
  }
});
