using Better.Operations.Runtime.Buffers;
using UnityEngine;

namespace Better.Operations.Runtime.Stages
{
    public class LogContractlessStage<TBuffer> : ContractlessStage<TBuffer>
        where TBuffer : OperationBuffer
    {
        protected LogType LogType { get; }
        protected string OriginMessage { get; }

        public LogContractlessStage(string message, LogType logType)
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