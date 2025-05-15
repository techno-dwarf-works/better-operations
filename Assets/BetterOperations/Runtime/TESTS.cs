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
            var crouchModif = new CrouchModif();
            var registry = (IOperationMemberRegistry<IOperationModifier>)this;
            registry.Register(jumpModif);
            registry.Register(crouchModif);
        }

        bool IOperationMemberRegistry<IOperationModifier>.Register(IOperationModifier member)
        {
            _jumpOperation.TryRegister(member);
            return _members.Add(member);
        }

        bool IOperationMemberRegistry<IOperationModifier>.IsRegistered(IOperationModifier member)
        {
            return _members.Contains(member);
        }

        bool IOperationMemberRegistry<IOperationModifier>.Unregister(IOperationModifier member)
        {
            _jumpOperation.Unregister(member);
            return _members.Remove(member);
        }
    }
}