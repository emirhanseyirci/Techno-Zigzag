using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GroundSpawner : MonoBehaviour
{

    public GameObject final_ground; //var olan son zemindir,oluşacak olan yeni zemin bu zeminin bilgilerine göre oluşur.
    public GameObject ball; //oyunumuzdaki topumuz
    public int power; //topun jump fonksiyonunu kullanabilmemiz için gerekli olan değerdir.
    public TextMeshProUGUI powertext; //power değerinin ekranda yazacağı textmeshtir.
    private bool powerboost = true; // power değeri bir seviyeye kadar artabilir ve azaldıktan sonra paralel olarak artışa geçmesi gerekir. bu durumun kontrol edilmesi için powerboostu kullanıyoruz.
    public BallMove ballmovescript; //ballmovescriptindeki bazı değerleri kullanacağımız için scripte erişmek için kullandık.
    public int speedlimits; //zeminin oluşma sayısını/zorluğunu ayarlayan değerdir.
    private int maxPower = 10; // power değerinin alabileceği max değerdir.
    public AudioSource downblock_Sound; // küpler rastgele oluşurken mevcut y pozisyonuna göre daha aşağıda oluştuğunda çalacak ses.
    
    public void gameEnd() //oyunun sonlanma fonksiyonu
    {
        SceneManager.LoadScene("Main_Menu"); //gameend fonk. çağırıldığında Main_Menu adlı sahne açılır

    }

    

    void Start()
    {

        downblock_Sound= GetComponent<AudioSource>(); //oyun başladığında downblocksound adlı audiosource değişkenine scriptin bulunduğu groundspawner objesinin içinde bulunan audio source komponentine erişilir ve bu komponentin içinde bulunan ses atanmış olur.

        UpdateSpeedLimits(); //oyun başladığında küplerin oluşması gerekir doğal olarak,bu yüzden bu fonksiyonu çağırıyoruz ve speed limits değerini alıyoruz...
        for (int i = 0; i < speedlimits; i++) //... yukarıdaki fonk. çalışmamızın sonucunda aldığımız speedlimitse göre döngü başlıyor ve makeaground fonksiyonu çalıştırılarak küplerimiz oluşuyor
        {
            power = 0;
            //burada power=0 değerini vermezsek  ilk başta makeaground fonksiyonunun rastgele oluşturduğu ground sayısı=power olur.
            makea_ground();
        }

    }

    void Update()
    {     
        UpdateSpeedLimits(); //topun hızı ve küpün oluşma sıklığının değeri sürekli güncellenmesi gerektiği için update içinde çalıştırıyoruz.
    }

    void downblock_Sound_effect()
    {
        downblock_Sound.Play();  //çağırıldığında downblocksound sesini çalan fonksiyon
    }

    void UpdateSpeedLimits()
    {
        float currentSpeed = ballmovescript.speed; //currentspeed:topun mevcut/anlık hızıdır. yani ballmovescripttekispeed değeridir.


        if (currentSpeed < 2)     /*topun hızı 2.1'den küçükse speed limits=6 olur
                                   * for değeri buna göre döner dolayısıyla makeagrond fonksiyonu döngü kadar çalışır */
            speedlimits = 5;
        else if (currentSpeed < 2.1)
            speedlimits = 6;
        else if (currentSpeed < 2.2)
            speedlimits = 7;
        else if (currentSpeed < 2.3)
            speedlimits = 9;
        else if (currentSpeed < 2.5)
            speedlimits = 11;
        else if (currentSpeed < 2.6)
            speedlimits = 14;
        else if (currentSpeed < 2.8)
            speedlimits = 18;
        else if (currentSpeed < 3)
            speedlimits = 25;
        else if (currentSpeed < 3.1)
            speedlimits = 33;
        else if (currentSpeed < 3.3)
            speedlimits = 50;
        else if (currentSpeed < 3.5)
            speedlimits = 75;
        else if (currentSpeed < 4)
            speedlimits = 90;
        else if (currentSpeed < 4.75)
            speedlimits = 120;
        else if (currentSpeed < 5.75)
            speedlimits = 150;
        else if (currentSpeed < 7)
            speedlimits = 185;



    }







    public void makea_ground()
    {
        Vector3 directionnewground; //topun yeni yönünü temsil eder


        if (Random.Range(0, 2) == 0) //0 ile 2 arasında bir sayı seç (0,1,2) bu sayı 0 a eşitse
        {
                if (Random.Range(0, 25) == 3) //0 ile 25 arasında bir sayı seç (0...,...25) bu sayı 3'e eşitse
                {
                    downblock_Sound_effect(); //downblocksoundeffect fonksiyonunu çalıştır --->ses efekti çalar
                    directionnewground = Vector3.left * 2 + Vector3.down * 2; 
                }
                else //seçilen sayı 3 değilse 
                {
                    directionnewground = Vector3.left; //yönü left yap
                if (powerboost == true) //powerboost aktifse
                {
                    if (power < maxPower) //mevcut power değeri maxpowerdan (10) küçükse power değerini bir arttır.
                    {
                        power++;
                    }
                }
                }
        }
        else //seçilen değer 0 değilse
        {
                if (Random.Range(0, 25) == 3)  
                {
                    downblock_Sound_effect();
                    directionnewground = Vector3.forward * 2 + Vector3.down * 3;
                }
                else
                {
                    directionnewground = Vector3.forward;
                    if (powerboost == true)
                    {
                        if (power < maxPower)
                        {
                            power++;
                        }
                    }
                }
        }
        

        power = Mathf.Clamp(power, 0, maxPower); //bu kod power değerinin belli bir aralıkta kalmasını sağlar. (maxPower=10)

        powertext.text = power.ToString(); //powertext adlı textmeshpronun textini powera eşitler


        final_ground = Instantiate(final_ground, final_ground.transform.position + directionnewground, final_ground.transform.rotation); //burada klonlanacak olan nesnenin özelliklerini belirliyoruz. burada farklı olan tek şey finalgroundtransformpositionun üstüne directionnewground koordinatlarının eklenmesidir.

        if (power <= 0 && powerboost) //power değeri sıfıra eşit ve küçükse ve powerboost değeri true ise powerboostu false yap ve powerboosactive adlı coroutineı çalıştır
        {
            powerboost = false;
            StartCoroutine(powerboostactive());



        }
    }


    IEnumerator powerboostactive()
    {
        yield return new WaitForSeconds(5f); /*yukarıdaki şartlar sağlandığında yani power değeri sıfıra eşit ve küçük olduğunda ve powerboost değeri true olduğunda powerboostu false  yapar
                                              *5 saniye boyunca bekler ve power değerini bir yapar ve powerboostu aktifleştirir */
        power = 1;                                                          
        powertext.text = power.ToString();
        powerboost = true;

    }


}

    


    




    




