angular.module('EstouroPilhaApp').controller('cadastrarPerguntaController', function ($scope, authService, perguntaService){

  $scope.cadastrarPergunta = function(novaPergunta) {
    cadastroService.cadastrarPergunta(novaPergunta)
            .then(response => {
              alert('Pergunta cadastrada com sucesso, Tchê!')
            }, error => {
              alert('Alguma coisa deu errada, tenta de novo, Tchê!');
            })
   }
});
