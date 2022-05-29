using System.Collections;
using UnityEngine;

namespace IO.Unity3D.Source.Async.Samples
{
    //******************************************
    //  
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-29 13:05
    //******************************************
    public class SomeOverheadExecution
    {
        private CoroutineManager _CoroutineManager;

        public SomeOverheadExecution(CoroutineManager coroutineManager)
        {
            _CoroutineManager = coroutineManager;
        }

        public ITaskWithoutResult DoOverheadExecution()
        {
            var task = new TaskWithoutResult();
            _CoroutineManager.StartCoroutine(_DoOverheadExecution(task));
            return task;
        }

        private IEnumerator _DoOverheadExecution(TaskWithoutResult task)
        {
            yield return new WaitForSeconds(3);
            task.Executed();
        }
    }
}