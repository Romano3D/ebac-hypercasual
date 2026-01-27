using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemCollactableBase : MonoBehaviour
{
    public string compareTag = "Player";
    public ParticleSystem particleVFX;
    public float timeToHide = 3;
    public GameObject graphicItem;

    [Header("Sounds")]
    public AudioSource audioSource;

  private void Awake()
    {
        //if (particleVFX != null) particleVFX.transform.SetParent(null);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag(compareTag))
        {
            Collect();
        }
    }
    protected virtual void Collect()
    {
        if (graphicItem != null) graphicItem.SetActive(false);
        Invoke("HideObject", timeToHide);
        OnCollect();
    }
    protected void HideItens()
    {
        gameObject.SetActive(false);
    }
    protected virtual void OnCollect()
    {
        if (particleVFX != null) particleVFX.Play();
        if (audioSource != null) audioSource.Play();
    }

}
 

   





    

