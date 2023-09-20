using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Button directionButton; //yön butonu
   public Button jumpButton; //zıplama butonu
    public BallMove ballMoveScript; 
    public GroundSpawner groundSpawnerScript;
   


    private void Start()
    {
        directionButton.onClick.AddListener(ballMoveScript.DirectionChange);
        //direction butonuna tıklandığında change direction fonk. çalıştırır ve bu fonksiyonda directionchangei çalıştırır.
    }


}

 
