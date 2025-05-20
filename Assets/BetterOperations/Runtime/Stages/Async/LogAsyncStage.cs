using System.Threading.Tasks;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;
using UnityEngine;

namespace Better.Operations.Runtime.Stages
{
    public class LogAsyncStage<TBuffer, TMember> : AsyncStage<TBuffer, TMember>
        where TBuffer : AsyncBuffer<TMember>
        where TMember : IOperationMember
    {
        protected LogType LogType { get; }
        protected string OriginMessage { get; }

        public LogAsyncStage(string message, LogType logType)
        {
            OriginMessage = message;
            LogType = logType;
        }

        public override Task<bool> TryExecuteAsync(TBuffer buffer)
        {
            var message = $"{OriginMessage}\n{nameof(buffer)}:{buffer}";
            Debug.unityLogger.Log(LogType, message);

            return Task.FromResult(true);
        }
    }
}