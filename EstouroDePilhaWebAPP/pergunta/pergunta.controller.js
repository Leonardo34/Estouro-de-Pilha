
angular.module('EstouroPilhaApp').controller('perguntaController',
    function ($scope, $routeParams, authService, perguntaService, tagService) {
  var email;
  var idDaPergunta = $routeParams.id;
  var data;
  var copiaPergunta; //utilizado para voltar ao estado original caso edição seja cancelada
  var temRespostaCorreta;
  var edicaoAberta = false;
  $scope.logout = authService.logout;
  $scope.adicionarMarkdown = adicionarMarkdown;
  $scope.proxima = proxima;
  $scope.anterior = anterior;
  $scope.usuarioQueFezAPerguntaNaoMarcouNenhumaRespostaComoCorreta = usuarioQueFezAPerguntaNaoMarcouNenhumaRespostaComoCorreta;
  $scope.marcarComoCorreta = marcarComoCorreta;
  $scope.pagina = 0;  
  buscarQuantidadeDeRespostasPorIdDaPergunta();
  buscarPerguntaPorId(idDaPergunta);
  buscarRespostaPorIdDaPergunta(idDaPergunta);
  $scope.editarPergunta = editarPergunta;
  $scope.upvoteResposta = upvoteResposta;
  $scope.downvoteResposta = downvoteResposta;
  $scope.usuarioVotouEmResposta = usuarioVotouEmResposta;
  $scope.usuarioDeuUpvoteResposta = usuarioDeuUpvoteResposta;
  $scope.usuarioDeuDownvoteResposta = usuarioDeuDownvoteResposta;
  $scope.usuario = authService.getUsuario();
  $scope.estaLogado = authService.isAutenticado();
  buscarRespostaPorIdDaPergunta();
  $scope.podeEditarPergunta = podeEditarPergunta;
  $scope.abrirFecharModal = abrirFecharModal;
  $scope.cancelarEdicao = cancelarEdicao;
  $scope.responderPergunta = responderPergunta;
  function buscarPerguntaPorId() {
    perguntaService.buscarPerguntaPorId(idDaPergunta).then(function (response) {
      $scope.pergunta = response.data.result;
      copiaPergunta = angular.copy($scope.pergunta);
      data  = $scope.pergunta.DataPergunta;
      email =   $scope.pergunta.Usuario.Email;
    })
  }

  function podeEditarPergunta() {
    if ((Date.parse(new Date()) - Date.parse(data))/(1000*3600*24) <= 7 && email === authService.getUsuario().Email) {
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
    if (authService.getUsuario() === undefined) {
      return false
    }
    return authService.getUsuario().Email == email;
  }

  function usuarioQueFezAPerguntaNaoMarcouNenhumaRespostaComoCorreta() {
    return usuarioLogado() && !temRespostaCorreta;
  }

  function marcarComoCorreta(idDaResposta){
     perguntaService. marcarComoCorreta(idDaResposta).then(function (response) {
      buscarQuantidadeDeRespostasPorIdDaPergunta()
      usuarioQueFezAPerguntaNaoMarcouNenhumaRespostaComoCorreta()
      buscarRespostaPorIdDaPergunta(idDaPergunta)
    })
  };

  function anterior() {
    if ($scope.pagina == 0) {
      return;
    }
    $scope.pagina = $scope.pagina- 1;
    buscarRespostaPorIdDaPergunta()
  }

  function proxima() {
    if ((5 * ($scope.pagina +1)) / $scope.totalDeRespostas >= 1) {
      return;
    }
    $scope.pagina =$scope.pagina + 1;
    buscarRespostaPorIdDaPergunta();
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

  function editarPergunta(pergunta) {
    abrirFecharEdicao();
    var perguntaModel = {Titulo:pergunta.Titulo, Descricao:pergunta.Descricao, TagsIds:pergunta.TagsIds, Id:pergunta.Id};
    perguntaService.editarPergunta(perguntaModel).then(function (response){
       buscarPerguntaPorId(idDaPergunta);
    })
  }

  function responderPergunta () {
    perguntaService.responderPergunta($scope.responder, $scope.pergunta.Id)
      .then(response => {
        new Noty({
                type: 'success',
                timeout: 2000,
                text:  'A resposta foi inserida!'                
            }).show();
         abrirFecharModal('R');
      }, fail => {
        new Noty({
                type: 'error',
                timeout: 2000,
                text:  fail.data.ExceptionMessage                
            }).show();
      })
  }

  function modalEdicao(){
    return {
      div: document.getElementById("div-edicao"),
      form: document.getElementById("form-edicao")
    }
  }

  function modalResponder(){
    return {
      div: document.getElementById("div-responder"),
      form: document.getElementById("form-responder")
    }
  }

  function modalComentar(){
    return {
      div: document.getElementById("div-comentar"),
      form: document.getElementById("form-comentar")
    }
  }

  function abrirFecharModal(botao) {
    let elemento;
    edicaoAberta = !edicaoAberta;
    switch(botao) {
      case 'E':
        elemento = modalEdicao();
        break;
      case 'R':
        elemento = modalResponder();
        break;
      case 'C':
        elemento = modalComentar();
        break;
    }    

    if (!edicaoAberta) {
      elemento.div.style.height = '0';
      elemento.form.style.opacity = '0';
    } else {
      elemento.div.style.height = '65vh';
      elemento.form.style.opacity = '1';
    }
  }

  function cancelarEdicao() {
    $scope.pergunta = angular.copy(copiaPergunta);
    abrirFecharModal('E');
  }

  function adicionarMarkdown(tipo, objeto) {
    $scope[objeto].Descricao = window.adicionarMarkdown(tipo, $scope[objeto].Descricao);    
  }
});
