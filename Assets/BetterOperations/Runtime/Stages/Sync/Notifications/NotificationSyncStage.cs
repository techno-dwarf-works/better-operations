using System.Collections.Generic;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Stages
{
    public class NotificationSyncStage<TBuffer, TMember> : SyncStage<TBuffer, TMember>
        where TBuffer : SyncBuffer<TMember>
        where TMember : IOperationMember
    {
        private OnNotification _notification;
        private HashSet<GetNotificationBy> _memberNotificationGetters;

        public delegate void OnNotification();

        public delegate OnNotification GetNotificationBy(TMember member);

        public NotificationSyncStage()
        {
            _memberNotificationGetters = new();
        }

        public void Register(OnNotification notification) => _notification += notification;
        public void Register(GetNotificationBy getter) => _memberNotificationGetters.Add(getter);

        public override TBuffer Execute(TBuffer buffer)
        {
            ExecuteNotification(buffer);
            ExecuteMembersNotification(buffer);

            return buffer;
        }

        private void ExecuteNotification(TBuffer buffer)
        {
            _notification?.Invoke();
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
                    notification.Invoke();
                }
            }
        }
    }
}