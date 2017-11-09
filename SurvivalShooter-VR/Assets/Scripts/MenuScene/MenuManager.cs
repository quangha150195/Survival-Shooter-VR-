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
  private GameObject m_BtnAbout;
  [SerializeField]
  private GameObject m_BtnSelectLevel;

  RaycastHit hit;
  private int m_Once;
	// Use this for initialization
	void Start () {
	  
	}
	
	// Update is called once per frame
	void Update () {
	    Physics.Raycast(m_Recticle.transform.position, m_Recticle.transform.forward, out hit); 
        GameObject _thisButton = new GameObject();
        if(hit.collider.tag == "VRMenu")
        {
             _thisButton = hit.collider.gameObject;
              iTween.ScaleTo(_thisButton, iTween.Hash("x", 1.5f, "y", 1.5f, "time", 0.3f));
              if(GvrViewer.Instance.Triggered)
              {
                  m_Player.GetComponent<PlayerController>().m_move = true;
                  _thisButton.GetComponent<Button>().onClick.Invoke();
              }
        }
        else
        {
            GameObject[] _buttonMenu = GameObject.FindGameObjectsWithTag("VRMenu");
            foreach (GameObject a in _buttonMenu)
            {
                iTween.ScaleTo(a, iTween.Hash("x", 1.0f, "y", 1.0f, "time", 0.3f));
            }
        }
    }


  public void btnPlay()
  {
      
      m_BtnGroup.SetActive(false);
      m_BtnSelectLevel.SetActive(true);
  }

  public void btnAbout()
  {
      m_BtnGroup.SetActive(false);
      m_BtnAbout.SetActive(true);
  }





}
