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

    .when('/home', {
      controller: 'homeController',
      templateUrl: 'home/home.html'
    })

    .when('/tags', {
      controller: 'tagsController',
      templateUrl: 'tags/tags.html'
    })

    .when('/cadastroPergunta', {
      controller: 'cadastroPerguntaController',
      templateUrl: 'cadastroPergunta/cadastroPergunta.html'
    })

    .when('/pergunta', {
      controller: 'perguntaController',
      templateUrl: 'pergunta/pergunta.html'
    })

    .when('/perfil/:id', {
      controller: 'perfilController',
      templateUrl: 'perfil/perfil.html'
    })

    .when('/pesquisarPergunta', {
      controller: 'pesquisarPerguntaController',
      templateUrl: 'pesquisarPergunta/pesquisar.pergunta.html'
    })

    .otherwise({redirectTo: '/home'});
});
