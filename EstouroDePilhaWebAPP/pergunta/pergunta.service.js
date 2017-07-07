angular.module('EstouroPilhaApp').service("perguntaService", function ($http){

  var urlPerguntas = "http://localhost:53986/api/perguntas/"

  var urlResposta = "http://localhost:53986/api/respostas/"

  function   buscarPerguntaPorId(id){
    return $http.get(urlPerguntas + 3);
  };

  function  buscarRespostaPorIdDaPergunta(id){
    return $http.get(urlResposta+"pergunta/" + 3);
  };

  function pegarPerguntasDoUsuario(id) {
    return $http.get(`${urlPerguntas}/usuario/${id}`);
  }

  function pegarRespostasDoUsuario(id) {
    return $http.get(`${urlResposta}/usuario/${id}`)
  }
  return{
    buscarPerguntaPorId : buscarPerguntaPorId,
    buscarRespostaPorIdDaPergunta : buscarRespostaPorIdDaPergunta,
    pegarPerguntasDoUsuario: pegarPerguntasDoUsuario,
    pegarRespostasDoUsuario: pegarRespostasDoUsuario
  }

});
