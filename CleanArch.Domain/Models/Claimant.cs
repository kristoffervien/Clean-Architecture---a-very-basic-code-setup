using CleanArch.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Domain.Models
{
    public class Claimant : BaseAuditableEntity
    {
        public string UserId { get; set; }
        public string Note { get; set; }
    }
}
