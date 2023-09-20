using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundColorChange : MonoBehaviour
{



    public MeshRenderer _meshRenderer; /*Bu değişken, bu bileşene bağlı olan nesnenin MeshRenderer bileşenini depolar. 
                                                          * Yani, nesnenin görünen özelliklerini yönetmemize olanak tanır.*/

    public Color[] _colors; /*Bu dizi, nesnenin geçiş yapacağı renkleri içerir. 
                                               * Color türündeki renkler dizisi, nesnenin renginin değiştirilmesi için kullanılır.*/

    public float _lerpTime; /*Bu değişken, renk geçişlerinin ne kadar süreceğini belirtir. 
                                               * _lerpTime değeri ne kadar yüksekse, renk değişimleri o kadar yavaş olur.*/

    public int _index; /*Bu değişken, _colors dizisindeki hangi renge geçileceğini belirtir.
                                          * _index değeri dizinin bir öğesini işaret eder.*/

    public float _changer; /*Bu değişken, renk geçişlerinin ne kadar tamamlandığını izler.
                                              * Değer, 0 ile 1 arasında değişir ve bir renk geçişi tamamlandığında sıfırlanır.*/

    public BallMove ballmovescript;


     void Start()
    {

        ballmovescript=FindAnyObjectByType<BallMove>(); // BallMove bileşenine sahip ilk nesneyi bulmaya çalışır ve bu bileşene erişim sağlar.
        _lerpTime = ballmovescript.speed;
      _meshRenderer=GetComponent<MeshRenderer>();
        
    }

     void Update()
    {

        _lerpTime=ballmovescript.speed;

        _meshRenderer.material.color = Color.Lerp(_meshRenderer.material.color, _colors[_index], _lerpTime*Time.deltaTime);

        _changer = Mathf.Lerp(_changer, 1f, _lerpTime * Time.deltaTime);

        if (_changer > 0.9f) //changer değeri 1 olduğunda changer sıfırlanır ve index bir artar,yani renk değişir.
        {
            _changer = 0;
            _index++;

            if (_index >= _colors.Length) //index değeri colors dizisinin uzunluğuna eşit veya büyükse index sıfırlanır,yani en baştaki renge dönülür.
            {
                _index = 0;
            }


            /*meshrendererin color materyalinin başlangıç rengi varsayılan rengi,
             * colors dizisindeki renkler hedef renk, _index ile bu renklerin 
             * indexine göre ulaşıyoruz. _lerptime ise renk değişim hızı. _changer,
             * varsayılan değerinden 1f'e ulaşmaya çalışıyor _lerptime*time.deltatime zamanında göre.
             * eğer _changer 1'e neredeyse ulaşınca _changer değeri 0'a eşitleniyor ve 
             * index değeri bir artıyor ki diğer renge geçelim. _index color dizisinin uzunluğuna eşit veya 
             * büyükse indexi 0 a eşitliyoruz ki döngü başa dönsün */
           
        }

    }

}
