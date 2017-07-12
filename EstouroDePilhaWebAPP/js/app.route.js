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
    .when('/cadastroPergunta', {
      controller: 'cadastrarPerguntaController',
      templateUrl: 'cadastroPergunta/cadastroPergunta.html',
      resolve: {
        autenticado: function (authService) {
          if(!authService.isAutenticado()){
            new Noty({
                type: 'error',
                timeout: 2000,
                text: 'Precisa te cadastrar, Tchê!'
            }).show();
            return authService.isAutenticadoPromise();
          }
        }
      }
    })
    .when('/pergunta/:id', {
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
    .otherwise({
      redirectTo: '/home'
    });
});
