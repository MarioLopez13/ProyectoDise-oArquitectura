using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace DiseñoArquitecturaProyecto.Models.DB;

public partial class ProyectContext : DbContext
{
    private readonly IConfiguration _configuration;
    public ProyectContext(DbContextOptions<ProyectContext> options, IConfiguration configuration)
    {
        _configuration = configuration;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
    public virtual DbSet<Auditoria> Auditorias { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Reporte> Reportes { get; set; }

    public virtual DbSet<Transaccion> Transaccions { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Auditoria>(entity =>
        {
            entity.HasKey(e => e.IdAditoria).HasName("Auditorias_pk");

            entity.Property(e => e.IdAditoria)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("Id_Aditoria");
            entity.Property(e => e.DetalleAuditoria)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("Detalle_Auditoria");
            entity.Property(e => e.FechaAuditoria)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_Auditoria");
            entity.Property(e => e.IdTransaccion)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("Id_Transaccion");
            entity.Property(e => e.Ubicacion)
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.HasOne(d => d.IdTransaccionNavigation).WithMany(p => p.Auditoria)
                .HasForeignKey(d => d.IdTransaccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Transaccion_Auditorias_fk");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("Cliente_pk");

            entity.ToTable("Cliente");

            entity.Property(e => e.IdCliente)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("Id_Cliente");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Cedula).HasColumnType("numeric(10, 0)");
            entity.Property(e => e.Contrasea)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Cuenta).HasColumnType("numeric(10, 0)");
            entity.Property(e => e.Direccion)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.Nombres)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TotalCuenta)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Total_Cuenta");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.IdLog).HasName("Logs_pk");

            entity.Property(e => e.IdLog)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("Id_LOG");
            entity.Property(e => e.Detalle)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Evento)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.IdUsuario)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("Id_Usuario");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Logs)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Usuario_Logs_fk");
        });

        modelBuilder.Entity<Reporte>(entity =>
        {
            entity.HasKey(e => e.IdReporte).HasName("Reportes_pk");

            entity.Property(e => e.IdReporte)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("Id_Reporte");
            entity.Property(e => e.Contenido)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.FechaReporte)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_Reporte");
            entity.Property(e => e.IdUsuario)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("Id_Usuario");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Reportes)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Usuario_Reportes_fk");
        });

        modelBuilder.Entity<Transaccion>(entity =>
        {
            entity.HasKey(e => e.IdTransaccion).HasName("Transaccion_pk");

            entity.ToTable("Transaccion");

            entity.Property(e => e.IdTransaccion)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("Id_Transaccion");
            entity.Property(e => e.Estado)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.FechaTransaccion)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_Transaccion");
            entity.Property(e => e.IdCliente)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("Id_Cliente");
            entity.Property(e => e.IdOrigenCli)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("Id_OrigenCli");
            entity.Property(e => e.Monto).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Transaccions)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Cliente_Transaccion_fk");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("Usuario_pk");

            entity.ToTable("Usuario");

            entity.Property(e => e.IdUsuario)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("Id_Usuario");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Cedula).HasColumnType("numeric(10, 0)");
            entity.Property(e => e.Contrasea)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Nombres)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Rol)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
