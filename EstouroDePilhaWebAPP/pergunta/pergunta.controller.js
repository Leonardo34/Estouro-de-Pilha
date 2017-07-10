
angular.module('EstouroPilhaApp').controller('perguntaController', 
    function ($scope, $routeParams, authService, perguntaService, tagService) {
  var email;
  var idDaPergunta = $routeParams.id;
  var data;
  var temRespostaCorreta;
  $scope.proxima = proxima;
  $scope.anterior = anterior;
  $scope.usuarioQueFezAPerguntaNaoMarcouNenhumaRespostaComoCorreta = usuarioQueFezAPerguntaNaoMarcouNenhumaRespostaComoCorreta;
  $scope.marcarComoCorreta = marcarComoCorreta;
  $scope.pagina = 0;
  buscarQuantidadeDeRespostasPorIdDaPergunta();
  buscarPerguntaPorId(idDaPergunta);
  buscarRespostaPorIdDaPergunta(idDaPergunta);

  $scope.upvoteResposta = upvoteResposta;
  $scope.downvoteResposta = downvoteResposta;
  $scope.usuarioVotouEmResposta = usuarioVotouEmResposta;
  $scope.usuarioDeuUpvoteResposta = usuarioDeuUpvoteResposta;
  $scope.usuarioDeuDownvoteResposta = usuarioDeuDownvoteResposta;
  $scope.usuario = authService.getUsuario();
  buscarRespostaPorIdDaPergunta();
  $scope.editarPergunta = editarPergunta;

  function buscarPerguntaPorId() {
    perguntaService.buscarPerguntaPorId(idDaPergunta).then(function (response) {
      $scope.pergunta = response.data.result;
      data  = $scope.pergunta.DataPergunta;
      email =   $scope.pergunta.Usuario.Email;
    })
  }

  function editarPergunta(){
    if ((Date.parse(new Date()) - Date.parse(data))/(1000*3600*24)>=7 && email === authService.getUsuario().Email){
      return true;
    }
  }

  function buscarRespostaPorIdDaPergunta() {
    perguntaService.buscarRespostaPorIdDaPergunta($scope.pagina, idDaPergunta).then(function (response) {
      $scope.respostas = response.data.result;
    })
  }

  function buscarQuantidadeDeRespostasPorIdDaPergunta() {
    perguntaService. buscarQuantidadeDeRespostasPorIdDaPergunta(idDaPergunta).then(function(response){
      $scope.totalDeRespostas = response.data.dados;
      temRespostaCorreta = response.data.outrosDados;
    })
  }

  function usuarioLogado() {
      if (authService.getUsuario() === undefined){
        return false
      }
      return (authService.getUsuario().Email == email)
    }

    function usuarioQueFezAPerguntaNaoMarcouNenhumaRespostaComoCorreta(){
        return  (usuarioLogado() &&  !temRespostaCorreta);
    }

   function marcarComoCorreta(idDaResposta){
     perguntaService. marcarComoCorreta(idDaResposta).then(function (response){
      buscarQuantidadeDeRespostasPorIdDaPergunta()
      usuarioQueFezAPerguntaNaoMarcouNenhumaRespostaComoCorreta()
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
