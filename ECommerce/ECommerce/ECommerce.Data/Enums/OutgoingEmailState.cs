using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Data.Enums
{
    public enum OutgoingEmailState
    {
        Pending = 0,
        Sending = 1,
        Sent = 2,
        Fail = 3
    }
}
