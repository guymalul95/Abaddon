using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum FireMode
{
    Auto,
    Semi
}

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioSource))]
public class Weapon : MonoBehaviour {

    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 1.5f;
    public FireMode fireMode = FireMode.Auto;
    public Camera playerCamera;
    public int maxRoundsInClip = 30;
    public bool weaponEnabled = true;

    private float nextTimeToFire = 0;
    private int currentRoundsInClip;
    private float reloadTime = 2f;
    private FireMode chosenFireMode;

    public ParticleSystem muzzleFlashParticles;
    public Text ClipText;
    
    public float minimumBulletSpread = 0.8f;
    public float maximumBulletSpread = 1.8f;
    private float currentBulletSpread;
    private float bulletSpreadRatio = 0.1f;
    private bool isTriggerHeld;
    private AudioSource shootAudioPlayer;
    private AudioSource emptyAudioPlayer;
    private AudioSource reloadAudioPlayer;

    public GameObject bulletImpactPrefab_Metal;
    public GameObject bulletImpactPrefab_Wood;
    public GameObject bulletImpactPrefab_Concrete;

    private void Start()
    {
        muzzleFlashParticles.Stop();
        currentBulletSpread = minimumBulletSpread;
        isTriggerHeld = false;
        Random.InitState(System.Environment.TickCount);

        var audioSources = GetComponents<AudioSource>();

        shootAudioPlayer = audioSources[0];
        shootAudioPlayer.loop = false;

        emptyAudioPlayer = audioSources[1];
        emptyAudioPlayer.loop = false;

        reloadAudioPlayer = audioSources[2];
        reloadAudioPlayer.loop = false;

        currentRoundsInClip = maxRoundsInClip;
        chosenFireMode = fireMode;

        bulletSpreadRatio = (maximumBulletSpread - minimumBulletSpread) * 0.1f;

        UpdateGUI();
    }

    private void FixedUpdate()
    {
        if (isTriggerHeld && currentBulletSpread < maximumBulletSpread)
        {
            currentBulletSpread = Mathf.Min(maximumBulletSpread, currentBulletSpread + bulletSpreadRatio);
        }
        else if (!isTriggerHeld && currentBulletSpread > minimumBulletSpread)
        {
            currentBulletSpread = Mathf.Max(minimumBulletSpread, currentBulletSpread - (bulletSpreadRatio * 3));
        }
    }

    // Update is called once per frame
    void Update ()
    {
        // fire
        if (Time.time >= nextTimeToFire)
        {
            if (fireMode == FireMode.Auto)
            {
                if (Input.GetButtonDown("Fire1") || isTriggerHeld)
                {
                    isTriggerHeld = true;
                    nextTimeToFire = Time.time + 1f / fireRate;
                    Fire();
                }
            }
            else
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    nextTimeToFire = Time.time + 1f / fireRate;
                    Fire();
                }
            }
        }

        // release trigger
        if (Input.GetButtonUp("Fire1"))
        {
            isTriggerHeld = false;
        }

        // reload
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

        // switch fire mode
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (fireMode == FireMode.Auto)
            {
                chosenFireMode = FireMode.Semi;
            }
            else if (fireMode == FireMode.Semi)
            {
                chosenFireMode = FireMode.Auto;
            }

            fireMode = chosenFireMode;
        }
    }

    void Fire()
    {
        if (weaponEnabled)
        {
            if (currentRoundsInClip <= 0)
            {
                fireMode = FireMode.Semi;
                emptyAudioPlayer.Play();
            }
            else
            {
                float xSpread = Random.Range(-1f, 1f) + 0.01f;
                float ySpread = Random.Range(-1f, 1f) + 0.01f;
                float zSpread = Random.Range(-1f, 1f) + 0.01f;

                Vector3 spread = new Vector3(xSpread, ySpread, zSpread).normalized * currentBulletSpread;

                muzzleFlashParticles.Play();
                shootAudioPlayer.Play();

                Quaternion rotation = Quaternion.Euler(spread);
                Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, rotation, Vector3.one);

                // rotate from the forward direction
                Vector3 direction = matrix.MultiplyPoint(playerCamera.transform.forward);

                RaycastHit hit;
                if (Physics.Raycast(playerCamera.transform.position, direction, out hit, range))
                {
                    GameObject bulletImpactPrefab = null;
                    string tag = hit.collider.gameObject.tag.ToLower();

                    switch (tag)
                    {
                        case "enemy":
                        case "metal":
                            {
                                bulletImpactPrefab = bulletImpactPrefab_Metal;
                                break;
                            }
                        case "wood":
                            {
                                bulletImpactPrefab = bulletImpactPrefab_Wood;
                                break;
                            }
                        case "concrete":
                            {
                                bulletImpactPrefab = bulletImpactPrefab_Concrete;
                                break;
                            }
                        case "player":
                            return;
                    }

                    Target target = hit.transform.GetComponent<Target>();
                    if (target != null)
                    {
                        target.TakeDamage(damage);
                    }

                    if (bulletImpactPrefab != null)
                    {
                        GameObject impact = Instantiate(bulletImpactPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                        impact.transform.parent = hit.collider.transform;

                        Destroy(impact, 2.5f);
                    }
                }

                --currentRoundsInClip;
                UpdateGUI();
            }
        }
    }

    void Reload()
    {
        weaponEnabled = false;
        reloadAudioPlayer.Play();
        StartCoroutine(ReloadDone());
    }

    void UpdateGUI()
    {
        ClipText.text = string.Format("{0} / {1}", currentRoundsInClip, maxRoundsInClip);
    }

    IEnumerator ReloadDone()
    {
        yield return new WaitForSeconds(reloadTime);
        weaponEnabled = true;
        currentRoundsInClip = maxRoundsInClip;
        fireMode = chosenFireMode;
        UpdateGUI();
    }
}
