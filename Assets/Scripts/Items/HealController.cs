using UnityEngine;

public class HealController :MonoBehaviour, IPickeable
{
    [SerializeField]
    private Heal heal;

    public GameObject GetItem => gameObject;

    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public (Vector3, Vector3) GetOkRotationAndPosition()
    {
        // first rotation, then position
        return (new Vector3(0f, 14f, 75f), new Vector3(0.0068f, 0.0166f, 0.0433f));
    }

    public void GetPickedUp()
    {
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.SetActive(false);
    }

    public Sprite GetImage()
    {
        throw new System.NotImplementedException();
    }
}
