using System.Collections;
using UnityEngine;

namespace IO.Unity3D.Source.Async.Samples
{
    //******************************************
    //  
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-29 12:28
    //******************************************
    public class CoroutineManager
    {
        private CoroutineManagerMono _CoroutineManagerMono;
        
        public CoroutineManager()
        {
            _CoroutineManagerMono = new GameObject("CoroutineManager").AddComponent<CoroutineManagerMono>();
        }

        public void StartCoroutine(IEnumerator enumerator)
        {
            _CoroutineManagerMono.StartCoroutine(enumerator);
        }

        public ITaskWithoutResult StartCoroutineWithAsyncOperation(IEnumerator enumerator)
        {
            TaskWithoutResult task = new TaskWithoutResult();
            _CoroutineManagerMono.StartCoroutine(_Start(enumerator, task));
            return task;
        }

        private IEnumerator _Start(IEnumerator enumerator, TaskWithoutResult task)
        {
            yield return enumerator;
            task.Executed();
        }

        class CoroutineManagerMono : MonoBehaviour
        {
            private void Awake()
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}