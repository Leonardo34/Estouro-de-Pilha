angular.module('EstouroPilhaApp').controller('cadastroController',
 function($scope, $routeParams, $location, cadastroService, authService) {
  $scope.logout = authService.logout;
  $scope.cadastrarUsuario = function(novoUsuario) {
    cadastroService.cadastrarUsuario(novoUsuario)
      .then(response => {
        var usuario = {
          "email": novoUsuario.Email,
          "senha": novoUsuario.Senha
        }
        authService.login(usuario)
          .then(response => {
             $location.path('/home');
          })
      }, error => {
        if($scope.novoUsuario.Nome == null){
          alert('Nome do usuario não preenchido, tenta de novo, Tchê!');
        }
        else if($scope.novoUsuario.Apelido == null){
          alert('Apelido do usuario não preenchido, tenta de novo, Tchê!');
        }
        else if($scope.novoUsuario.Email == null){
          alert('Email do usuario não preenchido, tenta de novo, Tchê!');
        }
        else if($scope.novoUsuario.Endereco == null){
          alert('Endereco do usuario não preenchido, tenta de novo, Tchê!');
        }
        else if($scope.novoUsuario.Foto == null){
          alert('UrlFoto do usuario não preenchido, tenta de novo, Tchê!');
        }
        else if($scope.novoUsuario.Senha == null){
          alert('Senha do usuario não preenchido, tenta de novo, Tchê!');
        }
        else if($scope.novoUsuario.Descricao == null){
          alert('Descricao do usuario não preenchido, tenta de novo, Tchê!');
        }
      })
  }
});
