angular.module('EstouroPilhaApp').factory('pesquisarPerguntaService', function ($http){

  var urlPerguntas = "http://localhost:53986/api/perguntas/"

  function   buscarPerguntaPorTitulo(perguntaPesquisada){
    return $http.get(urlPerguntas + perguntaPesquisada);
  };

  return{
    pesquisar : perguntaPesquisadaPorTitulo
  }

});
