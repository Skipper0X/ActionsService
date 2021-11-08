namespace Runtime.Core.Framework
{
    /// <summary>
    /// Base Interface For Every <see cref="IAction"/>'s Instance...
    /// </summary>
    public interface IAction
    {
        /// <summary>
        /// Called after this <see cref="IAction"/>'s Dispatch...
        /// </summary>
        void OnReset();
    }
    /// <summary>
    /// A Specialized <see cref="IAction"/> which Dispatch by Service & Apply Required Changes.....
    /// </summary>
    public interface IStateAction : IAction
    {
        /// <summary>
        /// Called before this <see cref="IAction"/>'s Dispatch by Service....
        /// </summary>
        void Apply();
    }
}