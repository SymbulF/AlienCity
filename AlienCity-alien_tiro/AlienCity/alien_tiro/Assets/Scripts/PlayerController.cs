using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	private Animator anim;
	private Rigidbody2D rb2d;
    public float Velocidade;
    public bool viradoDireita = true;
    public float ShootingRate = 0.1f;
    public float ShootCooldown = 0f;
    public Transform CreateBullet;
    public GameObject Bullet;


	void Start () {
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D>();

	}
    
   	
	void FixedUpdate()
	{
		if (ShootCooldown > 0)
			ShootCooldown -= Time.deltaTime; //registra o cooldown do tiro


		float translationY = 0;
		float translationX = Input.GetAxis ("Horizontal") * Velocidade; //Controle de Movimento
		transform.Translate(new Vector3(translationX,0,0), Space.World);
		transform.Rotate (0, 0, 0);
		if (translationX != 0) {
			anim.SetTrigger ("corre");
		} else {
			anim.SetTrigger("parado");
		}

		if (translationX > 0 && !viradoDireita) {
			Flip ();
		} else if (translationX < 0 && viradoDireita) {
			Flip();
		}
        
        if(Input.GetKeyDown(KeyCode.Space)) // Executa tiro com tecla espaço
        {
            anim.SetTrigger("Atirar");
            Fire();
			ShootCooldown = ShootingRate;
        
        
        
        }  

	}
	
    void Flip() // verifica a escala do personagem e gerencia a virada do personagem
	{
		viradoDireita = !viradoDireita;
		Vector3 escala = transform.localScale;
		escala.x *= -1;
		transform.localScale = escala;
	}
    
    void Fire()
    {
        
        if(ShootCooldown <= 0f) //verifica se o cooldown do tiro está zerado
        {
            if(Bullet != null) //verifica a existência de um prefab "bala"
            {
                var cloneBullet = Instantiate(Bullet, CreateBullet.position, Quaternion.identity) as GameObject;
                cloneBullet.transform.localScale = this.transform.localScale; //cria um clone do objeto "tiro" toda vez que o botão de atirar for apertado
                  
            }
              
        }  
    
    }	
}