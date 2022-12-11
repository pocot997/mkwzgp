using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ManagerInterface
{
    ManagerStatus status { get; }

    void Startup();
}
