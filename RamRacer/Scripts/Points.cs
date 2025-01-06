using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Points : MonoBehaviour
{
   public int points = 0;
   

   public void IncrementPoints()
   {
      points++;
      gameObject.GetComponent<TextMeshProUGUI>().text = "Score: " + points.ToString();
   }


}
