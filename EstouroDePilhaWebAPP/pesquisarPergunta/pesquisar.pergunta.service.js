angular.module('EstouroPilhaApp').factory('pesquisarPerguntaService', function ($http){

  var urlPerguntas = "http://localhost:53986/api/perguntas/"

  function   buscarPerguntaPorTitulo(perguntaPesquisada){
    return $http.get(urlPerguntas + "pesquisa/" + perguntaPesquisada);
  };

  function paginacao(pagina) {
    return $http.get(urlPerguntas + "paginacao/" + pagina)
  };

  return{
    pesquisar : buscarPerguntaPorTitulo,
    paginacao : paginacao
  }

});
