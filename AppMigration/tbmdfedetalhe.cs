//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppMigration
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbmdfedetalhe
    {
        public string erpCode { get; set; }
        public string chaveCTe { get; set; }
        public Nullable<short> serie { get; set; }
        public Nullable<long> counterid { get; set; }
        public string origemManifesto { get; set; }
        public string destinoManifesto { get; set; }
        public string numVoo { get; set; }
        public string dataVoo { get; set; }
        public long idmdfe { get; set; }
        public Nullable<decimal> valorTotalAWB { get; set; }
        public Nullable<decimal> peso { get; set; }
        public Nullable<decimal> pecas { get; set; }
        public Nullable<decimal> pecasAWB { get; set; }
    
        public virtual tbmdfe tbmdfe { get; set; }
    }
}