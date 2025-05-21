namespace Better.Operations.Runtime.Members
{
    public interface IOperationMemberRegistry<TMember>
        where TMember : IOperationMember
    {
        public int MembersCount { get; }

        public bool Register(TMember member);
        public bool IsRegistered(TMember member);
        public bool Unregister(TMember member);
    }
}