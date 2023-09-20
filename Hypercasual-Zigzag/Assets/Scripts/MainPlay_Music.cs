using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class MainPlay_Music : MonoBehaviour
{
    public AudioSource mainplay_Sound;
    float maxSpeed = 10f; 
    float minPatch = 0.2f;
    float maxPitch = 2f;
    public BallMove ballmovescript;
    public float ballSpeedfor_pitch; 
    public GameObject ball;


    void Start()
    {
        mainplay_Sound = GetComponent<AudioSource>();
        ballmovescript=ball.GetComponent<BallMove>();
        mainplay_Sound.Play();
    }



    void Update()
    {
       float currentball_Speed=ballmovescript.speed;
       float pitch=Mathf.Lerp(minPatch, maxPitch, currentball_Speed/maxSpeed); /*Bu hesaplama, topun hızına göre pitch değerini bir aralıkta (minimum ve maksimum pitch) sürdürmek için kullanılır. 
                                                                                * Yani, topun hızı arttıkça veya azaldıkça, sesin yüksekliği değişir.*/
        mainplay_Sound.pitch=pitch;

    }




}
