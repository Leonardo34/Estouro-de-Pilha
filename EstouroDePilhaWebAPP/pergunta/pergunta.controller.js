app.controller('perguntaController', function ($scope, authService, perguntaService){

  buscarPerguntaPorId();

  function buscarPerguntaPorId(id){
    perguntaService.buscarPerguntaPorId(id).then(function (response){
      $scope.pergunta = response.data.result;
    })
  }

  function buscarRespostaPorIdDaPergunta(id){
    perguntaService.buscarRespostaPorIdDaPergunta(id).then(function (response){
      $scope.respostas = response.data.result;
    })
  }

});
