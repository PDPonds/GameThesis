using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DebugCommandBase
{
    string _commandID;
    string _commandDiscription;
    string _commandFormat;

    public string commandID { get { return _commandID; } }
    public string commandFormat { get { return _commandFormat; } }
    public string commandDiscription { get { return _commandDiscription; } }

    public DebugCommandBase(string ID, string discription, string format)
    {
        _commandID = ID;
        _commandDiscription = discription;
        _commandFormat = format;
    }
}

public class DebugCommand : DebugCommandBase
{
    Action command;
    public DebugCommand(string id, string discription, string format, Action command) : base(id, discription, format)
    {
        this.command = command;
    }

    public void Invoke()
    {
        command.Invoke();
    }

}

public class DebugCommand<T1> : DebugCommandBase
{
    Action<T1> command;
    public DebugCommand(string id, string discription, string format, Action<T1> command) : base(id, discription, format)
    {
        this.command = command;
    }

    public void Invoke(T1 value)
    {
        command.Invoke(value);
    }
}
public class DebugCommand<T1, T2> : DebugCommandBase
{
    Action<T1, T2> command;
    public DebugCommand(string id, string discription, string format, Action<T1, T2> command) : base(id, discription, format)
    {
        this.command = command;
    }

    public void Invoke(T1 value1, T2 value2)
    {
        command.Invoke(value1, value2);
    }
}

