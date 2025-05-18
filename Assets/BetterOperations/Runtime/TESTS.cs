using System.Collections.Generic;
using System.Threading.Tasks;
using Better.Operations.Runtime.Builders;
using Better.Operations.Runtime.Extensions;
using Better.Operations.Runtime.Instructions;
using Better.Operations.Runtime.Members;
using Better.Operations.Runtime.Permissions;
using UnityEngine;

namespace Better.Operations.Runtime.BetterOperations.Runtime
{
    public interface IOperationModifier : IOperationMember
    {
    }

    public interface IJumpOperationMember : IOperationModifier
    {
        public Task OnPreJumpAsync(Vector3 sourceValue, Vector3 modifiedValue);
        public Task OnPostJumpAsync();
    }

    public interface ICrouchOperationMember : IOperationModifier
    {
    }

    public class JumpModif : IJumpOperationMember
    {
        public Task OnPreJumpAsync(Vector3 sourceValue, Vector3 modifiedValue)
        {
            throw new System.NotImplementedException();
        }

        public Task OnPostJumpAsync()
        {
            throw new System.NotImplementedException();
        }
    }

    public class CrouchModif : ICrouchOperationMember
    {
    }

    public class TESTS : IOperationMemberRegistry<IOperationModifier>
    {
        private HashSet<IOperationModifier> _members;
        private ValueAsyncOperation<Vector3, IJumpOperationMember> _jumpOperation;
        public int MembersCount => _members.Count;

        public void Test()
        {
            _jumpOperation = ValueAsyncOperationBuilder<Vector3, IJumpOperationMember>.Create()
                .AppendNotification(member => member.OnPreJumpAsync)
                .AppendNotification(member => member.OnPostJumpAsync)
                .Build();
            
            // var jumpModif = new JumpModif();
            // var crouchModif = new CrouchModif();
            // var registry = (IOperationMemberRegistry<IOperationModifier>)this;
            // registry.Register(jumpModif);
            // registry.Register(crouchModif);
        }

        private void OnJumpStarted()
        {
        }

        private void OnJumpCompleted()
        {
        }

        public void DoJump()
        {
            // _jumpOperation.ExecuteAsync(default, default);

            var maxAllowPermission = PermissionFlag.Create(PermissionValues.MaxAllow);
            var minAllowPermission = PermissionFlag.Create(PermissionValues.MinAllow);

            var isSome = maxAllowPermission > minAllowPermission;
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