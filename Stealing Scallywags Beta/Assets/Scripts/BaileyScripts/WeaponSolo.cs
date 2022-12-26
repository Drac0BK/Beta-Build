using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityCore;
using UnityCore.Audio;

public class WeaponSolo : MonoBehaviour
{
    public bool isCutlass = false;
    public bool isBlunder = false;
    public bool isBomb = false;
    [SerializeField] bool hasFired;
    [SerializeField] bool hasThrown;
    [SerializeField] bool hasSwung;
    public float timer;
    public float swingTimer;

    public Vector3 bombPosition;
    public GameObject explosion;
    public GameObject gunFire;
    public GameObject gunSmoke;
    public GameObject bombRange;

    public GameObject bulletPrefab;

    public GameObject fuse;
    public GameObject fusePoint;
    //public GameObject bombRadius;

    public GameObject myPlayer;


    private void Start()
    {
    }

    private void FixedUpdate()
    {
        timer -= Time.deltaTime;

        string name = GetComponent<MeshFilter>().sharedMesh.name;

        if (name == "cutlasssword_LOW")
        {
            isCutlass = true;
            isBomb = false;
            isBlunder = false;
        }

        else if (name == "pCube3")
        {
            isCutlass = false;
            isBomb = false;
            isBlunder = true;
        }
        else if (name == "bomb_LOW")
        {
            isCutlass = false;
            isBomb = true;
            isBlunder = false;
        }

        if (timer <= 0)
        {
            GetComponent<MeshRenderer>().material.color = Color.white;

            if (hasSwung == true)
            {
                hasSwung = false;
            }
            else if (hasFired == true && hasThrown == false)
            {
                Debug.Log(hasFired);
                gunSmoke.SetActive(false);
                hasFired = false;
                Debug.Log(hasFired);

            }
            else if (hasThrown == true)
            {
                Debug.Log(hasThrown);
                GetComponent<MeshRenderer>().enabled = true;
                hasThrown = false;
                Debug.Log(hasThrown);

            }
            // Debug.Log(hasThrown + "after");

            //Debug.Log("Reloaded");
        }
    }

    //void OnDrawGizmosSelected()
    //{
    //     //Draw a yellow sphere at the transform's position
    //     Gizmos.color = Color.black;
    //    // Gizmos.DrawSphere(transform.position, 0.9f);                                  // Sword Swing  
    //    // Gizmos.DrawLine(transform.position, transform.position + transform.up*10);    // Blunder buss 
    //     Gizmos.DrawSphere(transform.position, 10);                                     // Bomb Boom
    //}

