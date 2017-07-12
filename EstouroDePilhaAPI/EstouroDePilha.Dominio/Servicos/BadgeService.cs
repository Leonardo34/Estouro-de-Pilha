using EstouroDePilha.Dominio.Repositórios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EstouroDePilha.Dominio.Entidades;

namespace EstouroDePilha.Dominio.Servicos
{
    public class BadgeService : IBadgeService
    {
        private readonly IPerguntaRepositorio perguntaRepositorio;
        private readonly IUsuarioRepositorio usuarioRepositorio;
        private readonly ITagRepositorio tagRepositorio;
        private readonly IRespostaRepositorio respostaRepositorio;
        private readonly IBadgeRepositorio badgeRepositorio;

        public BadgeService(IPerguntaRepositorio perguntaRepositorio, 
                IUsuarioRepositorio usuarioRepositorio, 
                ITagRepositorio tagRepositorio, 
                IRespostaRepositorio respostaRepositorio,
                IBadgeRepositorio badgeRepositorio)
        {
            this.perguntaRepositorio = perguntaRepositorio;
            this.usuarioRepositorio = usuarioRepositorio;
            this.tagRepositorio = tagRepositorio;
            this.respostaRepositorio = respostaRepositorio;
            this.badgeRepositorio = badgeRepositorio;
        }

        public void ChecarBadges(Usuario usuario)
        {
            Badge badgeGuri = badgeRepositorio.ObterPorId(1);
            Badge badgeTramposo = badgeRepositorio.ObterPorId(2);
            usuario.AdicionaBadgeGuri(badgeGuri);
            usuario.AdicionaBadgeTramposo(badgeTramposo);
            usuarioRepositorio.Alterar(usuario);
        }
    }
}
