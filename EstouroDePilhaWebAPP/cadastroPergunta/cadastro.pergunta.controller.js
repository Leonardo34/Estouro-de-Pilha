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
    if(typeof $scope.novaPergunta.Descricao === 'undefined') return;

    let texto = angular.copy($scope.novaPergunta.Descricao);
    let novoTexto = "";
    let resultado = ""; 
    let selecao;
    if(document.getSelection().toString())
      selecao = document.getSelection().toString();
    else {
      selecao = document.getElementById("descricao").value;  
    }   
    selecao.trim();    
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