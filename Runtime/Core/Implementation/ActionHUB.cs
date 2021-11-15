using System;
using System.Collections.Generic;
using Runtime.Core.Framework;
namespace Runtime.Core.Implementation
{
    public sealed class ActionHUB
    {
        private readonly Dictionary<Type, IActionBinding> m_Cache = new Dictionary<Type, IActionBinding>();
        /// <summary>
        /// Dispatch <see cref="IAction"/>'s Connection...
        /// </summary>
        /// <typeparam name="AType"><see cref="IAction"/>'s Type</typeparam>
        public void Dispatch<AType>() where AType : IAction
        {
            if (m_Cache.TryGetValue(typeof(AType), out var actionBinding) == false) return;
            if (actionBinding.Action is IStateAction stateAction) stateAction.Apply();
            actionBinding.Dispatch();
            actionBinding.Action.OnReset();
        }
        /// <summary>
        /// Bind & Subscribe The Connections With Given <see cref="IAction"/>
        /// </summary>
        /// <param name="callback">Callback For This <see cref="IAction"/></param>
        /// <typeparam name="AType"><see cref="IAction"/>'s Type</typeparam>
        public void Subscribe<AType>(Action<AType> callback) where AType : IAction, new()
        {
            if (m_Cache.TryGetValue(typeof(AType), out var actionBinding) == false) actionBinding = Bind<AType>();
            if (actionBinding is ActionBinding<AType> typeBinding) typeBinding.Subscribe(callback);
        }
        /// <summary>
        /// UnSubscribe The Connections With Given <see cref="IAction"/>
        /// </summary>
        /// <param name="callback">Callback For This <see cref="IAction"/></param>
        /// <typeparam name="AType"><see cref="IAction"/>'s Type</typeparam>
        public void UnSubscribe<AType>(Action<AType> callback) where AType : IAction
        {
            if (m_Cache.TryGetValue(typeof(AType), out var actionBinding) == false) return;
            if (actionBinding is ActionBinding<AType> typeBinding) typeBinding.UnSubscribe(callback);
        }
        /// <summary>
        /// Get <see cref="IAction"/>'s Instance Of Given Type...
        /// </summary>
        /// <typeparam name="AType"><see cref="IAction"/>'s Type.</typeparam>
        /// <returns><see cref="IAction"/>'s Instance.</returns>
        public AType Get<AType>() where AType : IAction, new()
        {
            if (m_Cache.TryGetValue(typeof(AType), out var actionBinding) == false) actionBinding = Bind<AType>();
            actionBinding.Action.OnReset();
            return (AType)actionBinding.Action;
        }
        /// <summary>
        /// Reset's The <see cref="ActionHUB"/> & Clear All Of The Produce Cache....
        /// </summary>
        public void Reset()
        {
            foreach (var _item in m_Cache) _item.Value.Reset();
            m_Cache.Clear();
        }
        /// <summary>
        /// UnBind <see cref="IAction"/>'s Type Of Connection In HUB....
        /// </summary>
        /// <typeparam name="AType"><see cref="IAction"/>'s Type</typeparam>
        /// <returns>Returns True if <see cref="IActionBinding"/> Exists & CanRemove...</returns>
        public bool UnBind<AType>() where AType : IAction
        {
            var actionType = typeof(AType);
            if (m_Cache.ContainsKey(typeof(AType)) == false) return false;
            m_Cache[actionType].Reset();
            m_Cache.Remove(actionType);
            return true;
        }
        /// <summary>
        /// Bind <see cref="IAction"/>'s Type Of Connection In HUB....
        /// </summary>
        /// <typeparam name="AType"><see cref="IAction"/>'s Type</typeparam>
        /// <returns>Instance Of <see cref="IActionBinding"/></returns>
        public IActionBinding Bind<AType>() where AType : IAction, new()
        {
            var actionType = typeof(AType);
            if (m_Cache.ContainsKey(typeof(AType))) return m_Cache[actionType];
            var actionBinding = new ActionBinding<AType>(new AType());
            m_Cache.Add(actionType, actionBinding);
            return actionBinding;
        }
    }
}