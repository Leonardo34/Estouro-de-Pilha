app.factory("perguntaService", function ($http){

  var urlPerguntas = "http://localhost:53986/api/perguntas/"

  var urlPerguntas = "http://localhost:53986/api/respostas/"

  function   buscarPerguntaPorId(id){
    return $http.get(url+ 3);
  };

  function  buscarRespostaPorIdDaPergunta(id){
    return $http.get(url+ 3);
  };

  return{
    buscarPerguntaPorId : buscarPerguntaPorId,
    buscarRespostaPorIdDaPergunta : buscarRespostaPorIdDaPergunta
  }

});
