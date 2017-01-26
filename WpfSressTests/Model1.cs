namespace WpfSressTests
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<EMIR_REP_VALUATIONS_HISTORICO_ARCHIVADAS> EMIR_REP_VALUATIONS_HISTORICO_ARCHIVADAS { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EMIR_REP_VALUATIONS_HISTORICO_ARCHIVADAS>()
                .Property(e => e.Branch)
                .IsUnicode(false);

            modelBuilder.Entity<EMIR_REP_VALUATIONS_HISTORICO_ARCHIVADAS>()
                .Property(e => e.MessageId)
                .IsUnicode(false);

            modelBuilder.Entity<EMIR_REP_VALUATIONS_HISTORICO_ARCHIVADAS>()
                .Property(e => e.TradeType)
                .IsUnicode(false);

            modelBuilder.Entity<EMIR_REP_VALUATIONS_HISTORICO_ARCHIVADAS>()
                .Property(e => e.SentBy)
                .IsUnicode(false);

            modelBuilder.Entity<EMIR_REP_VALUATIONS_HISTORICO_ARCHIVADAS>()
                .Property(e => e.SentTo)
                .IsUnicode(false);

            modelBuilder.Entity<EMIR_REP_VALUATIONS_HISTORICO_ARCHIVADAS>()
                .Property(e => e.CreationTimestamp)
                .IsUnicode(false);

            modelBuilder.Entity<EMIR_REP_VALUATIONS_HISTORICO_ARCHIVADAS>()
                .Property(e => e.TradeId)
                .IsUnicode(false);

            modelBuilder.Entity<EMIR_REP_VALUATIONS_HISTORICO_ARCHIVADAS>()
                .Property(e => e.PartyIdType)
                .IsUnicode(false);

            modelBuilder.Entity<EMIR_REP_VALUATIONS_HISTORICO_ARCHIVADAS>()
                .Property(e => e.PartyId)
                .IsUnicode(false);

            modelBuilder.Entity<EMIR_REP_VALUATIONS_HISTORICO_ARCHIVADAS>()
                .Property(e => e.ReportingEntityIdType)
                .IsUnicode(false);

            modelBuilder.Entity<EMIR_REP_VALUATIONS_HISTORICO_ARCHIVADAS>()
                .Property(e => e.ReportingEntityId)
                .IsUnicode(false);

            modelBuilder.Entity<EMIR_REP_VALUATIONS_HISTORICO_ARCHIVADAS>()
                .Property(e => e.MarkToMarketValue)
                .HasPrecision(24, 6);

            modelBuilder.Entity<EMIR_REP_VALUATIONS_HISTORICO_ARCHIVADAS>()
                .Property(e => e.MarkToMarketCurrency)
                .IsUnicode(false);

            modelBuilder.Entity<EMIR_REP_VALUATIONS_HISTORICO_ARCHIVADAS>()
                .Property(e => e.ValuationTime)
                .IsUnicode(false);

            modelBuilder.Entity<EMIR_REP_VALUATIONS_HISTORICO_ARCHIVADAS>()
                .Property(e => e.ValuationType)
                .IsUnicode(false);

            modelBuilder.Entity<EMIR_REP_VALUATIONS_HISTORICO_ARCHIVADAS>()
                .Property(e => e.Delegation)
                .IsUnicode(false);

            modelBuilder.Entity<EMIR_REP_VALUATIONS_HISTORICO_ARCHIVADAS>()
                .Property(e => e.Deleg_PartyIdType)
                .IsUnicode(false);

            modelBuilder.Entity<EMIR_REP_VALUATIONS_HISTORICO_ARCHIVADAS>()
                .Property(e => e.Deleg_PartyId)
                .IsUnicode(false);

            modelBuilder.Entity<EMIR_REP_VALUATIONS_HISTORICO_ARCHIVADAS>()
                .Property(e => e.Deleg_ReportingEntityIdType)
                .IsUnicode(false);

            modelBuilder.Entity<EMIR_REP_VALUATIONS_HISTORICO_ARCHIVADAS>()
                .Property(e => e.Deleg_ReportingEntityId)
                .IsUnicode(false);

            modelBuilder.Entity<EMIR_REP_VALUATIONS_HISTORICO_ARCHIVADAS>()
                .Property(e => e.Deleg_MarkToMarketValue)
                .HasPrecision(20, 5);

            modelBuilder.Entity<EMIR_REP_VALUATIONS_HISTORICO_ARCHIVADAS>()
                .Property(e => e.Deleg_MarkToMarketCurrency)
                .IsUnicode(false);

            modelBuilder.Entity<EMIR_REP_VALUATIONS_HISTORICO_ARCHIVADAS>()
                .Property(e => e.Deleg_ValuationTime)
                .IsUnicode(false);

            modelBuilder.Entity<EMIR_REP_VALUATIONS_HISTORICO_ARCHIVADAS>()
                .Property(e => e.Deleg_ValuationType)
                .IsUnicode(false);

            modelBuilder.Entity<EMIR_REP_VALUATIONS_HISTORICO_ARCHIVADAS>()
                .Property(e => e.TipoEvento)
                .IsUnicode(false);

            modelBuilder.Entity<EMIR_REP_VALUATIONS_HISTORICO_ARCHIVADAS>()
                .Property(e => e.Estado)
                .IsUnicode(false);

            modelBuilder.Entity<EMIR_REP_VALUATIONS_HISTORICO_ARCHIVADAS>()
                .Property(e => e.Encriptada)
                .IsUnicode(false);

            modelBuilder.Entity<EMIR_REP_VALUATIONS_HISTORICO_ARCHIVADAS>()
                .Property(e => e.DeliverableCurrency2)
                .IsUnicode(false);

            modelBuilder.Entity<EMIR_REP_VALUATIONS_HISTORICO_ARCHIVADAS>()
                .Property(e => e.InterimTradeId)
                .IsUnicode(false);

            modelBuilder.Entity<EMIR_REP_VALUATIONS_HISTORICO_ARCHIVADAS>()
                .Property(e => e.NombreFicheroKondor)
                .IsUnicode(false);

            modelBuilder.Entity<EMIR_REP_VALUATIONS_HISTORICO_ARCHIVADAS>()
                .Property(e => e.NombreFicheroOpics)
                .IsUnicode(false);

            modelBuilder.Entity<EMIR_REP_VALUATIONS_HISTORICO_ARCHIVADAS>()
                .Property(e => e.ReasonCode)
                .IsUnicode(false);

            modelBuilder.Entity<EMIR_REP_VALUATIONS_HISTORICO_ARCHIVADAS>()
                .Property(e => e.ReasonDescription)
                .IsUnicode(false);
        }
    }
}
