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
    
    public partial class tbmdfe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbmdfe()
        {
            this.tbmdfedetalhes = new HashSet<tbmdfedetalhe>();
        }
    
        public long idMdfe { get; set; }
        public string chaveMDFe { get; set; }
        public Nullable<long> protocolNumber { get; set; }
        public string processDate { get; set; }
        public string processMessage { get; set; }
        public Nullable<short> statusProcess { get; set; }
        public Nullable<short> statusMDFe { get; set; }
        public string origemManifesto { get; set; }
        public string destinoManifesto { get; set; }
        public string numVoo { get; set; }
        public string dataVoo { get; set; }
        public string txtDate { get; set; }
        public string serie { get; set; }
        public string counterid { get; set; }
        public string processHHMM { get; set; }
        public Nullable<System.DateTime> authorizationDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbmdfedetalhe> tbmdfedetalhes { get; set; }
        public virtual tbmdfedocument tbmdfedocument { get; set; }
        public virtual tbmdfeprocess tbmdfeprocess { get; set; }
        public virtual tbmdfeRetorno tbmdfeRetorno { get; set; }
    }
}
