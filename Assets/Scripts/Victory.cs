using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{
    void OnTriggerEnter()
    {
        ScenesManager.Instance.LoadScene(ScenesManager.Scene.S_Victory);
    }
}
