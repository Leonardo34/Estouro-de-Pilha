angular.module('EstouroPilhaApp').controller('perguntaController', function ($scope, $routeParams, authService, perguntaService) {

  var idDaPergunta = $routeParams.id;

  buscarPerguntaPorId(idDaPergunta);
  buscarRespostaPorIdDaPergunta(idDaPergunta);
  $scope.upvoteResposta = upvoteResposta;
  $scope.downvoteResposta = downvoteResposta;
  $scope.usuarioVotouEmResposta = usuarioVotouEmResposta;
  $scope.usuarioDeuUpvoteResposta = usuarioDeuUpvoteResposta;
  $scope.usuarioDeuDownvoteResposta = usuarioDeuDownvoteResposta;
  $scope.usuario = authService.getUsuario();

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
    resposta.UpVotes.push($scope.usuario);
  }

  function downvoteResposta(resposta) {
    perguntaService.downvoteResposta(resposta.Id);
    resposta.QuantidadeDownVotes += 1;
    resposta.DownVotes.push($scope.usuario);
  }

  function usuarioVotouEmResposta(resposta) {
    return usuarioDeuDownvoteResposta(resposta) || usuarioDeuUpvoteResposta(resposta);
  }

  function usuarioDeuDownvoteResposta(resposta) {
    return resposta.DownVotes.some(u => u.Id == authService.getUsuario().Id);
  }

  function usuarioDeuUpvoteResposta(resposta) {
    return resposta.UpVotes.some(u => u.Id == authService.getUsuario().Id);
  }
});