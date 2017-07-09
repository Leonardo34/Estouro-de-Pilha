angular.module('EstouroPilhaApp').controller('perguntaController', function ($scope, $routeParams, authService, perguntaService){
  var email;
  var idDaPergunta = $routeParams.id;
  $scope.usuarioLogado = usuarioLogado;
  $scope.marcarComoCorreta = marcarComoCorreta;
  buscarPerguntaPorId(idDaPergunta);
  buscarRespostaPorIdDaPergunta(idDaPergunta);

  function buscarPerguntaPorId(idDaPergunta){
    perguntaService.buscarPerguntaPorId(idDaPergunta).then(function (response){
      $scope.pergunta = response.data.result;
      email =   $scope.pergunta.Usuario.Email;
    })
   }

  function buscarRespostaPorIdDaPergunta(idDaPergunta){
    perguntaService.buscarRespostaPorIdDaPergunta(idDaPergunta).then(function (response){
      $scope.respostas = response.data.result;
      console.log(  $scope.respostas);
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

});
