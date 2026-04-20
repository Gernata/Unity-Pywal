using System;

namespace UnityPywal.Editor
{
    internal sealed class PywalReloadScheduler
    {
        private readonly TimeSpan debounce;

        private DateTime scheduledUtc;
        private bool pending;
        private string pendingReason = string.Empty;

        public PywalReloadScheduler(TimeSpan debounce)
        {
            this.debounce = debounce;
        }

        public bool IsPending => pending;
        public DateTime ScheduledUtc => scheduledUtc;

        public void Reset()
        {
            pending = false;
            pendingReason = string.Empty;
            scheduledUtc = DateTime.MinValue;
        }

        public void RequestReload(DateTime utcNow, string reason)
        {
            pending = true;
            pendingReason = reason ?? string.Empty;
            scheduledUtc = utcNow + debounce;
        }

        public bool TryConsume(DateTime utcNow, out string reason)
        {
            if (pending && utcNow >= scheduledUtc)
            {
                pending = false;
                reason = pendingReason;
                pendingReason = string.Empty;
                return true;
            }

            reason = string.Empty;
            return false;
        }
    }
}
