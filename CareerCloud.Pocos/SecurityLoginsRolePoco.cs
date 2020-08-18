using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CareerCloud.Pocos
{
    [Table("Security_Logins_Roles")]
    public class SecurityLoginsRolePoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Login { get; set; }
        public Guid Role { get; set; }

        [Column("Time_Stamp")]
        [Timestamp]
        public Byte[] TimeStamp { get; set; }

        [ForeignKey("Id")]
        public virtual SecurityLoginPoco SecurityLogin { get; set; }

        [ForeignKey("Id")]
        public virtual SecurityRolePoco SecurityRole { get; set; }

        //[ForeignKey("Code")]
        //public virtual SystemCountryCodePoco SystemCountryCode { get; set; }
    }
}
