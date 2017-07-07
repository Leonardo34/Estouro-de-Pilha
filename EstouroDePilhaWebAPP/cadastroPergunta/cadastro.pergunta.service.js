angular.module('EstouroPilhaApp').service('cadastroPerguntaService', function ($http) {

  this.cadastrarPergunta = function(pergunta) {
    return $http.post('http://localhost:53986/api/perguntas/nova', pergunta);
  }
})
