using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plataformaFlutuante : MonoBehaviour {

    private Transform plataforma;
    private float movimento;
    private float posIni;
	// Use this for initialization
	void Start () {
        plataforma = GetComponent<Transform>();
        movimento = 0.001f;
        posIni = plataforma.position.x;

    }
	
	// Update is called once per frame
	void Update () {
        if (plataforma.position.x <= posIni)
        {
            movimento = Mathf.Abs(movimento);
        }
        else if (plataforma.position.x >= posIni + 5)
        {
            movimento = movimento * -1;
        }
        plataforma.Translate(movimento, 0, 0);

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "MegaMan")
        {
            other.gameObject.transform.parent = transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.transform.parent = null;
    }
}
