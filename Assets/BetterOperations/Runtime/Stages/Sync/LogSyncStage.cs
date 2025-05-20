using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Extensions;
using Better.Operations.Runtime.Members;
using Better.Operations.Runtime.Permissions;
using UnityEngine;

namespace Better.Operations.Runtime.Stages
{
    public class LogSyncStage<TBuffer, TMember> : SyncStage<TBuffer, TMember>
        where TBuffer : SyncBuffer<TMember>
        where TMember : IOperationMember
    {
        protected LogType LogType { get; }
        protected string OriginMessage { get; }

        public LogSyncStage(string message, LogType logType)
        {
            OriginMessage = message;
            LogType = logType;
        }

        public sealed override bool TryExecute(TBuffer buffer)
        {
            var message = $"{OriginMessage}\n{nameof(buffer)}:{buffer}";
            Debug.unityLogger.Log(LogType, message);

            return true;
        }
    }
}