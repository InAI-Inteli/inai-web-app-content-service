using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPIContentService.Domain.Entities;

namespace WebAPIContentService.Infra.Data.Mappings
{
    public class MaterialUsuarioConfiguration : IEntityTypeConfiguration<MaterialUsuario>
    {
        public void Configure(EntityTypeBuilder<MaterialUsuario> builder)
        {
            builder.HasKey(e => e.IdMaterialUsuario)
                .HasName("materiais_usuarios_pkey");

            builder.ToTable("materiais_usuarios");

            // Configuração das propriedades da entidade MaterialUsuario
            builder.Property(e => e.IdMaterialUsuario).HasColumnName("id");

            builder.Property(e => e.DataEntrega).HasColumnName("data_entrega");

            builder.Property(e => e.EntregouNoPrazo).HasColumnName("entregou_no_prazo");

            builder.Property(e => e.IdMaterial).HasColumnName("id_material");

            builder.Property(e => e.IdUsuario).HasColumnName("id_usuario");

            builder.Property(e => e.Nota).HasColumnName("nota");

            builder.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");

            builder.Property(e => e.UpdateAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            builder.Property(e => e.Status)
                .HasColumnType("status_enum")
                .HasColumnName("status_enum");

            builder.HasOne(d => d.IdMaterialNavigation)
                .WithMany(p => p.MaterialUsuarios)
                .HasForeignKey(d => d.IdMaterial)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_material");
        }
    }
}
