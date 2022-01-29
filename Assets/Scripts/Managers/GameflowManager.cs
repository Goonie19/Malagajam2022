using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameflowManager : MonoBehaviour
{
    public Transform SpawnPosForMagnetic;
    public GameObject MagneticObject;

    public void Play()
    {
        GameManager.Instance.PlayerReference.enabled = true;
        Instantiate(MagneticObject, SpawnPosForMagnetic.position, MagneticObject.transform.rotation);
    }

    
}
