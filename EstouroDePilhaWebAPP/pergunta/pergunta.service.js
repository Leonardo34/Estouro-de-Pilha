app.factory("perguntaService", function ($http){

  var urlPerguntas = "http://localhost:53986/api/perguntas/"

  var urlResposta = "http://localhost:53986/api/respostas/"

  function   buscarPerguntaPorId(id){
    return $http.get(urlPerguntas + 3);
  };

  function  buscarRespostaPorIdDaPergunta(id){
    return $http.get(urlResposta + 3);
  };

  return{
    buscarPerguntaPorId : buscarPerguntaPorId,
    buscarRespostaPorIdDaPergunta : buscarRespostaPorIdDaPergunta
  }

});
