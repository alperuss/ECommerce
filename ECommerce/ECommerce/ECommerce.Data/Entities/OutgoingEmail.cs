﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Data.Entities
{
    public class OutgoingEmail:Abstracts.Entity
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public int TryCount { get; set; }
        public Enums.OutgoingEmailState OutgoingEmailStateId { get; set; }
    }
}
