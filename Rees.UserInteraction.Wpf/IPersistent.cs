namespace Rees.Wpf
{
    /// <summary>
    ///     A class used to encapsulate any model that needs to be persistented.  It gives the consumer a opportunity to store
    ///     any metadata about the persisted model as well as top level versioning.
    /// </summary>
    public interface IPersistent
    {
        /// <summary>
        /// Gets or sets the model to persist to permentant storage.
        /// </summary>
        object Model { get; set; }

        /// <summary>
        /// Gets the sequence number for this implementation. This is used to broadcast more crucial higher priority persistent data out first, if any.
        /// </summary>
        int Sequence { get; }


        /// <summary>
        /// Adapts the model into the given type. This is a convience method to get the model and cast it in one tidy call.
        /// </summary>
        /// <typeparam name="T">The type to attempt to cast to.</typeparam>
        T AdaptModel<T>();
    }
}