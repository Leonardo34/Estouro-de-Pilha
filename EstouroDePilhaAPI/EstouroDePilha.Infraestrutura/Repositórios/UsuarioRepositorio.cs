﻿using EstouroDePilha.Dominio.Entidades;
using EstouroDePilha.Dominio.Repositórios;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Infraestrutura.Repositórios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly Contexto contexto;

        public UsuarioRepositorio(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public void Alterar(Usuario usuario)
        {
            contexto.Entry(usuario).State = System.Data.Entity.EntityState.Modified;
            contexto.SaveChanges();
        }

        public void Criar(Usuario usuario)
        {
            contexto.Usuarios.Add(usuario);
            contexto.SaveChanges();
        }

        public void Deletar(Usuario usuario)
        {
            contexto.Usuarios.Remove(usuario);
            contexto.SaveChanges();
        }

        public List<Usuario> Listar()
        {
            return contexto.Usuarios.ToList();
        }

        public Usuario ObterPorId(int id)
        {
            return contexto.Usuarios
                .Include("Badges")
                .FirstOrDefault(u => u.Id == id);
        }

        public Usuario ObterPorEmail(string email)
        {
            return contexto.Usuarios
                .Include("Badges")
                .Include("Perguntas")
                .Include("Respostas")
                .Include("Respostas.Usuario")
                .Include("Perguntas.Usuario")
                .FirstOrDefault(u => u.Email.Equals(email));
        }

        public int QuantidadeDownVotesUsuario(Usuario usuario)
        {
            int quantidadeDownVotesPergunta = contexto.DownVotesPerguntas
                .Where(d => d.Usuario.Id == usuario.Id)
                .Count();

            int quantidadeDownVotesResposta = contexto.DownVotesResposta
                .Where(d => d.Usuario.Id == usuario.Id)
                .Count();

            return quantidadeDownVotesPergunta + quantidadeDownVotesResposta;
        }

        public int QuantidadeUpVotesUsuario(Usuario usuario)
        {
            int quantidadeUpVotesPergunta = contexto.DownVotesPerguntas
                .Where(u => u.Usuario.Id == usuario.Id)
                .Count();

            int quantidadeUpVotesResposta = contexto.DownVotesResposta
                .Where(u => u.Usuario.Id == usuario.Id)
                .Count();

            return quantidadeUpVotesPergunta + quantidadeUpVotesResposta;
        }

        public List<Usuario> ObterUsuariosCadastraosHa(int dias)
        {
           return contexto.Usuarios
                .Where(u => DbFunctions.DiffDays(u.DataCadastro, DateTime.Now) >= dias)
                .ToList();            
        }

        public List<UpVotePergunta> ObterUpVotesPerguntaPorUsuario(Usuario usuario)
        {
            return contexto.UpVotesPerguntas
                .Where(u => u.Usuario.Id == usuario.Id)
                .ToList();
        }

        public List<UpVoteResposta> ObterUpVotesRespostaPorUsuario(Usuario usuario)
        {
            return contexto.UpVotesResposta
                .Where(u => u.Usuario.Id == usuario.Id)
                .ToList();
        }
    }
}
