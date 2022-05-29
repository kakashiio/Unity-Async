using System.Collections.Generic;

namespace IO.Unity3D.Source.Async
{
    //******************************************
    //  
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-27 00:24
    //******************************************
    public class TaskMonitor : ITask
    {
        public delegate void OnExecutingCallback(float progress);
        public delegate void OnExecutedCallback();
        
        public float Progress {
            get
            {
                float p = 0;
                foreach (var progress in _Progresses)
                {
                    p += progress;
                }
                return p / _Progresses.Count;
            }
        }

        public bool Finish {
            get
            {
                foreach (var finish in _Finishes)
                {
                    if (!finish)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        private List<ITask> _Tasks = new List<ITask>();
        private List<float> _Progresses = new List<float>();
        private List<bool> _Finishes = new List<bool>();

        private event OnExecutingCallback _OnExecuting;
        private event OnExecutedCallback _OnExecuted;

        public TaskMonitor Add<T>(ITaskWithResult<T> task)
        {
            task.ListenExecuting(_OnExecutingWithResult<T>(_Tasks.Count));
            task.ListenExecuted(_OnExecutedWithResult<T>(_Tasks.Count));
            _Progresses.Add(task.Progress);
            _Finishes.Add(task.Finish);
            _Tasks.Add(task);
            return this;
        }

        public TaskMonitor Add(ITaskWithoutResult task)
        {
            task.ListenExecuting(_OnExecutingWithoutResult(_Tasks.Count));
            task.ListenExecuted(_OnExecutedWithoutResult(_Tasks.Count));
            _Progresses.Add(task.Progress);
            _Finishes.Add(task.Finish);
            _Tasks.Add(task);
            return this;
        }

        private ITaskWithoutResult.OnExecutedCallback _OnExecutedWithoutResult(int index)
        {
            return () =>
            {
                _Finishes[index] = true;
                if (Finish)
                {
                    _OnExecuted?.Invoke();                
                }
            };
        }

        private ITaskWithoutResult.OnExecutingCallback _OnExecutingWithoutResult(int index)
        {
            return (progress) =>
            {
                _Progresses[index] = progress;
                if (_OnExecuting != null)
                {
                    _OnExecuting(Progress);
                }
            };
        }

        private ITaskWithResult<T>.OnExecutedCallback _OnExecutedWithResult<T>(int index)
        {
            return (obj) =>
            {
                _Finishes[index] = true;
                if (Finish)
                {
                    _OnExecuted?.Invoke();                
                }
            };
        }

        private ITaskWithResult<T>.OnExecutingCallback _OnExecutingWithResult<T>(int index)
        {
            return (progress) =>
            {
                _Progresses[index] = progress;
                if (_OnExecuting != null)
                {
                    _OnExecuting(Progress);
                }
            };
        }
        
        public TaskMonitor ListenExecuting(OnExecutingCallback onExecuting)
        {
            _OnExecuting += onExecuting;
            return this;
        }

        public TaskMonitor ListenExecuted(OnExecutedCallback onExecuted)
        {
            _OnExecuted += onExecuted;
            return this;
        }
    }
}