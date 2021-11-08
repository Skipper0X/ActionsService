using Runtime.Core.Implementation;
using UnityEngine;

namespace Runtime.Demo
{
    public class ActionDemo : MonoBehaviour
    {
        private void OnEnable()
        {
            ActionService.Subscribe<DemoAction>(OnDemoAction);
            ActionService.Subscribe<DemoStateAction>(OnDemoStateAction);
        }

        private void OnDisable()
        {
            ActionService.UnSubscribe<DemoAction>(OnDemoAction);
            ActionService.UnSubscribe<DemoStateAction>(OnDemoStateAction);
        }

        private void OnDestroy()
        {
            ActionService.Reset();
        }

        private void Start()
        {
            var demoStateAction = ActionService.Get<DemoStateAction>();
            demoStateAction.Msg = "--> DemoStateAction :: Blah Blah Blah";
            ActionService.Dispatch<DemoStateAction>();

            var demoAction = ActionService.Get<DemoAction>();
            demoAction.Msg = "--> DemoAction :: Blah Blah Blah";
            ActionService.Dispatch<DemoAction>();
        }

        private void OnDemoStateAction(DemoStateAction demoStateAction)
        {
            Debug.Log(demoStateAction.Msg);
        }

        private void OnDemoAction(DemoAction demoAction)
        {
            Debug.Log(demoAction.Msg);
        }
    }
}