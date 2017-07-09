angular.module('EstouroPilhaApp')
  .controller('cadastrarPerguntaController', cadastrarPerguntaController);

function cadastrarPerguntaController($scope, $routeParams, authService, cadastroPerguntaService) { 
  $scope.cadastrarPergunta = cadastrarPergunta;
  $scope.adicionarMarkdown = adicionarMarkdown;
  

  function cadastrarPergunta(novaPergunta) {
    cadastroPerguntaService.cadastrarPergunta(novaPergunta)
      .then(response => {
        alert('Pergunta cadastrada com sucesso, Tchê!');
      }, error => {
        alert('Alguma coisa deu errada, tenta de novo, Tchê!');
      })
  } 

  function adicionarMarkdown (tipo) {
    let texto = angular.copy($scope.novaPergunta.Descricao);
    let novoTexto = "";
    let selecao = window.getSelection().toString().trim();
    let resultado = "";
    let textarea = angular.element(document.getElementById("descricao"));
    switch(tipo) {
      case 'B':
        resultado = `**${selecao}**`;
        break;
      case 'I':
        resultado = `_${selecao}_`;
        break;
      case 'OL':
        resultado = `\n1. ${selecao}`;
        break;
      case 'UL':
        resultado = `\n* ${selecao}`;
        break;
      case 'C':
        resultado = '``'+ selecao + '``';
        break;
      case 'H':       
        resultado = 
        selecao.charAt(1) === ' '  || selecao.charAt(1) === '#'  ? 
              `#${selecao}` : `# ${selecao}`;
        break;      
    }

    novoTexto = texto.replace(selecao, resultado);
    $scope.novaPergunta.Descricao = novoTexto;
  }
}