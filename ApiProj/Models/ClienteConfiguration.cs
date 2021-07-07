using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiProj.Models
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder
                .ToTable("Clientes");

            builder
                .Property(c => c.Id)
                .HasColumnName("CDFLIFOR")
                .IsRequired()
                .HasComment("Código Cliente");

            builder
                .Property(c => c.Nome)
                .HasColumnName("NOCLIFOR")
                .IsRequired()
                .HasComment("Nome cliente/Fornecedor")
                .HasColumnType("varchar(60)");

            builder
                .Property(c => c.DDD)
                .HasColumnName("DDD_FONE")
                .HasColumnType("smallint(6)")
                .HasComment("DDD Fone");

            builder
                .Property(c => c.Fone)
                .HasColumnName("FONE")
                .HasColumnType("varchar(15)")
                .HasComment("Fone");

            builder
                .Property(c => c.CNPJouCpf)
                .HasColumnName("CNPJ_CPF")
                .HasColumnType("varchar(18)")
                .HasComment("CNPJ / CPF");
        }
    }
}
