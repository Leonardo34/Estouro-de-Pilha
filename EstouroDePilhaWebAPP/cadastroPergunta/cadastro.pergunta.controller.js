angular.module('EstouroPilhaApp')
  .controller('cadastrarPerguntaController', cadastrarPerguntaController);

function cadastrarPerguntaController($scope, $location, $routeParams, authService, cadastroPerguntaService, tagService) {
  $scope.cadastrarPergunta = cadastrarPergunta;
  $scope.adicionarMarkdown = adicionarMarkdown;
  $scope.tagsSelecionadas = [];

  buscarTags();

  function adicionarMarkdown (tipo){
    $scope.novaPergunta.Descricao = 
      window.adicionarMarkdown(tipo, $scope.novaPergunta.Descricao);
  } 

  function cadastrarPergunta(novaPergunta) {
    novaPergunta.TagsIds = $scope.tagsSelecionadas.map(t => $scope.tags.find(x => x.Descricao == t).Id);
    console.log(novaPergunta);
    cadastroPerguntaService.cadastrarPergunta(novaPergunta)
      .then(response => {
        alert('Pergunta cadastrada com sucesso, Tchê!');
        $location.path('/pergunta/' + response.data.result.id)
      }, error => {
        if($scope.novaPergunta.Titulo == null){
          alert('Pergunta sem titulo, tenta de novo, Tchê!');
        }
        if($scope.novaPergunta.Descricao == null){
          alert('Pergunta sem descrição, tenta de novo, Tchê')
        }
      })
  }

  function buscarTags() {
    tagService.pegarTodasTags().then(res => {
      $scope.tags = res.data.result;
      console.log($scope.tags);
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
      console.log($scope.tagsSelecionadas);
    } 
  }
}