namespace IO.Unity3D.Source.Async
{
    //******************************************
    //  
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-27 00:22
    //******************************************
    public interface ITask
    {
        float Progress { get; }
        bool Finish { get; }
    }
}