angular.module('EstouroPilhaApp').controller('perfilController', function ($scope, $routeParams, perfilService, perguntaService, tagService, authService){
    let id = $routeParams.id;

    $scope.logado = authService.isAutenticado();
    $scope.salvarEdicao = salvarEdicao;
    $scope.abrirFecharModalEdicao = abrirFecharModalEdicao;
    $scope.alternarModal = false;

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

    function salvarEdicao() {
        if($scope.formEdicao.$valid){
            perfilService.editarUsuario($scope.usuario)
                .then(response => {
                    console.log(response.data);
                }, fail => {
                    console.log(fail.data);
                });
        }
    }

    function abrirFecharModalEdicao() {
        $scope.alternarModal = !$scope.alternarModal;       
    }
});
