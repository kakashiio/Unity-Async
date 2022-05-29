using System.Collections;
using UnityEngine.Networking;

namespace IO.Unity3D.Source.Async.Samples
{
    //******************************************
    //  
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-29 12:04
    //******************************************
    public class HTTP
    {
        private static CoroutineManager _CoroutineManager;

        public static void Init(CoroutineManager coroutineManager)
        {
            _CoroutineManager = coroutineManager;
        }

        public static ITaskWithResult<UnityWebRequest> Get(string url)
        {
            var task = new TaskWithResult<UnityWebRequest>();
            _CoroutineManager.StartCoroutine(_Get(url, task));
            return task;
        }

        private static IEnumerator _Get(string url, TaskWithResult<UnityWebRequest> task)
        {
            var webRequest = new UnityWebRequest(url);
            yield return webRequest.SendWebRequest();
            task.Executed(webRequest);
        }
    }
}