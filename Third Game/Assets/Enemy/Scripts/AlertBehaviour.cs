using UnityEngine;
using System.Collections;

public class AlertBehaviour : MonoBehaviour
{
    public bool foundPlayer { get; private set; }

    private void Awake()
    {
        foundPlayer = false;
    }


}
