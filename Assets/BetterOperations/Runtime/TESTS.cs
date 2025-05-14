using System.Collections.Generic;
using Better.Operations.Runtime.Builders;
using Better.Operations.Runtime.Extensions;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.BetterOperations.Runtime
{
    public interface IOperationModifier : IOperationMember
    {
    }

    public interface IJumpOperationMember : IOperationModifier
    {
    }

    public interface ICrouchOperationMember : IOperationModifier
    {
    }

    public class JumpModif : IJumpOperationMember
    {
    }

    public class CrouchModif : ICrouchOperationMember
    {
    }

    public class TESTS : IOperationMemberRegistry<IOperationModifier>
    {
        private HashSet<IOperationModifier> _members;
        private SyncOperation<IJumpOperationMember> _jumpOperation;
        public int MembersCount => _members.Count;

        public void Test()
        {
            _jumpOperation = SyncOperationBuilder<IJumpOperationMember>.Create()
                .Build();

            var jumpModif = new JumpModif();
            Register(jumpModif);

            var crouchModif = new CrouchModif();
            Register(crouchModif);
        }

        public bool Register(IOperationModifier member)
        {
            _jumpOperation.Register(member);
            return _members.Add(member);
        }

        public bool IsRegistered(IOperationModifier member)
        {
            return _members.Contains(member);
        }

        public bool Unregister(IOperationModifier member)
        {
            _jumpOperation.Unregister(member);
            return _members.Remove(member);
        }
    }
}