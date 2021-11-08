using Runtime.Core.Framework;
using UnityEngine;

namespace Runtime.Demo
{
    public class DemoAction : IAction
    {
        public string Msg = null;
        /// <summary>
        /// Called after this <see cref="IAction"/>'s Dispatch...
        /// </summary>
        public void OnReset() => Msg = null;
    }
    
    public class DemoStateAction : IStateAction
    {
        public string Msg = null;
        /// <summary>
        /// Called after this <see cref="IAction"/>'s Dispatch...
        /// </summary>
        public void OnReset() => Msg = null;
        /// <summary>
        /// Called before this <see cref="IAction"/>'s Dispatch by Service....
        /// </summary>
        public void Apply()
        {
            Debug.Log("--> I'm An IStateAction.... (It is used to apply & update any application state changes before dispatching.)");
        }
    }
}