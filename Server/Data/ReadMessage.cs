//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class ReadMessage
    {
        public int Id { get; set; }
        public int Message_Id { get; set; }
        public bool IsRead { get; set; }
        public Nullable<int> User_Id { get; set; }
    
        public virtual User User { get; set; }
        public virtual Message Message { get; set; }
    }
}
