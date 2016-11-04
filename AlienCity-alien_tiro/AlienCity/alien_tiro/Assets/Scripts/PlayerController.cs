using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	private Animator anim;
	private Rigidbody2D rb2d;
	public Transform posPe;
	[HideInInspector] public bool tocaChao = false;
	public float Velocidade;
	public float ForcaPulo = 1000f;
	[HideInInspector] public bool viradoDireita = true;
	private bool pula = false; 
	public float ShootingRate = 0;
	public float ShootCooldown1 = 0.1f;
	public float ShootCooldown2 = 0.1f;
	public Transform CreateBullet;
	public Transform CreateBullet2;
	public GameObject Bullet;
	public GameObject Bullet2;

	

	void Start () {
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();

	}
	
	// Update is called once per frame
	void Update () {
		//Implementar Pulo Aqui!
		tocaChao = Physics2D.Linecast(transform.position, posPe.position, 1 << LayerMask.NameToLayer("Ground"));
		if(Input.GetKeyDown(KeyCode.Space) && tocaChao){
			//pula
			pula = true;
		}
	}

	void FixedUpdate()
	{
		float translationY = 0;
		float translationX = Input.GetAxis ("Horizontal") * Velocidade;
		transform.Translate(new Vector3(translationX,0,0), Space.World);
		transform.Rotate (0, 0, 0);
		
		if (translationX != 0 && tocaChao) {
			anim.SetTrigger ("corre");
		} else {
			anim.SetTrigger("parado");
		}

		//Programar o pulo Aqui!
		if(pula == true){

			rb2d.AddForce(new Vector2(0f, ForcaPulo));
			anim.SetTrigger("pula");
			pula = false;		
		
		} 

		if (translationX > 0 && !viradoDireita) {
			Flip ();
		} else if (translationX < 0 && viradoDireita) {
			Flip();
		}
		
		
		if(Input.GetKeyDown(KeyCode.C)) // Executa tiro com tecla c
        {
            anim.SetTrigger("atira1");
            Fire1();
			ShootCooldown1 = ShootingRate;   
        
        
        }  
		
		if(Input.GetKeyDown(KeyCode.V)) // Executa tiro com tecla v
        {
            anim.SetTrigger("atira2");
            Fire2();
			ShootCooldown2 = ShootingRate;
        
        
        
        }  
		

	}
	
	void Flip()
	{
		viradoDireita = !viradoDireita;
		Vector3 escala = transform.localScale;
		escala.x *= -1;
		transform.localScale = escala;
	}
	
	void Fire1()
    {
        
        if(ShootCooldown1 <= 0f) //verifica se o cooldown do tiro está zerado
        {
            if(Bullet != null) //verifica a existência de um prefab "bala"
            {
                var cloneBullet1 = Instantiate(Bullet, CreateBullet.position, Quaternion.identity) as GameObject;
                cloneBullet1.transform.localScale = this.transform.localScale; //cria um clone do objeto "tiro" toda vez que o botão de atirar for apertado
                  
            }
              
        }  
    
    }

	void Fire2()
    {
        
        if(ShootCooldown2 <= 0f) //verifica se o cooldown do tiro está zerado
        {
            if(Bullet != null) //verifica a existência de um prefab "bala"
            {
                var cloneBullet2 = Instantiate(Bullet2, CreateBullet2.position, Quaternion.identity) as GameObject;
                cloneBullet2.transform.localScale = this.transform.localScale; //cria um clone do objeto "tiro" toda vez que o botão de atirar for apertado
                  
            }
              
        }  
    
    }	

	
	
}
