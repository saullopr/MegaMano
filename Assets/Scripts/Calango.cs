using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calango : MonoBehaviour
{
    private RaycastHit2D hit;
    private Animator anim;
    private int hp=100;
    public GameObject tiro;
    private int contador = 0;
    private Vector2 doente;
    private IEnumerator coroutine;
    public AudioSource dano;
    // Use this for initialization
    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        hit = Physics2D.Raycast(transform.position, -transform.right);
        doente = new Vector2(-1 + transform.position.x, transform.position.y);
        Debug.DrawLine(transform.position, hit.point);
        if (hit.collider != null && contador <=0)
        {
            if (hit.collider.gameObject.name == "MegaMan")
            {
                anim.SetTrigger("Shoot");
               GameObject satanas = Instantiate(tiro, doente, transform.rotation);
               satanas.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 5f, ForceMode2D.Impulse);
               contador++;
               StartCoroutine("reseta");
            }
        }
        
        if(hp <= 0)
        {
            anim.SetTrigger("Die");
            StartCoroutine("esperaMorrer");
        }
    }
    private IEnumerator reseta()
    {
        yield return new WaitForSeconds(1.0f);
        contador = 0;
    }
    private IEnumerator esperaMorrer()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MegaBuster")
        {
            dano.Play();
            Destroy(collision.gameObject);
            hp = hp - 50;
        }   
    }

}


