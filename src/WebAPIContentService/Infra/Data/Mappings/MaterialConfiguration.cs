using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPIContentService.Domain.Entities;

namespace WebAPIContentService.Infra.Data.Mappings
{
    public class MaterialConfiguration : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.HasKey(e => e.IdMaterial)
                .HasName("materiais_pkey");

            builder.ToTable("materiais");

            // Configuração das propriedades da entidade materiais
            builder.Property(e => e.IdMaterial).HasColumnName("id");

            builder.Property(e => e.Ativo).HasColumnName("ativo");

            builder.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");

            builder.Property(e => e.CreatedBy).HasColumnName("created_by");

            builder.Property(e => e.DataFinal).HasColumnName("data_final");

            builder.Property(e => e.Descricao)
                .HasColumnType("character varying")
                .HasColumnName("descricao");

            builder.Property(e => e.IdDiretoria).HasColumnName("id_diretoria");

            builder.Property(e => e.Obrigatorio).HasColumnName("obrigatorio");

            builder.Property(e => e.PesoNota).HasColumnName("peso_nota");

            builder.Property(e => e.Tipo)
                .HasColumnType("character varying")
                .HasColumnName("tipo");

            builder.Property(e => e.Titulo)
                .HasMaxLength(100)
                .HasColumnName("titulo");

            builder.Property(e => e.UpdateAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            builder.Property(e => e.Url)
                .HasColumnType("character varying")
                .HasColumnName("url");

        }
    }
}
