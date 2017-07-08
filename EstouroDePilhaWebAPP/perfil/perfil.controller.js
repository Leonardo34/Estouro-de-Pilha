angular.module('EstouroPilhaApp').controller('perfilController', function ($scope, $routeParams, perfilService, perguntaService, tagService){
    let id = $routeParams.id;

    pegarUsuario();
    pegarRespostas();
    pegarPerguntas();
    pegarTags();
    
    function pegarUsuario(){
        perfilService.pegarUsuario(id)
            .then(response => {
                $scope.usuario = response.data.result;
        },  fail => {
                console.log(fail.data)
        });
    }

    function pegarRespostas () {
        perguntaService.pegarRespostasDoUsuario(id)
            .then(response => {
                $scope.usuario.respostas = response.data.result;
        }, fail =>{
                console.log(fail.data);
        });
    }

    function pegarPerguntas () {
        perguntaService.pegarPerguntasDoUsuario(id)
            .then(response => {
                $scope.usuario.perguntas = response.data.result;
        },  fail => {
                console.log(fail.data);
        })
    }

    function pegarTags () {
        tagService.pegarTagsDoUsuario(id)
            .then(response => {
                $scope.usuario.tags = response.data.result;
        }, fail => {
                console.log(fail.data);
        })
    }
});
