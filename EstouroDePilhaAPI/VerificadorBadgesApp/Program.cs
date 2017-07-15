using EstouroDePilha.Dominio.Entidades;
using EstouroDePilha.Dominio.Repositórios;
using EstouroDePilha.Dominio.Servicos;
using EstouroDePilha.Infraestrutura;
using EstouroDePilha.Infraestrutura.Repositórios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerificadorBadgesApp
{
    public class Program
    {
        private static readonly int TOTAL_DIAS_ANO = 365;

        private static Contexto contexto = new Contexto();
        private static IUsuarioRepositorio usuarioRepositorio = new UsuarioRepositorio(contexto);
        private static IBadgeRepositorio badgeRepositorio = new BadgeRepositorio(contexto);
        private static IBadgeService badgeService = 
            new BadgeService(usuarioRepositorio, badgeRepositorio);

        public static void Main(string[] args)
        {
            List<Usuario> usuariosCadastradosAMaisDeUmAno =
                usuarioRepositorio.ObterUsuariosCadastraosHa(TOTAL_DIAS_ANO);
               

            List<Usuario> usuariosCadastradosAMaisDeTresAnos =
                usuarioRepositorio.ObterUsuariosCadastraosHa(3 * TOTAL_DIAS_ANO);

            foreach (var each in usuariosCadastradosAMaisDeUmAno)
            {
                badgeService.UsuarioSeCadastrouHaUmAno(each);
            }
            foreach (var each in usuariosCadastradosAMaisDeTresAnos)
            {
                badgeService.UsuarioSeCadastrouHaTresAnos(each);
            }
        }
    }
}
