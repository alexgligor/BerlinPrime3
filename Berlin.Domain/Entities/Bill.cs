using Berlin.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berlin.Domain.Entities
{
    public class Bill:BaseDbObject
    {
        //Use Title for Serie and Description for number
        public int ReceiptId { get; set; }

        public Receipt Receipt { get; set; }

    }
}
