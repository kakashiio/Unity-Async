# Async Library in Source Framework for Unity3D

# Why
Define a common async interface for any operation such as resource loader, http request, async operation etc. 

It also provide a `TaskMonitor` to let you listen when all the tasks were finished in unified way.



## Without this library

You async code maybe looks like the following code:

```csharp
    // Without this library 
    
    // !!!
    // classes CoroutineManager、HTTP、ResouceManager、SomeOverheadExecution are classes just used for test the async library.
    // !!!
    
    var coroutineManager = new CoroutineManager();
    HTTP.Init(coroutineManager);
    var resourceManager = new ResouceManager(coroutineManager);
    var someOverheadExecution = new SomeOverheadExecution(coroutineManager);
    
    int finishedCount = 0;
    int totalCount = 6;           // We have 6 async operation here.
    
    Action onFinish = () => {
        finishedCount++;
        if(finishedCount == totalCount)
        {
            _Log("Finish all");
        }
    };
    
    HTTP.Get("https://unity3d.io/", (req)=>{
            _Log($"Req#1 {req.result}");
            onFinish();
    });
    
    HTTP.Get("https://unity3d.io/", (req)=>{
            _Log($"Req#2 {req.result}");
            onFinish();
    });
    
    resourceManager.Load<GameObject>("", (go)=>{
        _Log($"Loaded#1 {go}");
        onFinish();
    });
    
    resourceManager.Load<GameObject>("", (go)=>{
        _Log($"Loaded#2 {go}");
        onFinish();
    });
    
    someOverheadExecution.DoOverheadExecution(()=>{
        _Log($"Execute#1");
        onFinish();
    });
    
    someOverheadExecution.DoOverheadExecution(()=>{
        _Log($"Execute#2");
        onFinish();
    });
```

The code above is common in the project. But it is ease to make error and it is unnecessary. This library can make your life become easier.

## With this library

```csharp
    // With this library 
    
    // !!!
    // classes CoroutineManager、HTTP、ResouceManager、SomeOverheadExecution are classes just used for test the async library.
    // !!!
    
    var coroutineManager = new CoroutineManager();
    HTTP.Init(coroutineManager);
    var resourceManager = new ResouceManager(coroutineManager);
    var someOverheadExecution = new SomeOverheadExecution(coroutineManager);
    
    var httpRequest1 = HTTP.Get("https://unity3d.io/").ListenExecuted((req)=>_Log($"Req#1 {req.result}"));
    var httpRequest2 = HTTP.Get("https://unity3d.io/").ListenExecuted((req)=>_Log($"Req#2 {req.result}"));
    
    var resourceLoader1 = resourceManager.Load<GameObject>("").ListenExecuted((go)=>_Log($"Loaded#1 {go}"));
    var resourceLoader2 = resourceManager.Load<GameObject>("").ListenExecuted((go)=>_Log($"Loaded#2 {go}"));
    
    var execution1 = someOverheadExecution.DoOverheadExecution().ListenExecuted(()=>_Log($"Execute#1"));
    var execution2 = someOverheadExecution.DoOverheadExecution().ListenExecuted(()=>_Log($"Execute#2"));
    
    new TaskMonitor()
        .Add(httpRequest1)
        .Add(httpRequest2)
        .Add(resourceLoader1)
        .Add(resourceLoader2)
        .Add(execution1)
        .Add(execution2)
        .ListenExecuted(() => _Log("Finish all"));
```

That is all! You don't need to managed the count of different async task.

You can found the full sample code in `Samples~` directory. 

# How to use

## Add dependencies

You can add package from git url through the Package Manager.

All the following package should be added.

|Package|Description|
|--|--|
|[https://github.com/kakashiio/Unity-Async.git#1.0.0](https://github.com/kakashiio/Unity-Async.git#1.0.0)|Async Library|

# Tutorial

Some tutorial will be introduced here in someday. Because of this library is really so simple, I think you can understand it even without any tutorial.
