using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class voltar : MonoBehaviour {

    public void VoltarMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
