window.adicionarMarkdown = adicionarMarkdown;
window.listenerMudanca = listenerMudanca;

function adicionarMarkdown(tipo, elemScope) {
    if (typeof elemScope === 'undefined') return;

    let texto = angular.copy(elemScope);
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
        
    return novoTexto;
  }

  function listenerMudanca(elemento) {           
    if (elemento.selectionStart !== elemento.selectionEnd) {
    window.selecao =
        elemento.value.substring(elemento.selectionStart, elemento.selectionEnd);
    window.inicioSelecao = elemento.selectionStart;
    }    
  }
