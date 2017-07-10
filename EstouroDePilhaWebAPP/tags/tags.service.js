angular.module('EstouroPilhaApp').service("tagService", tagService);

function tagService ($http) {
    let url = "http://localhost:53986/api/tags";

    var pegarTagsDoUsuario = (id) => $http.get(`${url}/usuario/${id}`, id);

    var pegarTodasTags = () => $http.get(`${url}`);

    return {
        pegarTagsDoUsuario : pegarTagsDoUsuario,
        pegarTodasTags : pegarTodasTags
    } 
}