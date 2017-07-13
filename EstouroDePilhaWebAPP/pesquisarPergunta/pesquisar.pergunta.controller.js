angular.module('EstouroPilhaApp').controller('pesquisarPerguntaController', function ($scope, authService, tagService, pesquisarPerguntaService){
  $scope.logout = authService.logout;
  $scope.pesquisar = pesquisar;
  $scope.anterior = anterior;
  $scope.proxima = proxima;
  $scope.estaLogado = authService.isAutenticado();
  $scope.pagina = 0;
  var perguntaBuscada;

  $scope.tagsSelecionadas = [];
  var tags =   $scope.tagsSelecionadas;
  buscarTags();
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
    }
  }

  $scope.removeTag = function(nomeTag) {
    let index = $scope.tagsSelecionadas.indexOf(nomeTag);
    $scope.tagsSelecionadas.splice(index, 1);
  }

  function anterior(){
    if ($scope.pagina == 0){
      return;
    }
    $scope.ultimaPagina = false;
    $scope.pagina = $scope.pagina -1;
    pesquisarTrazerResultados(perguntaBuscada);
  }

  function proxima(){
    $scope.ultimaPagina = false;
    if ((10 * ($scope.pagina +1))/$scope.numeroDeResultadosDaPesquisa >= 1) {
      $scope.ultimaPagina = true;
      return;
    }
    $scope.pagina = $scope.pagina +1;
    pesquisarTrazerResultados(perguntaBuscada);
  }

  function pesquisar (busca){
    $scope.pagina = 0;
    perguntaBuscada = busca;
    if (perguntaBuscada === ''){
    perguntaBuscada = undefined;
    }
    pesquisarTrazerResultados(perguntaBuscada);
    numeroDeResultadosDaPesquisa (perguntaBuscada);
    $scope.mostrarPaginacao = false;
  }

  function pesquisarTrazerResultados(perguntaBuscada) {
    pesquisarPerguntaService.pesquisarTrazerResultados(
      $scope.pagina, perguntaBuscada, tags.toString().replace(","," ")).then(function (response){
        $scope.perguntasPesquisadas = response.data.result;
        $scope.busca = undefined;
        $scope.tags = []
    })
  }

  function numeroDeResultadosDaPesquisa (perguntaBuscada){
    pesquisarPerguntaService.numeroDeResultadosDaPesquisa(perguntaBuscada, tags.toString().replace(","," ")).then(function (response){
      $scope.numeroDeResultadosDaPesquisa = response.data.result;
      if ($scope.numeroDeResultadosDaPesquisa > 10){
          $scope.mostrarPaginacao = true;
       }
    })
  }
});
