using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Stages
{
    public class ValueNotificationAsyncStage<TBuffer, TValue, TMember> : AsyncStage<TBuffer, TMember>
        where TBuffer : ValueAsyncBuffer<TValue, TMember>
        where TValue : struct
        where TMember : IOperationMember
    {
        private HashSet<OnNotificationAsync> _notifications;
        private HashSet<OnTokenableNotificationAsync> _tokenableNotifications;
        private HashSet<GetNotification> _notificationGetters;
        private HashSet<GetTokenableNotification> _tokenableNotificationGetters;
        private HashSet<GetNotificationBy> _memberNotificationGetters;
        private HashSet<GetTokenableNotificationBy> _memberTokenableNotificationGetters;

        public delegate Task OnNotificationAsync(TValue sourceValue, TValue modifiedValue);

        public delegate Task OnTokenableNotificationAsync(TValue sourceValue, TValue modifiedValue, CancellationToken cancellationToken);

        public delegate OnNotificationAsync GetNotification();

        public delegate OnTokenableNotificationAsync GetTokenableNotification();

        public delegate OnNotificationAsync GetNotificationBy(TMember member);

        public delegate OnTokenableNotificationAsync GetTokenableNotificationBy(TMember member);

        public ValueNotificationAsyncStage()
        {
            _notifications = new();
            _tokenableNotifications = new();

            _notificationGetters = new();
            _tokenableNotificationGetters = new();
            _memberNotificationGetters = new();
            _memberTokenableNotificationGetters = new();
        }

        public void Register(OnNotificationAsync notification) => _notifications.Add(notification);
        public void Register(OnTokenableNotificationAsync notification) => _tokenableNotifications.Add(notification);
        public void Register(GetNotification getter) => _notificationGetters.Add(getter);
        public void Register(GetTokenableNotification getter) => _tokenableNotificationGetters.Add(getter);
        public void Register(GetNotificationBy getter) => _memberNotificationGetters.Add(getter);
        public void Register(GetTokenableNotificationBy getter) => _memberTokenableNotificationGetters.Add(getter);

        public override async Task<TBuffer> ExecuteAsync(TBuffer buffer)
        {
            var subTasks = new List<Task>(6);

            var notificationsTask = ExecuteNotificationsAsync(buffer);
            subTasks.Add(notificationsTask);

            var tokenableNotificationsTask = ExecuteTokenableNotificationsAsync(buffer);
            subTasks.Add(tokenableNotificationsTask);

            var notificationsByTask = ExecuteNotificationsByAsync(buffer);
            subTasks.Add(notificationsByTask);

            var tokenableNotificationsByTask = ExecuteTokenableNotificationsByAsync(buffer);
            subTasks.Add(tokenableNotificationsByTask);

            var memberNotificationsByTask = ExecuteMemberNotificationsByAsync(buffer);
            subTasks.Add(memberNotificationsByTask);

            var memberTokenableNotificationsByTask = ExecuteMemberTokenableNotificationsByAsync(buffer);
            subTasks.Add(memberTokenableNotificationsByTask);

            await subTasks.WhenAll();
            return buffer;
        }

        private Task ExecuteNotificationsAsync(TBuffer buffer)
        {
            var task = _notifications.Select(notification => notification.Invoke(buffer.SourceValue, buffer.ModifiedValue)).WhenAll();
            return task;
        }

        private Task ExecuteTokenableNotificationsAsync(TBuffer buffer)
        {
            var task = _tokenableNotifications.Select(notification =>
                    notification.Invoke(buffer.SourceValue, buffer.ModifiedValue, buffer.CancellationToken))
                .WhenAll();
            return task;
        }

        private Task ExecuteNotificationsByAsync(TBuffer buffer)
        {
            var task = _notificationGetters.Select(getter => getter.Invoke())
                .Select(notification => notification.Invoke(buffer.SourceValue, buffer.ModifiedValue))
                .WhenAll();

            return task;
        }

        private Task ExecuteTokenableNotificationsByAsync(TBuffer buffer)
        {
            var task = _tokenableNotificationGetters.Select(getter => getter.Invoke())
                .Select(notification => notification.Invoke(buffer.SourceValue, buffer.ModifiedValue, buffer.CancellationToken))
                .WhenAll();

            return task;
        }

        private Task ExecuteMemberNotificationsByAsync(TBuffer buffer)
        {
            var task = buffer.Members.SelectMany(member => _memberNotificationGetters
                    .Select(getter => getter.Invoke(member))
                    .Select(notification => notification.Invoke(buffer.SourceValue, buffer.ModifiedValue)))
                .WhenAll();

            return task;
        }

        private Task ExecuteMemberTokenableNotificationsByAsync(TBuffer buffer)
        {
            var task = buffer.Members.SelectMany(member => _memberTokenableNotificationGetters
                    .Select(getter => getter.Invoke(member))
                    .Select(notification => notification.Invoke(buffer.SourceValue, buffer.ModifiedValue, buffer.CancellationToken)))
                .WhenAll();

            return task;
        }
    }
}