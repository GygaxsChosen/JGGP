using UnityEngine;

public class UIHealthController : MonoBehaviour 
{
    public bool m_UseRelativeLocation = true;

    private Quaternion m_RelativeLocation;

	// Use this for initialization
	void Start () 
    {
        m_RelativeLocation = transform.parent.localRotation;
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (m_UseRelativeLocation)
            transform.rotation = m_RelativeLocation;
	}
}
