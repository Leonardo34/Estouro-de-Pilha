angular.module('EstouroPilhaApp').config(function ($routeProvider) {

    $routeProvider

    .when('/login', {
      controller: 'loginController',
      templateUrl: 'login/login.html'
    })

    .when('/cadastro', {
      controller: 'cadastroController',
      templateUrl: 'cadastro/cadastro.html'
    })

    .when('/tags', {
      controller: 'tagsController',
      templateUrl: 'tags/tags.html'
    })

    .when('/cadastroPergunta', {
      controller: 'cadastroPerguntaController',
      templateUrl: 'cadastroPergunta/cadastroPergunta.html'
    })

    .otherwise({redirectTo: '/cadastro'});
});
