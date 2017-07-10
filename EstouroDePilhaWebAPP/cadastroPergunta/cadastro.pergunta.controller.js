angular.module('EstouroPilhaApp')
  .controller('cadastrarPerguntaController', cadastrarPerguntaController);

function cadastrarPerguntaController($scope, $location, $routeParams, authService, cadastroPerguntaService) {
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
        $location.path('/pergunta/' + response.data.result.id)
      }, error => {
        if($scope.novaPergunta.Titulo == null){
          alert('Pergunta sem titulo, tenta de novo, Tchê!');
        }
        if($scope.novaPergunta.Descricao == null){
          alert('Pergunta sem descrição, tenta de novo, Tchê')
        }
      })
  }  
}