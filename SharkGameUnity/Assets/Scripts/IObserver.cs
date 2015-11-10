using UnityEngine;
using System.Collections;

public interface IObserver
{
    void ObserverUpdate(System.Object sender, System.Object message);
}

