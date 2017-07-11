angular.module('EstouroPilhaApp').service("perguntaService", function ($http){

  var urlPerguntas = "http://localhost:53986/api/perguntas/"
  var urlResposta = "http://localhost:53986/api/respostas/"

  function buscarPerguntaPorId(idDaPergunta) {
    return $http.get(urlPerguntas + idDaPergunta);
  };

  function  buscarRespostaPorIdDaPergunta(pagina, idDaPergunta){
    return $http.get(`${urlResposta}pergunta/${idDaPergunta}?skip=${pagina}`);
  };

  function  buscarQuantidadeDeRespostasPorIdDaPergunta(idDaPergunta){
    return $http.get(`${urlResposta}numeroDeRespostasDaPergunta/${idDaPergunta}`);
  };

  function buscarTotalPerguntasCadastradas() {
    return $http.get(`${urlPerguntas}total`);
  };

  function pegarPerguntasDoUsuario(id) {
    return $http.get(`${urlPerguntas}/usuario/${id}`);
  }

  function marcarComoCorreta(id) {
    return $http.put(`${urlResposta}correta/${id}`)
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

  function pegarRespostasDoUsuario(id) {
    return $http.get(`${urlResposta}/usuario/${id}`);
  }

  function editarPergunta(perguntaModel) {
    return $http.put(`${urlPerguntas}/editar`, perguntaModel);
  }

  function editarResposta(respostaModel) {
    return $http.put(`${urlResposta}/editar/${respostaModel.Id}`, respostaModel);
  }

  function responderPergunta(respostaModel, idPergunta) {
    return $http.post(`${urlResposta}/nova/${idPergunta}`, respostaModel);
  }
  
  function upvotePergunta (idPergunta) {
    return $http.post(`${urlPerguntas}${idPergunta}/upvote`, {});
  }

  function downvotePergunta (idPergunta) {
    return $http.post(`${urlPerguntas}${idPergunta}/downvote`, {});
  }

  return {
    buscarPerguntaPorId : buscarPerguntaPorId,
    buscarRespostaPorIdDaPergunta : buscarRespostaPorIdDaPergunta,
    pegarPerguntasDoUsuario : pegarPerguntasDoUsuario,
    pegarRespostasDoUsuario : pegarRespostasDoUsuario,
    buscarPerguntasPaginadas : buscarPerguntasPaginadas,
    marcarComoCorreta : marcarComoCorreta,
    buscarQuantidadeDeRespostasPorIdDaPergunta : buscarQuantidadeDeRespostasPorIdDaPergunta,
    upvoteResposta : upvoteResposta,
    downvoteResposta : downvoteResposta,
    editarPergunta : editarPergunta,
    editarResposta : editarResposta,
    buscarTotalPerguntasCadastradas : buscarTotalPerguntasCadastradas,
    responderPergunta: responderPergunta
    upvotePergunta : upvotePergunta,
    downvotePergunta : downvotePergunta
  }
});
