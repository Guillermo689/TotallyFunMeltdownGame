using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _pointsText;
    public int Points;


   public void UpdatePoints()
    {
        _pointsText.text = "Points: " + Points.ToString();
    }

}
