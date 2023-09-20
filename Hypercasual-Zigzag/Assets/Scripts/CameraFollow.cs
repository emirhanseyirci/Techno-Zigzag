using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform ballTransform; 
    Vector3 difference; //fark--->bizim unity içerisinde elle ayarladığımız uzaklık
    


    void Start()
    {
        difference = transform.position - ballTransform.position; //kameranın pozisyonu ile topun pozisyonu arasındaki farktır
        
    }


    

    void Update()
    {
        if (BallMove.did_itfall == false) //eğer top düşmediyse kameranın pozisyonu=topun pozisyonu + bunların arasındakı uzaklık

        {
            transform.position = ballTransform.position + difference;

        }
    }

}
