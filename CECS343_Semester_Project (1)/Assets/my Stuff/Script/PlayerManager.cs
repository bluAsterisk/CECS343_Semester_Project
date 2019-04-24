using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Just loads LoadManager
    private void Awake()
    {
        LoadManager test = LoadManager.Instance;
        CameraFollowTarget.Instance.NewTarget(test.transform);
    }
}
