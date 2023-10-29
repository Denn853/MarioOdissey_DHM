using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loseUI : MonoBehaviour
{
    [SerializeField] private Canvas canvas;

    public void ShowLoseUI()
    {
        canvas.enabled = true;
    }
}
