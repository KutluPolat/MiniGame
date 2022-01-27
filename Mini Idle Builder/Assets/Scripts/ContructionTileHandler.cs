using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContructionTileHandler : MonoBehaviour
{
    private GridController _gridController;

    private void Start()
    {
        _gridController = GameManager.Instance.GridController;
    }

    void Update()
    {
        Debug.Log(_gridController.IsGridEmpty(gameObject));
    }
}
