using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallMove : MonoBehaviour
{


    Vector3 defaultDirection;
    public float speed; //topun hızı
    public GroundSpawner groundspawnerscript; //diğer c# scriptini bu script içerisinden erişilebilir şekilde çağırdık ve ulaşacağımız fonksiyonu public yaptık.
    public static bool did_itfall;       //topun düşüp düşmediğini gösteren değişken-öbür scriptlerden erişmek için static yazdık.
    public float addSpeed; //topun hızına eklenecek olan hız değeri
    public float jumpingPower; //topun zıplama gücü
    public bool isFalling = false; //topun düşme "durumunda" olup olmadığını kontrol eder.
    public Rigidbody rb; //topun rigidbodysidir
    public AudioSource balljump_Sound; //top zıplayınca çalacak ses
    public AudioSource gameend_Sound; //top düşerken çalacak ses
    public Score scoreScript; //score scriptine  erişim




    public void Start()
    {
        speed = 2.15f; //topun başlangıç hızı 2.15
        defaultDirection = Vector3.forward; //0,0,1 varsayılan hareket
        did_itfall = false; //oyun başladığında düşme değeri false yani düşmemiş durumda
        addSpeed = 0.7f; //eklenecek hız 0.7
        jumpingPower = 5f;
        balljump_Sound =GetComponent<AudioSource>(); //audiosource bileşenindeki ses balljumpa eşittir,bu script aynı zamanda canvas objesinde bulunduğu için oradakii audiosourcea erişir.
        gameend_Sound =GetComponent<AudioSource>();//audiosource bileşenindeki ses gameende eşittir,işte bu ball objesinde bulunan audosourcea erişir.
    }

    public void gameend_Sound_effect()
    {
        gameend_Sound.Play();
    }


    public void balljump_Sound_effect()
    {
        balljump_Sound.Play();
    }


public void DirectionChange()
   {
                if (defaultDirection.x == 0) //defaultdirection.x sıfıra eşitse defaultdirection'u vector3 left yap=1,0,0
                {
                    defaultDirection = Vector3.left;
                }
                else //defaultdirection.x sıfıra eşit değilse  defaultdirection'u vector3forwardyap=0,0,1
                { //gördüğünüz üzere x değeri ya 1 ya 0 oluyor, işte bu bir nevi bir döngüde yön değiştiriyor.
                     defaultDirection = Vector3.forward;
                }
            speed += addSpeed * Time.deltaTime; //topun yönü her değiştiğinde hızı da artıyor.
    }


    public void Update()
    {
    
        if (transform.position.y - groundspawnerscript.final_ground.transform.position.y <= -3f)   //topun y değeri ile son zeminin y değeri arasındaki fark -3e eşit ve küçükse 

        {
            if (!gameend_Sound.isPlaying) //bu şartı,gameendsoundunun arka arkaya çalmasını engellemek için yazdım. gameendsound sesi çalınmıyorsa...
            {
                gameend_Sound_effect();
            }

            if (transform.position.y - groundspawnerscript.final_ground.transform.position.y <= -15f)   //...fark -15'e eşit ve küçükse 
            {
                isFalling = true;    //düşme durumunda olduğunu aktif et
                // Top yere düştüğünde gameEnd fonksiyonunu çağırın.
                FindObjectOfType<GroundSpawner>().gameEnd(); 
            }

        }
    }

    
    public void FixedUpdate()
    {
        Vector3 move = defaultDirection * Time.deltaTime * speed; //topun hareketini güncelleyen kod bloğudur. yönünü ve hızını günceller.
        transform.position += move; //topun mevcut pozisyonunu move vektörüne göre günceller.
        /*Sonuç olarak, bu kod bloğu, topun her sabit zaman aralığında belirli bir hızda ve yönle hareket etmesini sağlar. 
         * Hareket hesaplaması, oyunun sabit bir hızda çalışmasını ve farklı cihazlarda aynı deneyimi sunmasını sağlayan önemli bir parçadır.*/
    }


    public void OnCollisionExit(Collision collision) //top nesnesi ile ground teması kestiği anda tetiklenir.
    {
        if (collision.gameObject.tag == "ground") //eğer nesnenin tagı ground ise
        {
            scoreScript.score++; //score değerini bir arttır
            collision.gameObject.AddComponent<Rigidbody>();  //bu nesneye rigidbody ekle-->topun üstünden geçildikten sonra düşmesi için
            StartCoroutine(DeleteGround(collision.gameObject)); //teması kestiğin objeyi sil.
            groundspawnerscript.makea_ground(); //bir zemin yok olurken bir zeminde oluşsun ki arada boşluk olmasın.
        }
    }


    IEnumerator DeleteGround(GameObject willDeleteGround) //zemin silme fonksiyonu
    {
        yield return new WaitForSeconds(3f); //... saniye bekle ve alttaki komutu çalıştır.
        Destroy(willDeleteGround);
    }


    public void Jump()
    {
        if (rb!=null && groundspawnerscript.power >= 7 )   //rigidbody varsa ve groundspawnerscript.power 7'ye eşit ve büyükse
        {
         
               rb.AddForce(Vector3.up * jumpingPower, ForceMode.Impulse); //rigidbodye kuvvet uygula: yukarıya doğru*zıplama gücü (forcemode.impulse ani hareket için kullanıldı)
            groundspawnerscript.power = groundspawnerscript.power - 8; //her zıplayışta 8 birim power değerini harca yani azalt.
            balljump_Sound_effect(); //top zıplama ses efektini çalıştır.
        }
        else
       {
       } 
    }
  
}