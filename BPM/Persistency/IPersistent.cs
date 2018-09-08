using System;
using System.Collections.Generic;
using System.Text;

namespace BPM.Persistency
{
    public interface IPersistent
    {
        Guid Oid { get; set; }
        Guid? UpdateUserId { get; set; }
        DateTime UpdateTime { get; set; }

        void AfterConstruction();
    }
}
