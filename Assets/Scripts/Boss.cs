using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour {

    public static int boss = 0;
    float movimento;
    [SerializeField] Animator animacao;
    bool viradoDireita = false;
    Rigidbody2D bossRB;
    [SerializeField] Transform pontoTiro;
    [SerializeField] Rigidbody2D prefab;
    [SerializeField] int vida = 100;
    [SerializeField] Text texto;
    [SerializeField] Animator canvas;
    [SerializeField] AudioSource morte;

	// Use this for initialization
	void Start () {

        bossRB = GetComponent<Rigidbody2D>();

        StartCoroutine(Pular());
        StartCoroutine(Atacar());

    }
	
	// Update is called once per frame
	void Update () {
        if (boss == 1 && vida >= 0)
        {
            movimento = 5f * Time.deltaTime;
            transform.Translate(movimento, 0, 0);
            animacao.SetFloat("Run", movimento);

            texto.text = vida.ToString();
        }

        if(vida <= 0)
        {
            morte.Play();
            morte.volume = 1f;
            animacao.SetBool("Die", true);
            Invoke("Morrer", 2);
        }
        
        
	}

    void Morrer()
    {
        Destroy(gameObject);
        canvas.SetTrigger("Menu");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Limite")
        {
            if(viradoDireita == false)
            {
                transform.Rotate(0, 180, 0);
                viradoDireita = true;
            }
            else if(viradoDireita == true)
            {
                transform.Rotate(0, -180, 0);
                viradoDireita = false;
            }
            
        }
        if(collision.gameObject.tag == "MegaBuster" && boss == 1)
        {
            Destroy(collision.gameObject);
            vida -= 10;
        }
    }

    private IEnumerator Pular()
    {
        yield return new WaitForSeconds(3f);
        if (vida >= 1)
        {
            bossRB.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
            animacao.SetTrigger("Jump");
            StartCoroutine(Pular());
        }
    }

    private IEnumerator Atacar()
    {
        yield return new WaitForSeconds(1.5f);
        if (vida >= 1)
        {
            Rigidbody2D clone;
            clone = Instantiate(prefab, pontoTiro.position, pontoTiro.rotation) as Rigidbody2D;
            clone.AddForce(transform.right * 500f);
            animacao.SetTrigger("Attack");
            StartCoroutine(Atacar());
        }
    }

}
