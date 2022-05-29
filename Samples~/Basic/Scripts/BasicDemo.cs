using UnityEngine;

namespace IO.Unity3D.Source.Async.Samples
{
    //******************************************
    //  
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-29 12:00
    //******************************************
    internal class BasicDemo : MonoBehaviour
    {
        private void Awake()
        {
            var coroutineManager = new CoroutineManager();
            HTTP.Init(coroutineManager);
            var resourceManager = new ResouceManager(coroutineManager);
            var someOverheadExecution = new SomeOverheadExecution(coroutineManager);

            var httpRequest1 = HTTP.Get("https://unity3d.io/").ListenExecuted((req)=>_Log($"Req#1 {req.result}"));
            var httpRequest2 = HTTP.Get("https://unity3d.io/").ListenExecuted((req)=>_Log($"Req#2 {req.result}"));

            var resourceLoader1 = resourceManager.Load<GameObject>("").ListenExecuted((go)=>_Log($"Loaded#1 {go}"));
            var resourceLoader2 = resourceManager.Load<GameObject>("").ListenExecuted((go)=>_Log($"Loaded#2 {go}"));

            var execution1 = someOverheadExecution.DoOverheadExecution().ListenExecuted(()=>_Log($"Execute#1"));
            var execution2 = someOverheadExecution.DoOverheadExecution().ListenExecuted(()=>_Log($"Execute#2"));

            new TaskMonitor()
                .Add(httpRequest1)
                .Add(httpRequest2)
                .Add(resourceLoader1)
                .Add(resourceLoader2)
                .Add(execution1)
                .Add(execution2)
                .ListenExecuted(() => _Log("Finish all"));
        }

        private void _Log(object msg)
        {
            Debug.LogError($"[{Time.time}] -- {msg}");
        }
    }
}