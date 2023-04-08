using System;

public class GameEvent<T>
{
    private event Action<T> Event;

    public void Register(Action<T> method)
    {
        Event += method;
    }
    
    public void UnRegister(Action<T> method)
    {
        Event -= method;
    }

    public void Raise(T param)
    {
        Event?.Invoke(param);
    }
}
