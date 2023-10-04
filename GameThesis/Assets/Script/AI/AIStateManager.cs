using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIStateManager : MonoBehaviour
{
    public abstract AIBaseState s_currentState { get; set; }

}
