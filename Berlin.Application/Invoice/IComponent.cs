﻿using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berlin.Application.Invoice
{
    public interface IComponent
    {
        void Compose(RowDescriptor row);
    }
}