app.service("perfilService", function($http){
    let url = "http://localhost:53986/api/usuarios/"

    var pegarUsuario = (id) => $http.get(`${url}/${id}`, id);

    return {
        pegarUsuario: pegarUsuario
    }
});