angular.module('EstouroPilhaApp').service("perguntaService", function ($http){

  var urlPerguntas = "http://localhost:53986/api/perguntas/"

  var urlResposta = "http://localhost:53986/api/respostas/"

  function   buscarPerguntaPorId(idDaPergunta){
    return $http.get(urlPerguntas + idDaPergunta);
  };

  function  buscarRespostaPorIdDaPergunta(idDaPergunta){
    return $http.get(urlResposta+"pergunta/" + idDaPergunta);
  };

  return{
    buscarPerguntaPorId : buscarPerguntaPorId,
    buscarRespostaPorIdDaPergunta : buscarRespostaPorIdDaPergunta
  }

});
