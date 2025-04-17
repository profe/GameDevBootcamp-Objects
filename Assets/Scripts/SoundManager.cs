using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _backgroundMusic, _playerSFX, _enemySFX;
    [SerializeField] private AudioClip _clipPlayerShoot, _clipPlayerHurt, _clipEnemyDestroyed, _clipMeleeEnemyShoot, _clipMachineGunEnemyShoot, _clipShooterEnemyShoot, _clipReset;

    public void PlaySound(Sound sound)
    {
        switch (sound)
        {
            case Sound.PlayerShoot:
                _playerSFX.PlayOneShot(_clipPlayerShoot);
                break;
            case Sound.PlayerHurt:
                _playerSFX.PlayOneShot(_clipPlayerHurt);
                break;
            case Sound.EnemyDestroyed:
                _enemySFX.PlayOneShot(_clipEnemyDestroyed);
                break;
            case Sound.MeleeEnemyShoot:
                _enemySFX.PlayOneShot(_clipMeleeEnemyShoot);
                break;
            case Sound.MachineGunEnemyShoot:
                _enemySFX.PlayOneShot(_clipMachineGunEnemyShoot);
                break;
            case Sound.ShooterEnemyShoot:
                _enemySFX.PlayOneShot(_clipShooterEnemyShoot);
                break;
            case Sound.ResetGame:
                //pause background music
                _backgroundMusic.Stop();
                //play reset clip
                _playerSFX.PlayOneShot(_clipReset);
                //start background music
                _backgroundMusic.PlayDelayed(_clipReset.length);
                break;
        }
    }
}
