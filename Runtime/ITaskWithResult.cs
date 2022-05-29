namespace IO.Unity3D.Source.Async
{
    //******************************************
    //  
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-27 19:42
    //******************************************
    public interface ITaskWithResult<T> : ITask
    {
        public delegate void OnExecutingCallback(float progress);
        public delegate void OnExecutedCallback(T result);

        ITaskWithResult<T> ListenExecuting(OnExecutingCallback onExecuting);

        ITaskWithResult<T> ListenExecuted(OnExecutedCallback onExecuted);
    }
}