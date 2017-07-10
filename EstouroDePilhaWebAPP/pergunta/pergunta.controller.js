
angular.module('EstouroPilhaApp').controller('perguntaController', function ($scope, $routeParams, authService, perguntaService){
  var email;
  var idDaPergunta = $routeParams.id;
  
  $scope.proxima = proxima;
  $scope.anterior = anterior;
  $scope.usuarioLogado = usuarioLogado;
  $scope.marcarComoCorreta = marcarComoCorreta;
  $scope.pagina = 0;

  buscarPerguntaPorId(idDaPergunta);
  buscarRespostaPorIdDaPergunta(idDaPergunta);
  $scope.upvoteResposta = upvoteResposta;
  $scope.downvoteResposta = downvoteResposta;
  $scope.usuarioVotouEmResposta = usuarioVotouEmResposta;
  $scope.usuarioDeuUpvoteResposta = usuarioDeuUpvoteResposta;
  $scope.usuarioDeuDownvoteResposta = usuarioDeuDownvoteResposta;
  $scope.usuario = authService.getUsuario();
  buscarRespostaPorIdDaPergunta();

  function buscarPerguntaPorId(idDaPergunta){
    perguntaService.buscarPerguntaPorId(idDaPergunta).then(function (response) {
      $scope.pergunta = response.data.result;
      email =   $scope.pergunta.Usuario.Email;
    })
  }

  function buscarRespostaPorIdDaPergunta(idDaPergunta) {
    perguntaService.buscarRespostaPorIdDaPergunta(idDaPergunta).then(function (response) {
      $scope.respostas = response.data.result;
      buscarQuantidadeDeRespostasPorIdDaPergunta();
      usuarioLogado();
    })
  }

  function buscarQuantidadeDeRespostasPorIdDaPergunta(){
    perguntaService.buscarQuantidadeDeRespostasPorIdDaPergunta(idDaPergunta).then(function(response){
      $scope.totalDeRespostas = response.data.result;

    })
  }

  function usuarioLogado() {
    var jaFoiEscolhiaUmaRepostaCorreta =   $scope.respostas.filter(function (resposta) {return resposta.EhRespostaCorreta ==true}).length>0
    if (email === authService.getUsuario().Email && !jaFoiEscolhiaUmaRepostaCorreta)
    {
      return true
    }
  }

  function marcarComoCorreta(idDaResposta){
    perguntaService. marcarComoCorreta(idDaResposta).then(function (response){
      buscarRespostaPorIdDaPergunta(idDaPergunta)
    })
  };

  function anterior(){
    if ($scope.pagina == 0)
    {
      return;
    }
    $scope.pagina = $scope.pagina-1;
    buscarRespostaPorIdDaPergunta()
  }

  function proxima(){
    if ((5 * ($scope.pagina +1))/$scope.totalDeRespostas >1)
    {
      return;
    }
    $scope.pagina =$scope.pagina +1;
    buscarRespostaPorIdDaPergunta()
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