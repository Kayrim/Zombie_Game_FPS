using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{

    #region Singleton
    public static PlayerTracker tracker;

    private void Awake()
    {
        tracker = this;
    }
    #endregion

    public GameObject player;
}
