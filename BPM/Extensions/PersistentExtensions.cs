using System;
using System.Collections.Generic;
using System.Text;

namespace BPM.Extensions
{
    public static class PersistentExtensions
    {
        public static void Initialize(this IPersistent persistent, Guid? currentUserId = null)
        {
            persistent.Oid = Guid.NewGuid();
            persistent.UpdateUserId = currentUserId;
            persistent.UpdateTime = DateTime.UtcNow;

            persistent.AfterConstruction();
        }
    }
}
