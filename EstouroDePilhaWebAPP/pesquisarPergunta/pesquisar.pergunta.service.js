angular.module('EstouroPilhaApp').factory('pesquisarPerguntaService', function ($http){

  var urlPerguntas = "http://localhost:53986/api/perguntas/pesquisa/paginada/"
  var urlNumeroderesultados = "http://localhost:53986/api/perguntas/pesquisa/"

  function   buscarPerguntaPorTitulo(    paginaAtual, conteudo, tags){
    return $http.get(urlPerguntas  + paginaAtual + "/" + encodeURIComponent(conteudo) +"/" + encodeURIComponent(tags));
  };

  function    numeroDeResultadosDaPesquisa(conteudo, tags){
    return $http.get(urlNumeroderesultados +encodeURIComponent(conteudo) +"/" + encodeURIComponent(tags));
  };

  return{
    pesquisarTrazerResultados : buscarPerguntaPorTitulo,
    numeroDeResultadosDaPesquisa : numeroDeResultadosDaPesquisa
  }
});
