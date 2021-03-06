angular.module('EstouroPilhaApp')
  .controller('cadastrarPerguntaController', cadastrarPerguntaController);

function cadastrarPerguntaController($scope, $routeParams, $location,authService, perfilService, cadastroPerguntaService, tagService) {
  $scope.logout = authService.logout;
  $scope.cadastrarPergunta = cadastrarPergunta;
  $scope.adicionarMarkdown = adicionarMarkdown;
  $scope.estaLogado = authService.isAutenticado();
  $scope.tagsSelecionadas = [];
  $scope.idUsuarioAtivo = perfilService.pegarIdUsuarioAtivo();
  buscarTags();

  function adicionarMarkdown (tipo) {
    $scope.novaPergunta.Descricao =
      window.adicionarMarkdown(tipo, $scope.novaPergunta.Descricao);
  }

  function cadastrarPergunta(novaPergunta) {
    novaPergunta.TagsIds = $scope.tagsSelecionadas.map(t => $scope.tags.find(x => x.Descricao == t).Id);
    cadastroPerguntaService.cadastrarPergunta(novaPergunta)
      .then(response => {
        $location.path('/pergunta/' + response.data.result.id)
        new Noty({
            type: 'success',
            timeout: 2000,
            text: 'Pergunta cadastrada com sucesso!'
        }).show();
      }, fail => {
        fail.data.errors.forEach(erro => {
          new Noty({
              type: 'error',
              timeout: 2000,
              text: erro
          }).show();
        })
      })
  }

  function buscarTags() {
    tagService.pegarTodasTags().then(res => {
      $scope.tags = res.data.result;
    })
  }

  $scope.autocompleteTag = function(str, tags) {
    var matches = [];
    tags.forEach(tag => {
      if (tag.Descricao.includes(str)) {
        matches.push(tag);
      }
    })
    return matches;
  }

  $scope.adicionarTag = function(nomeTag) {
    if (!$scope.tagsSelecionadas.some(t => t === nomeTag)) {
      $scope.tagsSelecionadas.push(nomeTag);
      $scope.$broadcast('angucomplete-alt:clearInput');
    }
  }

  $scope.removeTag = function(nomeTag) {
    let index = $scope.tagsSelecionadas.indexOf(nomeTag);
    $scope.tagsSelecionadas.splice(index, 1);
  }
}
