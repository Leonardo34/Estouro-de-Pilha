angular.module('EstouroPilhaApp').controller('cadastrarPerguntaController', 
                      function($scope, $routeParams, authService, cadastroPerguntaService){

  $scope.cadastrarPergunta = function(novaPergunta) {
    cadastroPerguntaService.cadastrarPergunta(novaPergunta)
      .then(response => {
        alert('Pergunta cadastrada com sucesso, Tchê!')
      }, error => {
        alert('Alguma coisa deu errada, tenta de novo, Tchê!');
      })
   }
});
