using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

  [SerializeField]
  private GameObject m_Player;
  [SerializeField] 
  private GameObject m_Recticle;
  [SerializeField]
  private GameObject m_BtnGroup;
  [SerializeField]
  private Button m_btnPlay;
  [SerializeField]
  private GameObject m_BtnSelectLevel;
  RaycastHit hit;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	  Physics.Raycast(m_Recticle.transform.position, m_Recticle.transform.forward, out hit); 
    if(hit.collider.tag == "VRMenu" && GvrViewer.Instance.Triggered)
    {
          m_Player.GetComponent<PlayerController>().m_move = true;
          Invoke("SetActive", 1);
          Debug.Log("okokok");
    }
	}


  public void SetActive()
  {
      m_BtnGroup.SetActive(false);
      m_BtnSelectLevel.SetActive(true);
  }



}
