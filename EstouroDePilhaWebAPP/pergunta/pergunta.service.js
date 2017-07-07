angular.module('EstouroPilhaApp').service("perguntaService", function ($http){

  var urlPerguntas = "http://localhost:53986/api/perguntas/"

  var urlResposta = "http://localhost:53986/api/respostas/"

  function   buscarPerguntaPorId(id){
    return $http.get(urlPerguntas + 13);
  };

  function  buscarRespostaPorIdDaPergunta(id){
    return $http.get(urlResposta+"pergunta/" + 13);
  };

  return{
    buscarPerguntaPorId : buscarPerguntaPorId,
    buscarRespostaPorIdDaPergunta : buscarRespostaPorIdDaPergunta
  }

});
