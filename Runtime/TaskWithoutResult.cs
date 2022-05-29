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
    public class TaskWithoutResult : ITaskWithoutResult
    {
        public float Progress { get; private set; }
        public bool Finish { get; private set; }
        
        private event ITaskWithoutResult.OnExecutingCallback _OnExecuting;
        private event ITaskWithoutResult.OnExecutedCallback _OnExecuted;

        public void Executing(float progress)
        {
            Progress = progress;
            _OnExecuting?.Invoke(progress);
        }

        public void Executed()
        {
            Finish = true;
            if (!Mathf.Approximately(Progress, 1))
            {
                Executing(1);
            }
            _OnExecuted?.Invoke();
        }
        
        public ITaskWithoutResult ListenExecuting(ITaskWithoutResult.OnExecutingCallback onExecuting)
        {
            _OnExecuting += onExecuting;
            return this;
        }

        public ITaskWithoutResult ListenExecuted(ITaskWithoutResult.OnExecutedCallback onExecuted)
        {
            _OnExecuted += onExecuted;
            return this;
        }
    }
}