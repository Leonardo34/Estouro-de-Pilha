angular.module('EstouroPilhaApp').service("perguntaService", function ($http){

  var urlPerguntas = "http://localhost:53986/api/perguntas/"
  var urlResposta = "http://localhost:53986/api/respostas/"

  function buscarPerguntaPorId(idDaPergunta) {
    return $http.get(urlPerguntas + idDaPergunta);
  };

  function buscarRespostaPorIdDaPergunta(idDaPergunta) {
    return $http.get(urlResposta+"pergunta/" + idDaPergunta);
  };

  function pegarPerguntasDoUsuario(id) {
    return $http.get(`${urlPerguntas}/usuario/${id}`);
  }

  function pegarRespostasDoUsuario(id) {
    return $http.get(`${urlResposta}/usuario/${id}`)
  }

  function buscarPerguntasPaginadas(skip, take) {
    return $http.get(`${urlPerguntas}?skip=` + skip + '&take=' + take);
  }
  
  function upvoteResposta (idResposta) {
    return $http.post(`${urlResposta}/${idResposta}/upvote`, {});
  }

  function downvoteResposta (idResposta) {
    return $http.post(`${urlResposta}/${idResposta}/downvote`, {});
  }

  return{
    buscarPerguntaPorId : buscarPerguntaPorId,
    buscarRespostaPorIdDaPergunta : buscarRespostaPorIdDaPergunta,
    pegarPerguntasDoUsuario : pegarPerguntasDoUsuario,
    pegarRespostasDoUsuario : pegarRespostasDoUsuario,
    buscarPerguntasPaginadas : buscarPerguntasPaginadas
  }

});
