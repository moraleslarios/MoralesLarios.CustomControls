namespace WpfSressTests
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EMIR_REP_VALUATIONS_HISTORICO_ARCHIVADAS
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(8)]
        public string Branch { get; set; }

        [StringLength(40)]
        public string MessageId { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime FechaArchivado { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime FechaHistoricoArchivado { get; set; }

        [Required]
        [StringLength(16)]
        public string TradeType { get; set; }

        [Required]
        [StringLength(80)]
        public string SentBy { get; set; }

        [Required]
        [StringLength(11)]
        public string SentTo { get; set; }

        [StringLength(25)]
        public string CreationTimestamp { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(52)]
        public string TradeId { get; set; }

        [Required]
        [StringLength(12)]
        public string PartyIdType { get; set; }

        [Required]
        [StringLength(200)]
        public string PartyId { get; set; }

        [StringLength(12)]
        public string ReportingEntityIdType { get; set; }

        [StringLength(50)]
        public string ReportingEntityId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal MarkToMarketValue { get; set; }

        [Required]
        [StringLength(3)]
        public string MarkToMarketCurrency { get; set; }

        [Column(TypeName = "date")]
        public DateTime ValuationDate { get; set; }

        [Required]
        [StringLength(14)]
        public string ValuationTime { get; set; }

        [Required]
        [StringLength(1)]
        public string ValuationType { get; set; }

        [Required]
        [StringLength(1)]
        public string Delegation { get; set; }

        [StringLength(12)]
        public string Deleg_PartyIdType { get; set; }

        [StringLength(200)]
        public string Deleg_PartyId { get; set; }

        [StringLength(12)]
        public string Deleg_ReportingEntityIdType { get; set; }

        [StringLength(50)]
        public string Deleg_ReportingEntityId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Deleg_MarkToMarketValue { get; set; }

        [StringLength(3)]
        public string Deleg_MarkToMarketCurrency { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Deleg_ValuationDate { get; set; }

        [StringLength(14)]
        public string Deleg_ValuationTime { get; set; }

        [StringLength(1)]
        public string Deleg_ValuationType { get; set; }

        [StringLength(10)]
        public string TipoEvento { get; set; }

        [StringLength(16)]
        public string Estado { get; set; }

        [Required]
        [StringLength(2)]
        public string Encriptada { get; set; }

        public DateTime? FechaAnalisis { get; set; }

        public DateTime? FechaReporting { get; set; }

        public DateTime? FechaRespuesta { get; set; }

        public bool? IsSelected { get; set; }

        [StringLength(3)]
        public string DeliverableCurrency2 { get; set; }

        [StringLength(52)]
        public string InterimTradeId { get; set; }

        [Required]
        [StringLength(128)]
        public string NombreFicheroKondor { get; set; }

        [Required]
        [StringLength(128)]
        public string NombreFicheroOpics { get; set; }

        [StringLength(3)]
        public string ReasonCode { get; set; }

        [StringLength(60)]
        public string ReasonDescription { get; set; }
    }
}