    public void Attack(GameObject player)
    {
        if (isCutlass && !hasSwung)
        {




            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5f);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.GetComponent<SoloPlayer>() != null && hitCollider.GetComponent<SoloPlayer>().isHit == false && hitCollider.gameObject != player.gameObject)
                {
                    if (!hitCollider.transform.GetComponent<SoloPlayer>().GetInvul())
                    {
                        hitCollider.GetComponent<Rigidbody>().MovePosition(hitCollider.transform.position + player.transform.forward * 2);
                        hitCollider.GetComponent<SoloPlayer>().StartCoroutine("InvulFrames", 1f);
                        hitCollider.GetComponent<SoloPlayer>().hitPoints -= 1;
                        hasSwung = true;
                        timer = 1.5f;
                    }
                }
            }
        }
        else if (isBlunder && !hasFired)
        {
            //Debug.Log("Fire");
            //GetComponent<Animator>().Play("Bang");
            //Collider[] hitColliders = Physics.OverlapCapsule(transform.position, 0.9f);

            //Old Reliable Hit
            if (bulletPrefab == null)
            {
                RaycastHit hit;
                if (Physics.SphereCast(myPlayer.transform.position + Vector3.up, 2, transform.TransformDirection(Vector3.forward), out hit, 10.0f))
                {
                    if (hit.transform.gameObject.GetComponent<SoloPlayer>() != null)
                    {
                        hit.transform.GetComponent<Rigidbody>().MovePosition(hit.transform.position + player.transform.forward * 2);
                        hit.transform.GetComponent<SoloPlayer>().StartCoroutine("InvulFrames", 3f);
                        hit.transform.GetComponent<SoloPlayer>().hitPoints -= 1;
                    }
                }
            }
            else // New Experimental Projectile
            {
                GameObject bullet = Instantiate(bulletPrefab, myPlayer.transform.position + myPlayer.transform.forward * 2, myPlayer.transform.rotation);
                bullet.GetComponent<Rigidbody>().AddForce(myPlayer.transform.forward * 12, ForceMode.Force);
            }

            hasFired = true;
            myPlayer.GetComponent<SoloPlayer>().reloadStart = hasFired;
            timer = 3.5f;
            myPlayer.GetComponent<SoloPlayer>().reloadStart = hasFired;
            StartCoroutine(GunShot());

            //Debug.Log("Bang");

        }
        else if (isBomb && !hasThrown)
        {

            hasThrown = true;
            timer = 7f;
            GameObject thrownBomb = Instantiate(gameObject, player.transform.position + player.transform.up * 2, player.transform.rotation);
            thrownBomb.AddComponent<Rigidbody>();
            thrownBomb.GetComponent<Rigidbody>().AddForce(player.transform.up * 2 + player.transform.forward * 10, ForceMode.Impulse);
            thrownBomb.GetComponent<MeshCollider>().enabled = true;
            thrownBomb.GetComponent<WeaponSolo>().SetThrown(false);
            thrownBomb.GetComponent<WeaponSolo>().StartCoroutine(Explode(thrownBomb));
            myPlayer.GetComponent<SoloPlayer>().bombStop = hasThrown;
            myPlayer.GetComponent<SoloPlayer>().bombTimer = timer;

            //GetComponent<Animator>().Play("Boom");
            //Collider[] hitColliders = Physics.OverlapSphere(transform.position, 3);
            //foreach (var hitCollider in hitColliders)
            //{
            //    if (hitCollider.GetComponent<MyPlayer>() != null && hitCollider.GetComponent<MyPlayer>().isHit == false)
            //    {
            //        hitCollider.GetComponent<CharacterController>().Move(transform.up * 2);
            //        hitCollider.GetComponent<MyPlayer>().StartCoroutine("InvulFrames");
            //    }
            //}
        }
    }

    public void ReloadFinish()
    {
        Debug.Log(hasFired);
        gunSmoke.SetActive(false);
        hasFired = false;
        Debug.Log(hasFired);
        timer = 0;
    }

    public void ThrowFinish()
    {
        Debug.Log(hasThrown);
        GetComponent<MeshRenderer>().enabled = true;
        hasThrown = false;
        Debug.Log(hasThrown);
        timer = 0;
    }

    public IEnumerator Explode(GameObject bomb)
    {
        GetComponent<MeshRenderer>().material.color = Color.black;
        //Instantiate(bombRadius, transform, true);

        GameObject FuseSpark = Instantiate(fuse, bomb.GetComponent<WeaponSolo>().fusePoint.transform.position,
            bomb.GetComponent<WeaponSolo>().fusePoint.transform.rotation, bomb.GetComponent<WeaponSolo>().fusePoint.transform);
        yield return new WaitForSeconds(2f);
        if (bombRange != null)
            bomb.GetComponent<WeaponSolo>().bombRange.SetActive(true);

        bombPosition = bomb.transform.position;
        GameObject explosionClone = Instantiate(explosion, bombPosition, Quaternion.identity);

        Collider[] colliders = Physics.OverlapSphere(bomb.transform.position, 7.0f);
        foreach (var collider in colliders)
        {
            if (collider.GetComponent<Rigidbody>() != null)
                collider.GetComponent<Rigidbody>().AddExplosionForce(110, bomb.transform.position, 7.5f, 0.1f, ForceMode.Impulse);
            if (collider.GetComponent<SoloPlayer>() != null && collider.GetComponent<SoloPlayer>().isHit == false) //GetComponentInParent<MyPlayer>() == null
            {
                if (!collider.GetComponent<SoloPlayer>().GetInvul())
                {
                    collider.GetComponent<SoloPlayer>().StartCoroutine("InvulFrames", 4f);
                    collider.GetComponent<SoloPlayer>().hitPoints -= 1;
                }
            }
        }
        bomb.GetComponent<MeshRenderer>().enabled = false;

        //hasThrown = false;
        //GetComponent<MeshRenderer>().material.color = Color.white;
        Destroy(FuseSpark);
        Destroy(bomb.GetComponent<WeaponSolo>().bombRange);
        yield return new WaitForSeconds(2f);
        Destroy(explosionClone);     

        Destroy(bomb);
    }

    public IEnumerator GunShot()
    {
        Debug.Log("kek shooty");
        GameObject gunFireClone = Instantiate(gunFire, gunFire.transform.position, gunFire.transform.rotation);
        gunFireClone.SetActive(true);

        yield break;
    }

    public bool HasSwung() { return hasSwung; }
    public void SetSwung(bool a_swing) { hasSwung = a_swing; }
    public bool HasFired() { return hasFired; }
    public void SetFired(bool a_fired) { hasFired = a_fired; }
    public bool HasThrown() { return hasThrown; }
    public void SetThrown(bool a_thrown) { hasThrown = a_thrown; }
    public float Timer() { return timer; }
    public void SetTimer(float a_time) { timer = a_time; }
}
