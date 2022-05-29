using System.Collections;
using UnityEngine;

namespace IO.Unity3D.Source.Async.Samples
{
    //******************************************
    //  
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-29 12:50
    //******************************************
    public class ResouceManager
    {
        private CoroutineManager _CoroutineManager;

        public ResouceManager(CoroutineManager coroutineManager)
        {
            _CoroutineManager = coroutineManager;
        }

        public ITaskWithResult<T> Load<T>(string path) where T : UnityEngine.Object
        {
            var task = new TaskWithResult<T>();
            _CoroutineManager.StartCoroutine(_Start(path, task));
            return task;
        }

        private IEnumerator _Start<T>(string path, TaskWithResult<T> task) where T : Object
        {
            var request = Resources.LoadAsync<T>(path);
            yield return request;
            task.Executed(request.asset as T);
        }
    }
}