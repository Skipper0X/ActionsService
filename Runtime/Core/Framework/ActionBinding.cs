using System;
namespace Runtime.Core.Framework
{
    public interface IActionBinding
    {
        /// <summary>
        /// <see cref="IAction"/>'s Instance....
        /// </summary>
        IAction Action { get; }
        /// <summary>
        /// Dispatch This <see cref="IAction"/>
        /// </summary>
        void Dispatch();
        /// <summary>
        /// <see cref="Reset"/> This <see cref="ActionBinding{AType}"/> Cache.....
        /// </summary>
        void Reset();
    }
    /// <summary>
    /// Container Of Binding's Of <see cref="IAction"/>'s Types..
    /// </summary>
    /// <typeparam name="AType"><see cref="IAction"/>'s Type</typeparam>
    public class ActionBinding<AType> : IActionBinding where AType : IAction
    {
        private event Action<AType> _actionSet = default;
        /// <summary>
        /// Construct Object Of <see cref="ActionBinding{AType}"/>
        /// </summary>
        /// <param name="iAction"></param>
        public ActionBinding(AType iAction) => Action = iAction;
        /// <summary>
        /// <see cref="IAction"/>'s Instance....
        /// </summary>
        public IAction Action { get; private set; }
        /// <summary>
        /// Subscribe To The <see cref="IAction"/>'s <see cref="_actionSet"/>
        /// </summary>
        /// <param name="action">Callback Action...</param>
        public void Subscribe(Action<AType> action) => _actionSet += action;
        /// <summary>
        /// UnSubscribe To The <see cref="IAction"/>'s <see cref="_actionSet"/>
        /// </summary>
        /// <param name="action">Callback Action...</param>
        public void UnSubscribe(Action<AType> action) => _actionSet -= action;
        /// <summary>
        /// Dispatch This <see cref="IAction"/>
        /// </summary>
        public void Dispatch()
        {
            if (_actionSet != null) _actionSet((AType)Action);
        }
        /// <summary>
        /// <see cref="Reset"/> This <see cref="ActionBinding{AType}"/> Cache.....
        /// </summary>
        public void Reset() => _actionSet = null;
    }
}