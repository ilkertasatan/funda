namespace Funda.Assignment.Domain
{
    public abstract class Entity<TEntityId> : IMaybeExist
    {
        public TEntityId Id { get; init; }
        
        public bool Exists()
        {
            return !Id.Equals(default(TEntityId));
        }
    }
}