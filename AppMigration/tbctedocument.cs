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
    
    public partial class tbctedocument
    {
        public long idcte { get; set; }
        public byte[] additionalData { get; set; }
        public byte[] documentData { get; set; }
        public byte[] documentCTe { get; set; }
        public Nullable<System.DateTime> interfaceSendingDate { get; set; }
        public byte[] documentCTeCanc { get; set; }
        public Nullable<System.DateTime> mastersafSendingDate { get; set; }
    
        public virtual tbcte tbcte { get; set; }
    }
}
