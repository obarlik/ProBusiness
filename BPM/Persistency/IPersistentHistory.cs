using System;
using System.Collections.Generic;
using System.Text;

namespace BPM.Persistency
{
    public enum PersistentAction
    {
        Create,
        Update,
        Delete,
        Run
    }


    public interface IPersistentHistory
    {
        Guid HistoryId { get; set; }
        Guid Oid { get; set; }
        Guid UserId { get; set; }
        DateTime Time { get; set; }
        PersistentAction Action { get; set; }
        string Details { get; set; }
    }
}
