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
    
    public partial class tbctereceptionhistory
    {
        public long idReception { get; set; }
        public string clientId { get; set; }
        public string @ref { get; set; }
        public string awb { get; set; }
        public string serie { get; set; }
        public string counterid { get; set; }
        public string tipo { get; set; }
        public byte[] headerData { get; set; }
        public byte[] documentData { get; set; }
        public string processDate { get; set; }
        public string processHHMM { get; set; }
    }
}
