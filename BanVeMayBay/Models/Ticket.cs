//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BanVeMayBay.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ticket
    {
        public int Id { get; set; }
        public Nullable<int> TypeTicketId { get; set; }
        public Nullable<int> SeatId { get; set; }
        public Nullable<int> FlightId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime ModifyDate { get; set; }
    
        public virtual Flight Flight { get; set; }
        public virtual TypeTicket TypeTicket { get; set; }
    }
}
