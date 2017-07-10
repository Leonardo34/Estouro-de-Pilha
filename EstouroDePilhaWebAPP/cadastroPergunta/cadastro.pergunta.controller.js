angular.module('EstouroPilhaApp')
  .controller('cadastrarPerguntaController', cadastrarPerguntaController);

function cadastrarPerguntaController($scope, $routeParams, authService, cadastroPerguntaService) {
  $scope.cadastrarPergunta = cadastrarPergunta;
  $scope.adicionarMarkdown = adicionarMarkdown;
  
  listenerMudanca();

  function cadastrarPergunta(novaPergunta) {
    cadastroPerguntaService.cadastrarPergunta(novaPergunta)
      .then(response => {
        alert('Pergunta cadastrada com sucesso, Tchê!');
      }, error => {
        alert('Alguma coisa deu errada, tenta de novo, Tchê!');
      })
  }

  function adicionarMarkdown(tipo) {
    if (typeof $scope.novaPergunta.Descricao === 'undefined') return;

    let texto = angular.copy($scope.novaPergunta.Descricao);
    let novoTexto = "";
    let resultado = "";
    let selecao;    
    selecao = window.selecao.trim();

    switch (tipo) {
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
        resultado = '``' + selecao + '``';
        break;
      case 'H':
        resultado =
          selecao.charAt(1) === ' ' || selecao.charAt(1) === '#' ?
          `#${selecao}` : `# ${selecao}`;
        break;
    }
    let parteInicial = texto.substring(0, window.inicioSelecao);
    let parteComMarkdown = texto.substring(window.inicioSelecao).replace(selecao, resultado);
    novoTexto =  parteInicial + parteComMarkdown;
        
    $scope.novaPergunta.Descricao = novoTexto;
  }

  function listenerMudanca() {
    let input = document.getElementById("descricao");
    input.addEventListener('select', function () { 
      //verifica se algo foi selecionado para atualizar o valor    
      if (input.selectionStart !== input.selectionEnd) {
        window.selecao =
          input.value.substring(input.selectionStart, input.selectionEnd);
        window.inicioSelecao = input.selectionStart;
      }
    });
  }
}