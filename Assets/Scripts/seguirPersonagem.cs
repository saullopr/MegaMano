using UnityEngine;
using UnityEngine.SceneManagement;

public class seguirPersonagem : MonoBehaviour {

    [SerializeField] Transform personagem;
    [SerializeField] GameObject bossref;
    public int boss = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.SetPositionAndRotation(new Vector3(personagem.position.x, personagem.position.y + 1.5f, -20f), Quaternion.identity);

        if(personagem.position.x >= 115f)
        {
            boss = 1;
            Boss.boss = 1;
            bossref.SetActive(true);

        }

        if(boss == 1)
        {
            this.transform.SetPositionAndRotation(new Vector3(119f, 0.32f, -20f), Quaternion.identity);
        }
    }
}
