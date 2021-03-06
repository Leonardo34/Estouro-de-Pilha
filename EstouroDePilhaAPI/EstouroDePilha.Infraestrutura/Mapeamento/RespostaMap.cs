﻿using EstouroDePilha.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Infraestrutura.Mapeamento
{
    public class RespostaMap : EntityTypeConfiguration<Resposta>
    {
        public RespostaMap()
        {
            ToTable("Repostas");
            HasKey(x => x.Id);

            Property(x => x.EhRespostaCorreta);

            HasRequired(x => x.Usuario)
                .WithMany()
                .Map(x => x.MapKey("IdUsuario"));

            HasRequired(x => x.Pergunta)
                .WithMany()
                .Map(x => x.MapKey("IdPergunta"))
                .WillCascadeOnDelete(false);

            HasMany(x => x.UpVotes)
                .WithRequired(x => x.Resposta)
                .Map(x => x.MapKey("IdResposta"));

            HasMany(x => x.DownVotes)
                .WithRequired(x => x.Resposta)
                .Map(x => x.MapKey("IdResposta"));

            HasMany(x => x.Comentarios)
                .WithRequired(x => x.Resposta)
                .Map(x => x.MapKey("IdResposta"));
        }
    }
}