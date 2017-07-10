angular.module('EstouroPilhaApp')
  .controller('cadastrarPerguntaController', cadastrarPerguntaController);

function cadastrarPerguntaController($scope, $routeParams, authService, cadastroPerguntaService) {
  $scope.cadastrarPergunta = cadastrarPergunta;
  $scope.adicionarMarkdown = adicionarMarkdown;

  function adicionarMarkdown (tipo){
    $scope.novaPergunta.Descricao = 
      window.adicionarMarkdown(tipo, $scope.novaPergunta.Descricao);
  } 

  function cadastrarPergunta(novaPergunta) {
    cadastroPerguntaService.cadastrarPergunta(novaPergunta)
      .then(response => {
        alert('Pergunta cadastrada com sucesso, Tchê!');
      }, error => {
        alert('Alguma coisa deu errada, tenta de novo, Tchê!');
      })
  }  
}