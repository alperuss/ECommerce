using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Data.DTO
{
    public class Message_Response
    {
        public string Message { get; set; }
        public Data.Enum.MessageType MessageType { get; set; }
    }
}
