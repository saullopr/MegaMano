using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class runner : MonoBehaviour
{

    public GameObject player;

    public bool Lado;
    public float tempo;

    [SerializeField]
    private float velocidade;
    private int hp = 100;
    private Animator anim;
    public AudioSource dano;
    private SpriteRenderer teto;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(TrocaPosicao());

        anim = this.gameObject.GetComponent<Animator>();
        teto = this.gameObject.GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void FixedUpdate() { 
  

        if (hp <= 0)
        {
            anim.SetTrigger("Die");
            StartCoroutine("esperaMorrer");
        }

        transform.Translate(velocidade*(Vector2.left * Time.deltaTime));

    }
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
