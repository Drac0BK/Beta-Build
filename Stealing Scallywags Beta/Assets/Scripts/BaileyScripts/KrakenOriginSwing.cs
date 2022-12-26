using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrakenOriginSwing : MonoBehaviour
{
    bool isActive = false;
    [SerializeField]
    GameObject krakenSwingFrom;
    [SerializeField]
    GameObject dustEffect;
    [SerializeField]
    Animator animator;
    [SerializeField]
    float timer;
    
    [SerializeField]
    int playerCount;
    [SerializeField]
    bool isSwinging;

    public GameObject shadow;

    public void SetActive(bool a) { isActive = a; }

    private void Update()
    {
        if (isActive)
        {        
            if (playerCount != 0 && !isSwinging)
                timer += Time.deltaTime;

            if (timer >= 1)
            {
                Swing(transform.position);
                timer = 0;
            }
                
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<MyPlayer>() != null)
            playerCount += 1;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<MyPlayer>() != null)
            playerCount -= 1;
    }


    public void Swing(Vector3 attackPos)
    {
        isSwinging = true;
        Quaternion targetRotation = Quaternion.LookRotation(attackPos - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1 * Time.deltaTime);
        shadow.SetActive(true);
        StartCoroutine(SwingStart(attackPos));
    }
    public IEnumerator SwingStart(Vector3 attackPos)
    {
        animator.SetTrigger("KrakenSlam");
        yield return new WaitForSeconds(5.1f);
        dustEffect.SetActive(true);
        shadow.SetActive(false);
        Collider[] colliders = Physics.OverlapSphere(attackPos, 4.0f);
        foreach (var collider in colliders)
        {
            if (collider.GetComponent<Rigidbody>() != null)
                collider.GetComponent<Rigidbody>().AddExplosionForce(20, attackPos, 7.5f, 0.1f, ForceMode.Impulse);
            if (collider.GetComponent<MyPlayer>() != null && collider.GetComponent<MyPlayer>().isHit == false)
                collider.GetComponent<MyPlayer>().StartCoroutine("InvulFrames", 3f);
        }


        yield return new WaitForSeconds(1);

        dustEffect.SetActive(false);
        isSwinging = false;
    }
}
