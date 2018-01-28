using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public bool moonWantsToSwitch;
    public bool sunWantsToSwitch;

    [SerializeField]
    private Transform sunTransform;
    [SerializeField]
    private Transform moonTransform;

    private void Start()
    {
        moonWantsToSwitch = false;
        sunWantsToSwitch = false;
    }
}
