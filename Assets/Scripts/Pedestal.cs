using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestal : MonoBehaviour
{
    public float exhibitHeight = 0f;
    public float rotationRate = 1f;
    
    public GameObject prize;

    private Transform exhibitPoint;
    private ParticleSystem particle;
    private GameObject halo;

    private Vector3 originalScale;

    void Awake() {
        exhibitPoint = transform.Find("ExhibitPoint");
        particle = GetComponent<ParticleSystem>();
        halo = transform.Find("Halo").gameObject;
    }

    void Update() {
        if (prize != null) {
            prize.transform.RotateAround(exhibitPoint.position, Vector3.up, rotationRate * Time.deltaTime);
        }
    }

    public void ExhibitPrize(GameObject prize) {
        this.prize = prize;
        Prize pinfo = prize.GetComponent<Prize>();

        this.exhibitHeight = pinfo.exhibitHeight;
        originalScale = prize.transform.localScale;
        prize.transform.localScale *= pinfo.exhibitScale;
        prize.transform.Rotate(pinfo.exhibitRotation);

        // move prize to be rotated around the correct center
        Vector3 absoluteMovement = exhibitPoint.position - prize.transform.Find("RotationCenter").position;
        prize.transform.position += absoluteMovement + new Vector3(0, exhibitHeight, 0);

        particle.Play();
        halo.SetActive(true);
    }

    /* Remove reference to the prize GO */
    public void StopExhibit() {
        prize.transform.localScale = originalScale;
        prize = null;

        particle.Pause();
        particle.Clear();
        halo.SetActive(false);
    }
}
