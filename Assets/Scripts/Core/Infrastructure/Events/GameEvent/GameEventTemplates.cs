using System;

public class GameEvent
{
    private event Action Event;

    public void Register(Action method)
    {
        Event += method;
    }
    
    public void Unregister(Action method)
    {
        Event -= method;
    }

    public void Raise()
    {
        Event?.Invoke();
    }
}

public class GameEvent<T>
{
    private event Action<T> Event;

    public void Register(Action<T> method)
    {
        Event += method;
    }
    
    public void Unregister(Action<T> method)
    {
        Event -= method;
    }

    public void Raise(T param)
    {
        if (Event != null)
            Event?.Invoke(param);
    }
}

public class GameEvent<T1,T2>
{
    private event Action<T1,T2> Event;

    public void Register(Action<T1,T2> method)
    {
        Event += method;
    }
    
    public void UnRegister(Action<T1,T2> method)
    {
        Event -= method;
    }

    public void Raise(T1 param, T2 paramB)
    {
        Event?.Invoke(param,paramB);
    }
}

public class GameEvent<T1, T2, T3>
{
    private event Action<T1, T2, T3> Event;

    public void Register(Action<T1, T2, T3> method)
    {
        Event += method;
    }

    public void UnRegister(Action<T1, T2, T3> method)
    {
        Event -= method;
    }

    public void Raise(T1 param, T2 paramB, T3 paramC)
    {
        Event?.Invoke(param, paramB, paramC);
    }
}
