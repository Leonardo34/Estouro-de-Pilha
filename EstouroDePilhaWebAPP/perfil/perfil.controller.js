angular.module('EstouroPilhaApp').controller('perfilController', function ($scope, $location, $routeParams, perfilService, perguntaService, tagService, authService){
    let id = $routeParams.id;
    $scope.logout = authService.logout;
    $scope.logado = authService.isAutenticado() && authService.getUsuario().Id === Number($routeParams.id);
    $scope.salvarEdicao = salvarEdicao;
    $scope.abrirFecharModalEdicao = abrirFecharModalEdicao;
    $scope.estaLogado = authService.isAutenticado();
    $scope.usuario = [];
    $scope.alternarModal = false;
    $scope.idUsuarioAtivo = perfilService.pegarIdUsuarioAtivo();
    pegarUsuario();



    function pegarUsuario(){
        perfilService.pegarUsuario(id)
            .then(response => {
                $scope.usuario = response.data.result;
                pegarRespostas();
                pegarPerguntas();
                pegarTags();
        },  fail => {
            $location.path("/home");
            new Noty({
                type: 'error',
                timeout: 2000,
                text:  fail.data.ExceptionMessage
            }).show();

        });
    }

    function pegarRespostas () {
        perguntaService.pegarRespostasDoUsuario(id)
            .then(response => {
                $scope.usuario.respostas = response.data.result;
                $scope.usuario.respostas.forEach(r => perguntaService.removerMarkdown(r));
                $scope.existeRespostas = $scope.usuario.respostas.length > 0;
        }, fail =>{
                console.log(fail.data);
        });
    }

    function pegarPerguntas () {
        perguntaService.pegarPerguntasDoUsuario(id)
            .then(response => {
                $scope.usuario.perguntas = response.data.result;
                $scope.existePerguntas = $scope.usuario.perguntas.length > 0;
        },  fail => {
                console.log(fail.data);
        })
    }

    function pegarTags () {
        tagService.pegarTagsDoUsuario(id)
            .then(response => {
                $scope.usuario.tags = response.data.result;
                $scope.existeTags = $scope.usuario.tags.length > 0;
        }, fail => {
                console.log(fail.data);
        })
    }

    function salvarEdicao() {
        if($scope.formEdicao.$valid){
            perfilService.editarUsuario($scope.usuario)
                .then(response => {
                    $location.path("/perfil/" + $scope.usuario.Id);
                    console.log(response.data);
                    abrirFecharModalEdicao();
                    new Noty({
                        type: 'success',
                        timeout: 2000,
                        text: 'Usuario editado com sucesso!'
                    }).show();
                }, fail => {
                    console.log(fail.data);
                });
        }
    }

    function abrirFecharModalEdicao() {
        $scope.alternarModal = !$scope.alternarModal;
    }
});
