using UnityEngine;

namespace IO.Unity3D.Source.Async
{
    //******************************************
    //  
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-27 19:42
    //******************************************
    public class TaskWithResult<T> : ITaskWithResult<T>
    {
        public float Progress { get; private set; }
        public bool Finish { get; private set; }
        
        private event ITaskWithResult<T>.OnExecutingCallback _OnExecuting;
        private event ITaskWithResult<T>.OnExecutedCallback _OnExecuted;

        public void Executing(float progress)
        {
            Progress = progress;
            _OnExecuting?.Invoke(progress);
        }

        public void Executed(T obj)
        {
            Finish = true;
            if (!Mathf.Approximately(Progress, 1))
            {
                Executing(1);
            }
            _OnExecuted?.Invoke(obj);
        }
        
        public ITaskWithResult<T> ListenExecuting(ITaskWithResult<T>.OnExecutingCallback onExecuting) 
        {
            _OnExecuting += onExecuting;
            return this;
        }
        
        public ITaskWithResult<T> ListenExecuted(ITaskWithResult<T>.OnExecutedCallback onExecuted) 
        {
            _OnExecuted += onExecuted;
            return this;
        }
    }
}