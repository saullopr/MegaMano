using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalangoDoteto : MonoBehaviour {

    //private Rigidbody2D rb;
    public GameObject player;

    public bool Lado;
    public float tempo;

    [SerializeField]
    private float velocidade;
    private int hp = 100;
    private Animator anim;
    public AudioSource dano;
    private RaycastHit2D hit;
    private Vector2 posicao;
    private SpriteRenderer teto;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(TrocaPosicao());

        //rb = GetComponent<Rigidbody2D>();
        anim = this.gameObject.GetComponent<Animator>();
        teto = this.gameObject.GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
      
        hit = Physics2D.Raycast(transform.position,-transform.up);
        posicao = new Vector2(transform.position.x, transform.position.y);
        Debug.DrawLine(transform.position, hit.point);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.name == "MegaMan")
            {
                this.gameObject.GetComponent<Rigidbody2D>().constraints = ~RigidbodyConstraints2D.FreezePositionY;
                teto.flipY = false;
            }
        }

        //rb.AddForce(Vector2.left * 5);

        if (hp <= 0)
        {
            anim.SetTrigger("Die");
            StartCoroutine("esperaMorrer");
        }

        transform.Translate(velocidade * (Vector2.left * Time.deltaTime));
    }

 //   private void OnTriggerEnter2D(Collider2D collision)
  //  {
     //   if (collision.tag == "Limite")
   //     {
     //       MudarDirecao();
     //   }
  //  }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MegaBuster")
        {
            dano.Play();
            hp = hp - 20;
            Destroy(collision.gameObject);
        }
    }

    private IEnumerator esperaMorrer()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);
    }

    private IEnumerator TrocaPosicao()
    {
        yield return new WaitForSeconds(tempo);
        transform.Rotate(0, 180, 0);
        StartCoroutine(TrocaPosicao());
    }
}
