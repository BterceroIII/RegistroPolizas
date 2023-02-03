using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProyectoPoliza.Models;

public partial class GestionSegurosSaContext : DbContext
{
    public GestionSegurosSaContext()
    {
    }

    public GestionSegurosSaContext(DbContextOptions<GestionSegurosSaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<ContratoPoliza> ContratoPolizas { get; set; }

    public virtual DbSet<DetalleContrato> DetalleContratos { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Poliza> Polizas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Vehiculo> Vehiculos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("Server=BTERCERO\\SQLEXPRESS; DataBase=Gestion_SegurosSA; Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente);

            entity.ToTable("Cliente");

            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Nacionalidad)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NoDocIdentidad)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombreCliente)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.TipoDocIdentidad)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ContratoPoliza>(entity =>
        {
            entity.HasKey(e => e.IdContrato);

            entity.ToTable("ContratoPoliza");

            entity.Property(e => e.IdContrato).HasColumnName("idContrato");
            entity.Property(e => e.Comentario).IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaFin).HasColumnType("datetime");
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.IdEmpleadoGestor).HasColumnName("idEmpleadoGestor");
            entity.Property(e => e.IdEmpleadoTramitador).HasColumnName("idEmpleadoTramitador");
            entity.Property(e => e.IdPoliza).HasColumnName("idPoliza");
            entity.Property(e => e.OrigenFondoPago)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.ContratoPolizas)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK_ContratoPoliza_Cliente");

            entity.HasOne(d => d.IdEmpleadoGestorNavigation).WithMany(p => p.ContratoPolizaIdEmpleadoGestorNavigations)
                .HasForeignKey(d => d.IdEmpleadoGestor)
                .HasConstraintName("FK_ContratoPoliza_Empleados1");

            entity.HasOne(d => d.IdEmpleadoTramitadorNavigation).WithMany(p => p.ContratoPolizaIdEmpleadoTramitadorNavigations)
                .HasForeignKey(d => d.IdEmpleadoTramitador)
                .HasConstraintName("FK_ContratoPoliza_Empleados");

            entity.HasOne(d => d.IdPolizaNavigation).WithMany(p => p.ContratoPolizas)
                .HasForeignKey(d => d.IdPoliza)
                .HasConstraintName("FK_ContratoPoliza_Poliza");
        });

        modelBuilder.Entity<DetalleContrato>(entity =>
        {
            entity.HasKey(e => e.IdDetalle);

            entity.ToTable("DetalleContrato");

            entity.Property(e => e.IdDetalle).HasColumnName("idDetalle");
            entity.Property(e => e.IdContrato).HasColumnName("idContrato");
            entity.Property(e => e.IdVehiculo).HasColumnName("idVehiculo");

            entity.HasOne(d => d.IdContratoNavigation).WithMany(p => p.DetalleContratos)
                .HasForeignKey(d => d.IdContrato)
                .HasConstraintName("FK_DetalleContrato_ContratoPoliza");

            entity.HasOne(d => d.IdVehiculoNavigation).WithMany(p => p.DetalleContratos)
                .HasForeignKey(d => d.IdVehiculo)
                .HasConstraintName("FK_DetalleContrato_Vehiculo");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado);

            entity.Property(e => e.IdEmpleado).HasColumnName("idEmpleado");
            entity.Property(e => e.Cargo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Cedula)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Poliza>(entity =>
        {
            entity.HasKey(e => e.IdPoliza);

            entity.ToTable("Poliza");

            entity.Property(e => e.IdPoliza).HasColumnName("idPoliza");
            entity.Property(e => e.Codigo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EmpresaProveedora)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Tipo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario);

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Contraseña)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.IdEmpleado).HasColumnName("idEmpleado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Rol)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuarios_Empleados");
        });

        modelBuilder.Entity<Vehiculo>(entity =>
        {
            entity.HasKey(e => e.IdVehiculo);

            entity.ToTable("Vehiculo");

            entity.Property(e => e.IdVehiculo).HasColumnName("idVehiculo");
            entity.Property(e => e.Año)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.Marca)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Modelo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NoChasis)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NoCirculacion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NoMotor)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NoPlaca)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Tipo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Uso)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Vehiculos)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK_Vehiculo_Cliente");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
