app.controller('perfilController', function ($scope, $routeParams, perfilService){
    
    pegarUsuario();

    function pegarUsuario(){
        perfilService.pegarUsuario($routeParams.id)
            .then(response => {
                $scope.usuario = response.data.result;
        },  fail => {
                console.log(fail.data)
        });
    }
});
