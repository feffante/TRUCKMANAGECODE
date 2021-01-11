using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TRUCKMODEL.Models
{
    public partial class truckmanageContext : DbContext
    {
        //"server=localhost;database=truckmanage;user=root;pwd=Stralis!"
         string Connession =ConfigurationManager.AppSettings["truckmanageContextCon"].ToString();
        public truckmanageContext()
        {
        }

        public truckmanageContext(DbContextOptions<truckmanageContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AnagraficaFornitori> AnagraficaFornitori { get; set; }
        public virtual DbSet<AnagraficaImpiegati> AnagraficaImpiegati { get; set; }
        public virtual DbSet<AnagraficaMezzi> AnagraficaMezzi { get; set; }
        public virtual DbSet<AnagraficaMezziMarca> AnagraficaMezziMarca { get; set; }
        public virtual DbSet<AnagraficaMezziTipo> AnagraficaMezziTipo { get; set; }
        public virtual DbSet<ApplicationLog> ApplicationLog { get; set; }
        public virtual DbSet<Magazzino> Magazzino { get; set; }
        public virtual DbSet<MagazzinoCategorie> MagazzinoCategorie { get; set; }
        public virtual DbSet<MagazzinoIva> MagazzinoIva { get; set; }
        public virtual DbSet<MagazzinoLogCarico> MagazzinoLogCarico { get; set; }
        public virtual DbSet<MagazzinoLogScarico> MagazzinoLogScarico { get; set; }
        public virtual DbSet<MagazzinoUnitaMisure> MagazzinoUnitaMisure { get; set; }
        public virtual DbSet<ManutenzioneMagazzino> ManutenzioneMagazzino { get; set; }
        public virtual DbSet<ManutenzioneProgrammataStorico> ManutenzioneProgrammataStorico { get; set; }
        public virtual DbSet<ManutenzioneProgrammataTipo> ManutenzioneProgrammataTipo { get; set; }
        public virtual DbSet<ManutenzioneProgrammata> ManutenzioneProgrammata { get; set; }
        public virtual DbSet<ManutenzioneTipo> ManutenzioneTipo { get; set; }
        public virtual DbSet<ManutenzioneUnitaMisuraUsura> ManutenzioneUnitaMisuraUsura { get; set; }
        public virtual DbSet<Manutenzioni> Manutenzioni { get; set; }
        public virtual DbSet<Sysdiagram> Sysdiagram { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<ViewCarico> ViewCarico { get; set; }
        public virtual DbSet<ViewMagazzino> ViewMagazzino { get; set; }
        public virtual DbSet<ViewManutenzioneMateriale> ViewManutenzioneMateriale { get; set; }
        public virtual DbSet<ViewManutenzioneProgrammataStorico> ViewManutenzioneProgrammataStorico { get; set; }
        public virtual DbSet<ViewManutenzioneProgrammata> ViewManutenzioneProgrammata { get; set; }
        public virtual DbSet<ViewManutenzioni> ViewManutenzioni { get; set; }
        public virtual DbSet<ViewMezzi> ViewMezzi { get; set; }
        public virtual DbSet<ViewScarico> ViewScarico { get; set; }
        public virtual DbSet<VisualizzaLibretto> VisualizzaLibretto { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //"server=localhost;database=truckmanage;user=feffe;pwd=Stralis450e5!"
                optionsBuilder.UseMySql(Connession, Microsoft.EntityFrameworkCore.ServerVersion.FromString("10.5.8-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnagraficaFornitori>(entity =>
            {
                entity.HasKey(e => e.IdAnagFornitore)
                    .HasName("PRIMARY");

                entity.ToTable("anagrafica_fornitori");

                entity.Property(e => e.IdAnagFornitore)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("ID_ANAG_FORNITORE");

                entity.Property(e => e.Cap)
                    .HasColumnType("varchar(10)")
                    .HasColumnName("CAP")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Cell)
                    .HasColumnType("varchar(20)")
                    .HasColumnName("CELL")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Cfisc)
                    .HasColumnType("varchar(20)")
                    .HasColumnName("CFISC")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Citta)
                    .HasColumnType("varchar(50)")
                    .HasColumnName("CITTA")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.CodiceFornitore)
                    .HasColumnType("int(11)")
                    .HasColumnName("CODICE_FORNITORE");

                entity.Property(e => e.Contatto)
                    .HasColumnType("varchar(50)")
                    .HasColumnName("CONTATTO")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Email)
                    .HasColumnType("varchar(50)")
                    .HasColumnName("EMAIL")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Indirizzo)
                    .HasColumnType("varchar(150)")
                    .HasColumnName("INDIRIZZO")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Nome)
                    .HasColumnType("varchar(50)")
                    .HasColumnName("NOME")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Note)
                    .HasColumnType("varchar(500)")
                    .HasColumnName("NOTE")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.NumFax)
                    .HasColumnType("varchar(20)")
                    .HasColumnName("NUM_FAX")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.NumTel)
                    .HasColumnType("varchar(20)")
                    .HasColumnName("NUM_TEL")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Piva)
                    .HasColumnType("varchar(20)")
                    .HasColumnName("PIVA")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<AnagraficaImpiegati>(entity =>
            {
                entity.HasKey(e => e.IdImpiegato)
                    .HasName("PRIMARY");

                entity.ToTable("anagrafica_impiegati");

                entity.Property(e => e.IdImpiegato)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("ID_IMPIEGATO");

                entity.Property(e => e.Cap)
                    .HasColumnType("varchar(10)")
                    .HasColumnName("CAP")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Cellulare)
                    .HasColumnType("varchar(20)")
                    .HasColumnName("CELLULARE")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Cfisc)
                    .HasColumnType("varchar(20)")
                    .HasColumnName("CFISC")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Citta)
                    .HasColumnType("varchar(20)")
                    .HasColumnName("CITTA")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.CognomeImpiegato)
                    .HasColumnType("varchar(30)")
                    .HasColumnName("COGNOME_IMPIEGATO")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.DtInserimento)
                    .HasMaxLength(6)
                    .HasColumnName("DT_INSERIMENTO");

                entity.Property(e => e.Email)
                    .HasColumnType("varchar(100)")
                    .HasColumnName("EMAIL")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Indirizzo)
                    .HasColumnType("varchar(100)")
                    .HasColumnName("INDIRIZZO")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.NomeImpiegato)
                    .HasColumnType("varchar(30)")
                    .HasColumnName("NOME_IMPIEGATO")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.NumTel)
                    .HasColumnType("varchar(20)")
                    .HasColumnName("NUM_TEL")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<AnagraficaMezzi>(entity =>
            {
                entity.HasKey(e => e.IdMezzo)
                    .HasName("PRIMARY");

                entity.ToTable("anagrafica_mezzi");

                entity.Property(e => e.IdMezzo)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("ID_MEZZO");

                entity.Property(e => e.Allestimento)
                    .IsRequired()
                    .HasColumnType("varchar(250)")
                    .HasColumnName("ALLESTIMENTO")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Cv)
                    .HasColumnType("int(11)")
                    .HasColumnName("CV");

                entity.Property(e => e.DataImm)
                    .HasMaxLength(6)
                    .HasColumnName("DATA_IMM");

                entity.Property(e => e.IdMarca)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_MARCA");

                entity.Property(e => e.IdTipoMezzo)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_TIPO_MEZZO");

                entity.Property(e => e.Modello)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("MODELLO")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Note)
                    .IsRequired()
                    .HasColumnType("varchar(500)")
                    .HasColumnName("NOTE")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Targha)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("TARGHA")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Telaio)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("TELAIO")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<AnagraficaMezziMarca>(entity =>
            {
                entity.HasKey(e => e.IdMarca)
                    .HasName("PRIMARY");

                entity.ToTable("anagrafica_mezzi_marca");

                entity.Property(e => e.IdMarca)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("ID_MARCA");

                entity.Property(e => e.Marca)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("MARCA")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Note)
                    .IsRequired()
                    .HasColumnType("varchar(250)")
                    .HasColumnName("NOTE")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<AnagraficaMezziTipo>(entity =>
            {
                entity.HasKey(e => e.IdTipoMezzo)
                    .HasName("PRIMARY");

                entity.ToTable("anagrafica_mezzi_tipo");

                entity.Property(e => e.IdTipoMezzo)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("ID_TIPO_MEZZO");

                entity.Property(e => e.TipoMezzo)
                    .HasColumnType("varchar(50)")
                    .HasColumnName("TIPO_MEZZO")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<ApplicationLog>(entity =>
            {
                entity.ToTable("application_log");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasColumnType("varchar(3000)")
                    .HasColumnName("MESSAGE")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Severity)
                    .HasColumnType("int(11)")
                    .HasColumnName("SEVERITY");

                entity.Property(e => e.Timestamp)
                    .HasMaxLength(6)
                    .HasColumnName("TIMESTAMP");
            });

            modelBuilder.Entity<Magazzino>(entity =>
            {
                entity.HasKey(e => e.IdMagazzino)
                    .HasName("PRIMARY");

                entity.ToTable("magazzino");

                entity.HasIndex(e => e.IdCategoria, "FK_MAGAZZINO_MAGAZZINO_CATEGORIE");

                entity.HasIndex(e => e.IdIva, "FK_MAGAZZINO_MAGAZZINO_IVA");

                entity.HasIndex(e => e.IdUnitaMisura, "FK_MAGAZZINO_MAGAZZINO_UNITA_MISURE");

                entity.Property(e => e.IdMagazzino)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("ID_MAGAZZINO");

                entity.Property(e => e.CodiceArticolo)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("CODICE_ARTICOLO")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.CodiceBarre)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasColumnName("CODICE_BARRE")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.FlagScarico).HasColumnName("FLAG_SCARICO");

                entity.Property(e => e.IdArticolo)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("ID_ARTICOLO")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.IdCategoria)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_CATEGORIA");

                entity.Property(e => e.IdFornitore)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_FORNITORE");

                entity.Property(e => e.IdIva)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_IVA");

                entity.Property(e => e.IdMarca)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_MARCA");

                entity.Property(e => e.IdUnitaMisura)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_UNITA_MISURA");

                entity.Property(e => e.ImagePath)
                    .IsRequired()
                    .HasColumnType("varchar(1000)")
                    .HasColumnName("IMAGE_PATH")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.NomeArticolo)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("NOME_ARTICOLO")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Note)
                    .IsRequired()
                    .HasColumnType("varchar(500)")
                    .HasColumnName("NOTE")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.PrezzoUnitario)
                    .HasPrecision(19, 3)
                    .HasColumnName("PREZZO_UNITARIO");

                entity.Property(e => e.Quantita).HasColumnName("QUANTITA");

                entity.Property(e => e.QuantitaMin).HasColumnName("QUANTITA_MIN");

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Magazzinos)
                    .HasForeignKey(d => d.IdCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MAGAZZINO_MAGAZZINO_CATEGORIE");

                entity.HasOne(d => d.IdIvaNavigation)
                    .WithMany(p => p.Magazzinos)
                    .HasForeignKey(d => d.IdIva)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MAGAZZINO_MAGAZZINO_IVA");

                entity.HasOne(d => d.IdUnitaMisuraNavigation)
                    .WithMany(p => p.Magazzinos)
                    .HasForeignKey(d => d.IdUnitaMisura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MAGAZZINO_MAGAZZINO_UNITA_MISURE");
            });

            modelBuilder.Entity<MagazzinoCategorie>(entity =>
            {
                entity.HasKey(e => e.IdCategoria)
                    .HasName("PRIMARY");

                entity.ToTable("magazzino_categorie");

                entity.Property(e => e.IdCategoria)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("ID_CATEGORIA");

                entity.Property(e => e.Categoria)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("CATEGORIA")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.DescrizioneCategoria)
                    .IsRequired()
                    .HasColumnType("varchar(200)")
                    .HasColumnName("DESCRIZIONE_CATEGORIA")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<MagazzinoIva>(entity =>
            {
                entity.HasKey(e => e.IdIva)
                    .HasName("PRIMARY");

                entity.ToTable("magazzino_iva");

                entity.Property(e => e.IdIva)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("ID_IVA");

                entity.Property(e => e.DescrizioneIva)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("DESCRIZIONE_IVA")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Iva)
                    .HasColumnType("int(11)")
                    .HasColumnName("IVA");
            });

            modelBuilder.Entity<MagazzinoLogCarico>(entity =>
            {
                entity.HasKey(e => e.IdCarico)
                    .HasName("PRIMARY");

                entity.ToTable("magazzino_log_carico");

                entity.Property(e => e.IdCarico)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("ID_CARICO");

                entity.Property(e => e.DtCarico)
                    .HasMaxLength(6)
                    .HasColumnName("DT_CARICO");

                entity.Property(e => e.IdMagazzino)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_MAGAZZINO");

                entity.Property(e => e.IdUser)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_USER");

                entity.Property(e => e.Nota)
                    .IsRequired()
                    .HasColumnType("varchar(1000)")
                    .HasColumnName("NOTA")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<MagazzinoLogScarico>(entity =>
            {
                entity.HasKey(e => e.IdScarico)
                    .HasName("PRIMARY");

                entity.ToTable("magazzino_log_scarico");

                entity.Property(e => e.IdScarico)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("ID_SCARICO");

                entity.Property(e => e.DtScarico)
                    .HasMaxLength(6)
                    .HasColumnName("DT_SCARICO");

                entity.Property(e => e.IdMagazzino)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_MAGAZZINO");

                entity.Property(e => e.IdUser)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_USER");

                entity.Property(e => e.Nota)
                    .IsRequired()
                    .HasColumnType("varchar(1000)")
                    .HasColumnName("NOTA")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<MagazzinoUnitaMisure>(entity =>
            {
                entity.HasKey(e => e.IdUnitaMisura)
                    .HasName("PRIMARY");

                entity.ToTable("magazzino_unita_misure");

                entity.Property(e => e.IdUnitaMisura)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("ID_UNITA_MISURA");

                entity.Property(e => e.DescrizioneUnitaMisura)
                    .IsRequired()
                    .HasColumnType("varchar(200)")
                    .HasColumnName("DESCRIZIONE_UNITA_MISURA")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.UnitaMisura)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("UNITA_MISURA")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<ManutenzioneMagazzino>(entity =>
            {
                entity.HasKey(e => e.IdMagazzinoManutenzione)
                    .HasName("PRIMARY");

                entity.ToTable("manutenzione_magazzino");

                entity.Property(e => e.IdMagazzinoManutenzione)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("ID_MAGAZZINO_MANUTENZIONE");

                entity.Property(e => e.IdMagazzino)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_MAGAZZINO");

                entity.Property(e => e.IdManutenzione)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_MANUTENZIONE");

                entity.Property(e => e.Quantita)
                    .HasColumnType("int(11)")
                    .HasColumnName("QUANTITA");
            });

            modelBuilder.Entity<ManutenzioneProgrammataStorico>(entity =>
            {
                entity.HasKey(e => e.IdManutenzioneProgrammata)
                    .HasName("PRIMARY");

                entity.ToTable("manutenzione_programmata_storico");

                entity.Property(e => e.IdManutenzioneProgrammata)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("ID_MANUTENZIONE_PROGRAMMATA");

                entity.Property(e => e.DtInserimento)
                    .HasColumnType("date")
                    .HasColumnName("DT_INSERIMENTO");

                entity.Property(e => e.DtPrevista)
                    .HasColumnType("date")
                    .HasColumnName("DT_PREVISTA");

                entity.Property(e => e.GiorniPreavviso)
                    .HasColumnType("int(11)")
                    .HasColumnName("GIORNI_PREAVVISO");

                entity.Property(e => e.IdMezzo)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_MEZZO");

                entity.Property(e => e.IdTipoManutenzioneProgrammata)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_TIPO_MANUTENZIONE_PROGRAMMATA");

                entity.Property(e => e.KmDeltaProssimoIntervento)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("KM_DELTA_PROSSIMO_INTERVENTO");

                entity.Property(e => e.KmGiornalieri)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("KM_GIORNALIERI");

                entity.Property(e => e.KmPresunti)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("KM_PRESUNTI");

                entity.Property(e => e.KmReali)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("KM_REALI");

                entity.Property(e => e.ManutenzioneChiusa).HasColumnName("MANUTENZIONE_CHIUSA");
            });

            modelBuilder.Entity<ManutenzioneProgrammataTipo>(entity =>
            {
                entity.HasKey(e => e.IdTipoManutenzioneProgrammata)
                    .HasName("PRIMARY");

                entity.ToTable("manutenzione_programmata_tipo");

                entity.Property(e => e.IdTipoManutenzioneProgrammata)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("ID_TIPO_MANUTENZIONE_PROGRAMMATA");

                entity.Property(e => e.TipoManutenzioneProgrammata)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("TIPO_MANUTENZIONE_PROGRAMMATA")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<ManutenzioneProgrammata>(entity =>
            {
                entity.HasKey(e => e.IdManutenzioneProgrammata)
                    .HasName("PRIMARY");

                entity.ToTable("manutenzione_programmata");

                entity.HasIndex(e => e.IdMezzo, "FK_MANUTENZIONE_PROGRAMMATA_ANAGRAFICA_MEZZI");

                entity.HasIndex(e => e.IdTipoManutenzioneProgrammata, "FK_MANUTENZIONE_PROGRAMMATA_MANUTENZIONE_PROGRAMMATA_TIPO");

                entity.Property(e => e.IdManutenzioneProgrammata)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("ID_MANUTENZIONE_PROGRAMMATA");

                entity.Property(e => e.DtInserimento)
                    .HasColumnType("date")
                    .HasColumnName("DT_INSERIMENTO");

                entity.Property(e => e.DtPrevista)
                    .HasColumnType("date")
                    .HasColumnName("DT_PREVISTA");

                entity.Property(e => e.GiorniPreavviso)
                    .HasColumnType("int(11)")
                    .HasColumnName("GIORNI_PREAVVISO");

                entity.Property(e => e.IdMezzo)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_MEZZO");

                entity.Property(e => e.IdTipoManutenzioneProgrammata)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_TIPO_MANUTENZIONE_PROGRAMMATA");

                entity.Property(e => e.KmDeltaProssimoIntervento)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("KM_DELTA_PROSSIMO_INTERVENTO");

                entity.Property(e => e.KmGiornalieri)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("KM_GIORNALIERI");

                entity.Property(e => e.KmPresunti)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("KM_PRESUNTI");

                entity.Property(e => e.KmReali)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("KM_REALI");

                entity.Property(e => e.ManutenzioneChiusa).HasColumnName("MANUTENZIONE_CHIUSA");

                entity.HasOne(d => d.IdMezzoNavigation)
                    .WithMany(p => p.ManutenzioneProgrammata)
                    .HasForeignKey(d => d.IdMezzo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MANUTENZIONE_PROGRAMMATA_ANAGRAFICA_MEZZI");

                entity.HasOne(d => d.IdTipoManutenzioneProgrammataNavigation)
                    .WithMany(p => p.ManutenzioneProgrammata)
                    .HasForeignKey(d => d.IdTipoManutenzioneProgrammata)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MANUTENZIONE_PROGRAMMATA_MANUTENZIONE_PROGRAMMATA_TIPO");
            });

            modelBuilder.Entity<ManutenzioneTipo>(entity =>
            {
                entity.HasKey(e => e.IdTipoManutenzione)
                    .HasName("PRIMARY");

                entity.ToTable("manutenzione_tipo");

                entity.Property(e => e.IdTipoManutenzione)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("ID_TIPO_MANUTENZIONE");

                entity.Property(e => e.Note)
                    .IsRequired()
                    .HasColumnType("varchar(250)")
                    .HasColumnName("NOTE")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.TipoManutenzione)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("TIPO_MANUTENZIONE")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<ManutenzioneUnitaMisuraUsura>(entity =>
            {
                entity.HasKey(e => e.IdUmUsura)
                    .HasName("PRIMARY");

                entity.ToTable("manutenzione_unita_misura_usura");

                entity.Property(e => e.IdUmUsura)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("ID_UM_USURA");

                entity.Property(e => e.Descrizione)
                    .IsRequired()
                    .HasColumnType("varchar(250)")
                    .HasColumnName("DESCRIZIONE")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.UmUsura)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("UM_USURA")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<Manutenzioni>(entity =>
            {
                entity.HasKey(e => e.IdManutenzione)
                    .HasName("PRIMARY");

                entity.ToTable("manutenzioni");

                entity.HasIndex(e => e.IdMezzo, "FK_MANUTENZIONI_ANAGRAFICA_MEZZI");

                entity.HasIndex(e => e.IdManutenzioneTipo, "FK_MANUTENZIONI_MANUTENZIONE_TIPO");

                entity.HasIndex(e => e.IdUnitaMisura, "FK_MANUTENZIONI_MANUTENZIONE_UNITA_MISURA_USURA");

                entity.Property(e => e.IdManutenzione)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("ID_MANUTENZIONE");

                entity.Property(e => e.Descrizione)
                    .IsRequired()
                    .HasColumnType("longtext")
                    .HasColumnName("DESCRIZIONE")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.DtFine)
                    .HasColumnType("date")
                    .HasColumnName("DT_FINE");

                entity.Property(e => e.DtInizio)
                    .HasColumnType("date")
                    .HasColumnName("DT_INIZIO");

                entity.Property(e => e.IdManutenzioneTipo)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_MANUTENZIONE_TIPO");

                entity.Property(e => e.IdMezzo)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_MEZZO");

                entity.Property(e => e.IdUnitaMisura)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_UNITA_MISURA");

                entity.Property(e => e.Note)
                    .IsRequired()
                    .HasColumnType("longtext")
                    .HasColumnName("NOTE")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.PrezzoTot)
                    .HasPrecision(19, 4)
                    .HasColumnName("PREZZO_TOT");

                entity.Property(e => e.UmValore)
                    .HasColumnType("int(11)")
                    .HasColumnName("UM_VALORE");

                entity.HasOne(d => d.IdManutenzioneTipoNavigation)
                    .WithMany(p => p.Manutenzionis)
                    .HasForeignKey(d => d.IdManutenzioneTipo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MANUTENZIONI_MANUTENZIONE_TIPO");

                entity.HasOne(d => d.IdMezzoNavigation)
                    .WithMany(p => p.Manutenzionis)
                    .HasForeignKey(d => d.IdMezzo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MANUTENZIONI_ANAGRAFICA_MEZZI");

                entity.HasOne(d => d.IdUnitaMisuraNavigation)
                    .WithMany(p => p.Manutenzionis)
                    .HasForeignKey(d => d.IdUnitaMisura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MANUTENZIONI_MANUTENZIONE_UNITA_MISURA_USURA");
            });

            modelBuilder.Entity<Sysdiagram>(entity =>
            {
                entity.HasKey(e => e.DiagramId)
                    .HasName("PRIMARY");

                entity.ToTable("sysdiagrams");

                entity.Property(e => e.DiagramId)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("diagram_id");

                entity.Property(e => e.Definition).HasColumnName("definition");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(128)")
                    .HasColumnName("name")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.PrincipalId)
                    .HasColumnType("int(11)")
                    .HasColumnName("principal_id");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PRIMARY");

                entity.ToTable("users");

                entity.Property(e => e.IdUser)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_USER");

                entity.Property(e => e.DtCreazione)
                    .HasMaxLength(6)
                    .HasColumnName("DT_CREAZIONE");

                entity.Property(e => e.Note)
                    .IsRequired()
                    .HasColumnType("varchar(150)")
                    .HasColumnName("NOTE")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasColumnName("PASSWORD")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.User1)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasColumnName("USER")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<ViewCarico>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_carico");

                entity.Property(e => e.CodiceArticolo)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("CODICE_ARTICOLO")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.CognomeImpiegato)
                    .HasColumnType("varchar(30)")
                    .HasColumnName("COGNOME_IMPIEGATO")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.DtCarico)
                    .HasMaxLength(6)
                    .HasColumnName("DT_CARICO");

                entity.Property(e => e.IdCarico)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_CARICO");

                entity.Property(e => e.IdMagazzino)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_MAGAZZINO");

                entity.Property(e => e.IdUser)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_USER");

                entity.Property(e => e.NomeArticolo)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("NOME_ARTICOLO")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.NomeImpiegato)
                    .HasColumnType("varchar(30)")
                    .HasColumnName("NOME_IMPIEGATO")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Nota)
                    .IsRequired()
                    .HasColumnType("varchar(1000)")
                    .HasColumnName("NOTA")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<ViewMagazzino>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_magazzino");

                entity.Property(e => e.Categoria)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("CATEGORIA")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.CodiceArticolo)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("CODICE_ARTICOLO")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.CodiceBarre)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasColumnName("CODICE_BARRE")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.CodiceFornitore)
                    .HasColumnType("int(11)")
                    .HasColumnName("CODICE_FORNITORE");

                entity.Property(e => e.FlagScarico).HasColumnName("FLAG_SCARICO");

                entity.Property(e => e.IdArticolo)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("ID_ARTICOLO")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.IdCategoria)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_CATEGORIA");

                entity.Property(e => e.IdFornitore)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_FORNITORE");

                entity.Property(e => e.IdIva)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_IVA");

                entity.Property(e => e.IdMagazzino)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_MAGAZZINO");

                entity.Property(e => e.IdMarca)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_MARCA");

                entity.Property(e => e.IdUnitaMisura)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_UNITA_MISURA");

                entity.Property(e => e.ImagePath)
                    .IsRequired()
                    .HasColumnType("varchar(1000)")
                    .HasColumnName("IMAGE_PATH")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Iva)
                    .HasColumnType("int(11)")
                    .HasColumnName("IVA");

                entity.Property(e => e.Marca)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("MARCA")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.NomeArticolo)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("NOME_ARTICOLO")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.NomeFornitore)
                    .HasColumnType("varchar(50)")
                    .HasColumnName("NOME_FORNITORE")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Note)
                    .IsRequired()
                    .HasColumnType("varchar(500)")
                    .HasColumnName("NOTE")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.PrezzoUnitario)
                    .HasPrecision(19, 3)
                    .HasColumnName("PREZZO_UNITARIO");

                entity.Property(e => e.Quantita).HasColumnName("QUANTITA");

                entity.Property(e => e.QuantitaMin).HasColumnName("QUANTITA_MIN");

                entity.Property(e => e.UnitaMisura)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("UNITA_MISURA")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<ViewManutenzioneMateriale>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_manutenzione_materiale");

                entity.Property(e => e.CodiceArticolo)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("CODICE_ARTICOLO")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.CodiceBarre)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasColumnName("CODICE_BARRE")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.IdArticolo)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("ID_ARTICOLO")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.IdIva)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_IVA");

                entity.Property(e => e.IdMagazzinoManutenzione)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_MAGAZZINO_MANUTENZIONE");

                entity.Property(e => e.IdManutenzione)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_MANUTENZIONE");

                entity.Property(e => e.Iva)
                    .HasColumnType("int(11)")
                    .HasColumnName("IVA");

                entity.Property(e => e.NomeArticolo)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("NOME_ARTICOLO")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.PrezzoUnitario)
                    .HasPrecision(19, 3)
                    .HasColumnName("PREZZO_UNITARIO");

                entity.Property(e => e.Quantita)
                    .HasColumnType("int(11)")
                    .HasColumnName("QUANTITA");
            });

            modelBuilder.Entity<ViewManutenzioneProgrammataStorico>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_manutenzione_programmata_storico");

                entity.Property(e => e.Allestimento)
                    .IsRequired()
                    .HasColumnType("varchar(250)")
                    .HasColumnName("ALLESTIMENTO")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.DataImm)
                    .HasMaxLength(6)
                    .HasColumnName("DATA_IMM");

                entity.Property(e => e.DtInserimento)
                    .HasColumnType("date")
                    .HasColumnName("DT_INSERIMENTO");

                entity.Property(e => e.DtPrevista)
                    .HasColumnType("date")
                    .HasColumnName("DT_PREVISTA");

                entity.Property(e => e.GiorniPreavviso)
                    .HasColumnType("int(11)")
                    .HasColumnName("GIORNI_PREAVVISO");

                entity.Property(e => e.IdManutenzioneProgrammata)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_MANUTENZIONE_PROGRAMMATA");

                entity.Property(e => e.IdMarca)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_MARCA");

                entity.Property(e => e.IdMezzo)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_MEZZO");

                entity.Property(e => e.IdTipoManutenzioneProgrammata)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_TIPO_MANUTENZIONE_PROGRAMMATA");

                entity.Property(e => e.KmDeltaProssimoIntervento)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("KM_DELTA_PROSSIMO_INTERVENTO");

                entity.Property(e => e.KmGiornalieri)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("KM_GIORNALIERI");

                entity.Property(e => e.KmPresunti)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("KM_PRESUNTI");

                entity.Property(e => e.KmReali)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("KM_REALI");

                entity.Property(e => e.ManutenzioneChiusa).HasColumnName("MANUTENZIONE_CHIUSA");

                entity.Property(e => e.Marca)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("MARCA")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Modello)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("MODELLO")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Targha)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("TARGHA")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.TipoManutenzioneProgrammata)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("TIPO_MANUTENZIONE_PROGRAMMATA")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<ViewManutenzioneProgrammata>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_manutenzione_programmata");

                entity.Property(e => e.Allestimento)
                    .IsRequired()
                    .HasColumnType("varchar(250)")
                    .HasColumnName("ALLESTIMENTO")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.DataImm)
                    .HasMaxLength(6)
                    .HasColumnName("DATA_IMM");

                entity.Property(e => e.DtInserimento)
                    .HasColumnType("date")
                    .HasColumnName("DT_INSERIMENTO");

                entity.Property(e => e.DtPrevista)
                    .HasColumnType("date")
                    .HasColumnName("DT_PREVISTA");

                entity.Property(e => e.GiorniPreavviso)
                    .HasColumnType("int(11)")
                    .HasColumnName("GIORNI_PREAVVISO");

                entity.Property(e => e.IdManutenzioneProgrammata)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_MANUTENZIONE_PROGRAMMATA");

                entity.Property(e => e.IdMarca)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_MARCA");

                entity.Property(e => e.IdMezzo)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_MEZZO");

                entity.Property(e => e.IdTipoManutenzioneProgrammata)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_TIPO_MANUTENZIONE_PROGRAMMATA");

                entity.Property(e => e.KmDeltaProssimoIntervento)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("KM_DELTA_PROSSIMO_INTERVENTO");

                entity.Property(e => e.KmGiornalieri)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("KM_GIORNALIERI");

                entity.Property(e => e.KmPresunti)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("KM_PRESUNTI");

                entity.Property(e => e.KmReali)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("KM_REALI");

                entity.Property(e => e.ManutenzioneChiusa).HasColumnName("MANUTENZIONE_CHIUSA");

                entity.Property(e => e.Marca)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("MARCA")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Modello)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("MODELLO")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Targha)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("TARGHA")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.TipoManutenzioneProgrammata)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("TIPO_MANUTENZIONE_PROGRAMMATA")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<ViewManutenzioni>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_manutenzioni");

                entity.Property(e => e.Allestimento)
                    .IsRequired()
                    .HasColumnType("varchar(250)")
                    .HasColumnName("ALLESTIMENTO")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Cv)
                    .HasColumnType("int(11)")
                    .HasColumnName("CV");

                entity.Property(e => e.DataImm)
                    .HasMaxLength(6)
                    .HasColumnName("DATA_IMM");

                entity.Property(e => e.Descrizione)
                    .IsRequired()
                    .HasColumnType("longtext")
                    .HasColumnName("DESCRIZIONE")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.DtFine)
                    .HasColumnType("date")
                    .HasColumnName("DT_FINE");

                entity.Property(e => e.DtInizio)
                    .HasColumnType("date")
                    .HasColumnName("DT_INIZIO");

                entity.Property(e => e.IdManutenzione)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_MANUTENZIONE");

                entity.Property(e => e.IdManutenzioneTipo)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_MANUTENZIONE_TIPO");

                entity.Property(e => e.IdMarca)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_MARCA");

                entity.Property(e => e.IdMezzo)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_MEZZO");

                entity.Property(e => e.IdTipoMezzo)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_TIPO_MEZZO");

                entity.Property(e => e.IdUmUsura)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_UM_USURA");

                entity.Property(e => e.IdUnitaMisura)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_UNITA_MISURA");

                entity.Property(e => e.Modello)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("MODELLO")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Note)
                    .IsRequired()
                    .HasColumnType("longtext")
                    .HasColumnName("NOTE")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.NoteMezzi)
                    .IsRequired()
                    .HasColumnType("varchar(500)")
                    .HasColumnName("NOTE_MEZZI")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.PrezzoTot)
                    .HasPrecision(19, 4)
                    .HasColumnName("PREZZO_TOT");

                entity.Property(e => e.Targha)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("TARGHA")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Telaio)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("TELAIO")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.TipoManutenzione)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("TIPO_MANUTENZIONE")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.UmUsura)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("UM_USURA")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.UmValore)
                    .HasColumnType("int(11)")
                    .HasColumnName("UM_VALORE");
            });

            modelBuilder.Entity<ViewMezzi>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_mezzi");

                entity.Property(e => e.Allestimento)
                    .IsRequired()
                    .HasColumnType("varchar(250)")
                    .HasColumnName("ALLESTIMENTO")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Cv)
                    .HasColumnType("int(11)")
                    .HasColumnName("CV");

                entity.Property(e => e.DataImm)
                    .HasMaxLength(6)
                    .HasColumnName("DATA_IMM");

                entity.Property(e => e.Marca)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("MARCA")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Modello)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("MODELLO")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Note)
                    .IsRequired()
                    .HasColumnType("varchar(500)")
                    .HasColumnName("NOTE")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Targha)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("TARGHA")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Telaio)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("TELAIO")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.TipoMezzo)
                    .HasColumnType("varchar(50)")
                    .HasColumnName("TIPO_MEZZO")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<ViewScarico>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_scarico");

                entity.Property(e => e.CodiceArticolo)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("CODICE_ARTICOLO")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.CognomeImpiegato)
                    .HasColumnType("varchar(30)")
                    .HasColumnName("COGNOME_IMPIEGATO")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.DtScarico)
                    .HasMaxLength(6)
                    .HasColumnName("DT_SCARICO");

                entity.Property(e => e.IdMagazzino)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_MAGAZZINO");

                entity.Property(e => e.IdScarico)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_SCARICO");

                entity.Property(e => e.IdUser)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_USER");

                entity.Property(e => e.NomeArticolo)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("NOME_ARTICOLO")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.NomeImpiegato)
                    .HasColumnType("varchar(30)")
                    .HasColumnName("NOME_IMPIEGATO")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Nota)
                    .IsRequired()
                    .HasColumnType("varchar(1000)")
                    .HasColumnName("NOTA")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<VisualizzaLibretto>(entity =>
            {
                entity.HasKey(e => e.IdVisualizzaLibretto)
                    .HasName("PRIMARY");

                entity.ToTable("visualizza_libretto");

                entity.Property(e => e.IdVisualizzaLibretto)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("ID_VISUALIZZA_LIBRETTO");

                entity.Property(e => e.IdMezzo)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_MEZZO");

                entity.Property(e => e.Note)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasColumnName("NOTE")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasColumnType("longtext")
                    .HasColumnName("PATH")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
