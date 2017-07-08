angular.module('EstouroPilhaApp').factory('pesquisarPerguntaService', function ($http){

  var urlPerguntas = "http://localhost:53986/api/perguntas/paginacaopesquisa/"

  function   buscarPerguntaPorTitulo(perguntaPesquisada, paginaAtual){
    return $http.get(urlPerguntas + perguntaPesquisada + "/" + paginaAtual);
  };

  return{
    pesquisar : buscarPerguntaPorTitulo,
    pesquisasPaginada : buscarPerguntaPorTitulo
  }

});
