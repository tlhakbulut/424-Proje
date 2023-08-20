using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyNpc : MonoBehaviour
{
    //deskPointController
    private DeskPointController deskPointController;
    public Transform deskTransform;
    private void Awake()
    {
        deskPointController = deskTransform.GetComponent<DeskPointController>();
    }
    public void DestroyObj()
    {
        deskPointController.isEmpty = true;
        deskPointController.currentCount -= 1;
        Destroy(gameObject);
    }
}
