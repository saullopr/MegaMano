using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MegaMan : MonoBehaviour {

    //variaveis necessarias para controle
    [SerializeField] Animator animator;
    [SerializeField] Transform megaman;
    [SerializeField] SpriteRenderer spriteMegaMan;
    [SerializeField] Rigidbody2D mmrb;

    [SerializeField] Rigidbody2D prefabtiro;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject parede;
    float yv;
    bool mexer = true;
    float movimento = 0;
    private int hp=100;
    private int contador = 0;
    bool pulando = false;
    bool viradoDireita = true;
    public Text texto;
    public AudioSource dano;
    public AudioSource tiro1;
    public AudioSource fase1;
    public AudioSource BossFight;
    // Use this for initialization
    void Start () {
        StartCoroutine("esperaAudio");
    }
	
	// Update is called once per frame
	void Update () {
        //convertendo texto para string na tela
        texto.text = hp.ToString();
        //movimento do personagem
        movimento = Input.GetAxisRaw("Horizontal") * 3.5f * Time.deltaTime;
        megaman.Translate(Mathf.Abs(movimento), 0, 0);
        animator.SetFloat("Run", movimento);

        if (mexer)
        {
            if (movimento > 0 && viradoDireita == false)
            {
                megaman.Rotate(0f, 180f, 0f);
                viradoDireita = true;

                //spriteMegaMan.flipX = false;
            }
            else if (movimento < 0 && viradoDireita == true)
            {
                megaman.Rotate(0f, -180f, 0f);
                viradoDireita = false;

                //spriteMegaMan.flipX = true;
            }


            //tecla de pulo
            if (Input.GetKeyDown(KeyCode.Space) && pulando == false)
            {
                animator.SetBool("Jump", true);

                mmrb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse); 
                if (mmrb.velocity.y < 7f)
                {
                    yv = mmrb.velocity.y;
                    yv = 7f;
                }
                Debug.Log(mmrb.velocity);
                pulando = true;
            }
            //tecla de tiro
            //if (Input.GetKeyDown(KeyCode.J))
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("Shoot");
                tiro1.Play();
                Rigidbody2D clone;
                clone = Instantiate(prefabtiro, firePoint.position, firePoint.rotation) as Rigidbody2D;
                clone.AddForce(transform.right * 500f);
            }
        }

        //checagem para saber se o personagem morreu
        if (hp <= 0)
        {
            mexer = false;

            if(contador==0)
            animator.SetTrigger("Die");
            StartCoroutine("Renasce");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Chao" || collision.gameObject.tag == "plataforma") //resetar o pulo
        {
            pulando = false;
            animator.SetBool("Jump", false);
        }
        if (collision.gameObject.tag == "Dano") //fazer o player tomar dano
        {
            hp = hp - 20;
            texto.text = hp.ToString();
            Destroy(collision.gameObject);
            dano.Play();
            animator.SetTrigger("Damage");
        }
        if (collision.gameObject.tag == "Dano2")//fazer o player tomar dano
        {
            hp = hp - 30;
            texto.text = hp.ToString();
            dano.Play();
            animator.SetTrigger("Damage");
        }
        if (collision.gameObject.tag == "Dano3")//fazer o player tomar dano
        {
            hp = hp - 100;
            texto.text = hp.ToString();
            dano.Play();
            animator.SetTrigger("Die");
        }
        if(collision.gameObject.tag == "Limbo") //caso o player cair para fora do mundo
        {
            hp = 0;
            texto.text = hp.ToString();
            dano.Play();
            animator.SetTrigger("Die");
        }
        if(collision.gameObject.tag == "rapadura") //rapadura para recuperar vida
        {
            hp += 50;
            if(hp >= 100)
            {
                hp = 100;
            }
            Destroy(collision.gameObject);
        }
    }
    private IEnumerator esperaAudio()
    {
        yield return new WaitForSeconds(4.0f);
        fase1.Play();

    }
    private IEnumerator Renasce() //tempo para renascer apos morer
    {
        contador = 1;
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Finish") //chegou ao Boss
        {
            fase1.Pause();
            BossFight.Play();
            BossFight.volume = 0.3f;
            parede.SetActive(true);
            collider.gameObject.SetActive(false);
        }
        if (collider.gameObject.tag == "Dano2")
        {
            hp = hp - 20;
            Destroy(collider.gameObject);
        }
    }

}