angular.module('EstouroPilhaApp').factory('pesquisarPerguntaService', function ($http){

  var urlPerguntas = "http://localhost:53986/api/perguntas/paginacaopesquisa/"
  var urlNumeroderesultados = "http://localhost:53986/api/perguntas/pesquisa/numeroderesultados/"

  function   buscarPerguntaPorTitulo(perguntaPesquisada, paginaAtual){
    return $http.get(urlPerguntas + perguntaPesquisada + "/" + paginaAtual);
  };

  function    numeroDeResultadosDaPesquisa(perguntaPesquisada){
    return $http.get(urlNumeroderesultados  + perguntaPesquisada);
  };

  return{
    pesquisarTrazerResultados : buscarPerguntaPorTitulo,
    numeroDeResultadosDaPesquisa : numeroDeResultadosDaPesquisa
  }
});
