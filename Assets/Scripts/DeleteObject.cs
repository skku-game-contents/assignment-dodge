using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteObject : MonoBehaviour
{
    [SerializeField] GameObject obj;

    public void DestroyObj()
    {
        Destroy(obj);
    }
}
