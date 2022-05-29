namespace IO.Unity3D.Source.Async
{
    //******************************************
    //  
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-27 19:42
    //******************************************
    public interface ITaskWithoutResult : ITask
    {
        public delegate void OnExecutingCallback(float progress);
        public delegate void OnExecutedCallback();

        ITaskWithoutResult ListenExecuting(OnExecutingCallback onExecuting);

        ITaskWithoutResult ListenExecuted(OnExecutedCallback onExecuted);
    }
}