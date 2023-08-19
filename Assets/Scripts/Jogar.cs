using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jogar : MonoBehaviour {
    [SerializeField] Canvas canvas;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void IniciarJogo()
    {
        SceneManager.LoadScene("Jogo", LoadSceneMode.Single);
    }

    public void SairJogo()
    {
        Application.Quit();
    }

    public void Creditos()
    {    
        canvas.gameObject.SetActive(true);
    }
}
