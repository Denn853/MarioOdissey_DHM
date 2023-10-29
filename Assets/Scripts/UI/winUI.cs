using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winUI : MonoBehaviour
{
    [SerializeField] private Canvas canvas;

    public void ShowWinUI()
    {
        canvas.enabled = true;
    }
}
