angular.module('EstouroPilhaApp').service("perguntaService", function ($http){

  var urlPerguntas = "http://localhost:53986/api/perguntas/"
  var urlResposta = "http://localhost:53986/api/respostas/"

  function buscarPerguntaPorId(idDaPergunta) {
    return $http.get(urlPerguntas + idDaPergunta);
  };
  function  buscarRespostaPorIdDaPergunta(pagina, idDaPergunta){
    return $http.get(`${urlResposta}pergunta/${pagina}/${idDaPergunta}`);
  };

  function  buscarQuantidadeDeRespostasPorIdDaPergunta(idDaPergunta){
    return $http.get(`${urlResposta}numeroDeRespostasDaPergunta/${idDaPergunta}`);

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

  function pegarRespostasDoUsuario(id) {
    return $http.get(`${urlResposta}/usuario/${id}`)
  }

  return{
    buscarPerguntaPorId : buscarPerguntaPorId,
    buscarRespostaPorIdDaPergunta : buscarRespostaPorIdDaPergunta,
    pegarPerguntasDoUsuario: pegarPerguntasDoUsuario,
    pegarRespostasDoUsuario: pegarRespostasDoUsuario,
    buscarPerguntasPaginadas : buscarPerguntasPaginadas,
    marcarComoCorreta : marcarComoCorreta,
    buscarQuantidadeDeRespostasPorIdDaPergunta : buscarQuantidadeDeRespostasPorIdDaPergunta
  }
});
