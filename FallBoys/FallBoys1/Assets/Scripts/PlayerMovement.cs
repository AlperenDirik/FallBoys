using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed; //
    [SerializeField] private float rotationSpeed = 500;

    public Animator animator;

    private Touch _touch; //As�l nokta sadece ileri hareket ederek karakterin nereye d�nece�ini belirlemek. 

    private Vector3 _touchDown; //ekrana dokundu�umda bir nokta alaca��m
    private Vector3 _touchUp; //b�rakt���mda farkl� bir nokta alaca��m. Ard�ndan ��karma i�lemi yapaca��m b�ylece gidece�im y�n�n vekt�r�n� elde edece�im.

    private bool _dragStarted; //s�r�kleme ba�lang�c�n� kontrol edece�im de�i�ken
    private bool isMoving;
    private void Start()
    {
        animator.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.touchCount > 0) //ekrana bir input varsa 
        {
            _touch = Input.GetTouch(0); //ilk ald���m inputu kendi inputuna e�itliyorum
            if (_touch.phase==TouchPhase.Began) //dokunma i�lemi ba�lad��� zaman
            {
                _dragStarted = true; //dokunma ba�lad� bilgisini de�i�kenime g�nderiyorum.
                animator.SetBool("isMoving", true);//animator'den bu de�i�keni kullanaca��m i�in true'ya �ekiyorum
                _touchDown = _touch.position; 
                _touchUp = _touch.position; // 2 pozisyonu da elde ettim
            }
        }
        if (_dragStarted) //kullan�c�n�n elini �ekme olas�l��� oldu�u i�in touch phase moved kontrolleriyle hareket ettirece�im.
        {
            if (_touch.phase == TouchPhase.Moved)
            {
                _touchDown = _touch.position; //ba�lang�� pozisyonumu ald�m.
            }
            if (_touch.phase== TouchPhase.Ended)  
            {
                _touchDown = _touch.position;  //biti� pozisyonumu ald�m.
                animator.SetBool("isMoving", false);
                _dragStarted = false; //i�lemlerin bitti�ini de�i�kenlerime haber verdim.
            }
            gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, CalculatingRotation(),rotationSpeed*Time.deltaTime);
            //karakterimizi hareket ettirmemizi sa�l�yor nereden ba�layaca��z nerde b�rakaca��z nereye bakaca��z
            gameObject.transform.Translate(movementSpeed * Time.deltaTime * Vector3.forward); // ileri hareket etmesi i�in
        }
    }
    Quaternion CalculatingRotation() // quaternion ile y�n� manipule ederek a��y� alaca��m rotasyonda
    {
        Quaternion temp = Quaternion.LookRotation(CalculatingDirection(),Vector3.up); //bir vekt�r verildi�inde o y�ne bak�lmas�n� sa�l�yor
        return temp;
    }

    Vector3 CalculatingDirection() //fark al�p fonksiyonu �a��rd���m zaman vekt�r3 de�er d�nd�rmesi i�in
    {
        Vector3 temp = (_touchDown - _touchUp).normalized; //vekt�rleri k���ltmek i�in normalized kullan�yorum
        temp.z = temp.y; // y ekseninde bir hareket olmas�n istedi�im i�in z'ye e�itledim
        temp.y = 0;
        return temp;//art�k bu fonksiyon bana kullan�c�n�n olu�turdu�u �izginin vekt�rel halini verebilir.
    }
}
