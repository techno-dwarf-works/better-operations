using System.Collections.Generic;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Stages
{
    public class ValueNotificationSyncStage<TBuffer, TValue, TMember> : AllowableSyncStage<TBuffer, TMember>
        where TBuffer : ValueSyncBuffer<TValue, TMember>
        where TValue : struct
        where TMember : IOperationMember
    {
        private OnNotification _notification;
        private HashSet<GetNotificationBy> _memberNotificationGetters;

        public delegate void OnNotification(TValue sourceValue, TValue modifiedValue);

        public delegate OnNotification GetNotificationBy(TMember member);

        public ValueNotificationSyncStage()
        {
            _memberNotificationGetters = new();
        }

        public void Register(OnNotification notification) => _notification += notification;
        public void Register(GetNotificationBy getter) => _memberNotificationGetters.Add(getter);

        protected override void Execute(TBuffer buffer)
        {
            ExecuteNotification(buffer);
            ExecuteMembersNotification(buffer);
        }

        private void ExecuteNotification(TBuffer buffer)
        {
            _notification?.Invoke(buffer.SourceValue, buffer.ModifiedValue);
        }

        private void ExecuteMembersNotification(TBuffer buffer)
        {
            if (_memberNotificationGetters == null)
            {
                return;
            }

            foreach (var memberNotificationGetter in _memberNotificationGetters)
            {
                foreach (var member in buffer.Members)
                {
                    var notification = memberNotificationGetter.Invoke(member);
                    notification.Invoke(buffer.SourceValue, buffer.ModifiedValue);
                }
            }
        }
    }
}