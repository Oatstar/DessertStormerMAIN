using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplosionScript : MonoBehaviour
{
    private void Start()
    {
        Invoke("EventTrigger", 0.2f);
    }

    public void EventTrigger()
    {
        Destroy(this.gameObject);
    }
}
