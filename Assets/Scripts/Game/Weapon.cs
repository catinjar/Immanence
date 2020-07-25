using UnityEngine;

public class Weapon : MonoBehaviour {
    public GameObject bulletPrefab;
    public RandomAudioSource shotSound;

    public int maxAmmo = 5;
    public int ammo;

    public readonly float reloadTime = 1.0f;
    public float currentReloadTime = 0.0f;

    public readonly float pauseTime = 0.5f;
    public float currentPauseTime = 0.0f;

    private void Start() {
        ammo = maxAmmo;
    }

    private void Update() {
        currentReloadTime = UpdateWeaponTime(currentReloadTime, reloadTime);
        currentPauseTime = UpdateWeaponTime(currentPauseTime, pauseTime);

        if (!MouseMode.Instance.IsWeapon()) {
            return;
        }

        if (currentReloadTime != 0.0f || currentPauseTime != 0.0f) {
            return;
        }

        if (Input.GetButtonDown("Fire1")) {
            var bullet = Instantiate(bulletPrefab);

            float direction = PlayerState.Instance.flipped ? -1.0f : 1.0f;
            bullet.transform.position = transform.position + new Vector3(direction * 5.0f, 5.0f);
            bullet.GetComponent<Bullet>().direction = direction;

            --ammo;

            currentPauseTime = 0.001f;

            if (ammo == 0) {
                ammo = maxAmmo;
                currentReloadTime = 0.001f;
            }

            GetComponent<Player>().Stop();

            shotSound.Play();
        }
    }

    private float UpdateWeaponTime(float time, float maxTime) {
        if (time > 0.0f) {
            time += Time.deltaTime;

            if (time > maxTime) {
                time = 0.0f;
            }
        }

        return time;
    }
}
