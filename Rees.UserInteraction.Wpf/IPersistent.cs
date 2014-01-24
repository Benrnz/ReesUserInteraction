namespace Rees.Wpf
{
    /// <summary>
    /// A class used to encapsulate any model that needs to be persistented.  It gives the consumer a opportunity to store any metadata about the persisted model as well as top level versioning.
    /// </summary>
    public interface IPersistent
    {
        object Model { get; set; }

        T AdaptModel<T>();
    }
}