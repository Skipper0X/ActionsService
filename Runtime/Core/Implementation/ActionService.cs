using System;
using Runtime.Core.Framework;
namespace Runtime.Core.Implementation
{
    /// <summary>
    /// Global Actions Service & HUB for All The In_App <see cref="IAction"/>
    /// </summary>
    public static class ActionService
    {
        private static readonly ActionHUB m_HUB = new ActionHUB();
        /// <summary>
        /// Get <see cref="IAction"/>'s Instance Of Given Type...
        /// </summary>
        /// <typeparam name="AType"><see cref="IAction"/>'s Type.</typeparam>
        /// <returns><see cref="IAction"/>'s Instance.</returns>
        public static AType Get<AType>() where AType : IAction, new() => m_HUB.Get<AType>();
        /// <summary>
        /// Bind & Subscribe The Connections With Given <see cref="IAction"/>
        /// </summary>
        /// <param name="callback">Callback For This <see cref="IAction"/></param>
        /// <typeparam name="AType"><see cref="IAction"/>'s Type</typeparam>
        public static void Subscribe<AType>(Action<AType> callback) where AType : IAction, new() => m_HUB.Subscribe<AType>(callback);
        /// <summary>
        /// UnSubscribe The Connections With Given <see cref="IAction"/>
        /// </summary>
        /// <param name="callback">Callback For This <see cref="IAction"/></param>
        /// <typeparam name="AType"><see cref="IAction"/>'s Type</typeparam>
        public static void UnSubscribe<AType>(Action<AType> callback) where AType : IAction => m_HUB.UnSubscribe<AType>(callback);
        /// <summary>
        /// UnBind <see cref="IAction"/>'s Type Of Connection In HUB....
        /// </summary>
        /// <typeparam name="AType"><see cref="IAction"/>'s Type</typeparam>
        /// <returns>Returns True if <see cref="IActionBinding"/> Exists & CanRemove...</returns>
        public static bool UnBind<AType>() where AType : IAction => m_HUB.UnBind<AType>();
        /// <summary>
        /// Dispatch <see cref="IAction"/>'s Connection...
        /// </summary>
        /// <typeparam name="AType"><see cref="IAction"/>'s Type</typeparam>
        public static void Dispatch<AType>() where AType : IAction => m_HUB.Dispatch<AType>();
        /// <summary>
        /// Reset's The <see cref="ActionHUB"/> & Clear All Of The Produce Cache....
        /// </summary>
        public static void Reset() => m_HUB.Reset();
    }
}