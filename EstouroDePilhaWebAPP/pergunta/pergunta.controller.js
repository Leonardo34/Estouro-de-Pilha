angular.module('EstouroPilhaApp').controller('perguntaController', function ($scope, $routeParams, authService, perguntaService){
  var email;
  var idDaPergunta = $routeParams.id;
  var pagina = 0;
  $scope.proxima = proxima;
  $scope.anterior = anterior;
  $scope.usuarioLogado = usuarioLogado;
  $scope.marcarComoCorreta = marcarComoCorreta;
  buscarPerguntaPorId(idDaPergunta);
  buscarRespostaPorIdDaPergunta();

  function buscarPerguntaPorId(idDaPergunta){
    perguntaService.buscarPerguntaPorId(idDaPergunta).then(function (response){
      $scope.pergunta = response.data.result;
      email =   $scope.pergunta.Usuario.Email;
    })
   }


  function buscarRespostaPorIdDaPergunta(){
    perguntaService.buscarRespostaPorIdDaPergunta(pagina, idDaPergunta).then(function (response){
      $scope.respostas = response.data.result;
      buscarQuanditadeDeRespostasPorIdDaPergunta()
    })
  }

  function buscarQuanditadeDeRespostasPorIdDaPergunta(){
    perguntaService. buscarQuanditadeDeRespostasPorIdDaPergunta(idDaPergunta).then(function(response){
      $scope.totalDeRespostas = response.data.result
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
     if (pagina == 0)
     {
       return;
     }
     pagina = pagina-1;
     buscarRespostaPorIdDaPergunta()
    }

   function proxima(){
     if ((5 * pagina)/$scope.numeroDeResultadosDaPesquisa > 0) {
       return;
     }
     pagina = pagina +1;
     buscarRespostaPorIdDaPergunta()
   }

});
