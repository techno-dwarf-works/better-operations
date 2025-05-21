using System.Text;
using Better.Commons.Runtime.Extensions;
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

        public sealed override void Execute(TBuffer buffer)
        {
            var message = GetMessage(buffer);
            Debug.unityLogger.Log(LogType, message);
        }

        protected virtual string GetMessage(TBuffer buffer)
        {
            var stringBuilder = new StringBuilder()
                .AppendLine($"{OriginMessage};")
                .AppendFieldLine(nameof(OperationBuffer), buffer);

            return stringBuilder.ToString();
        }
    }
}