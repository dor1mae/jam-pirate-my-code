
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;
public class AttackController : MonoBehaviour 
{
    private StaminaController _staminaController;
    
    [SerializeField] private WeaponConfig _weaponConfig;
    [SerializeField] private GameObject _weaponObject; //то, чьими анимациями мы будем управлять

    private readonly Vector2 Center = new Vector2(Screen.width / 2, Screen.height / 2);
    private bool _isAnimation = false;
    private Animator _animator;

    private System.Random _random;

    private void Start()
    {
        _random = new System.Random();
    }

    public void SetParams(StaminaController staminaController)
    {
        _staminaController = staminaController;
    }

    public void AttackAction(InputAction.CallbackContext context)
    {
        _animator = _weaponObject.GetComponentInChildren<Animator>(); // получаем аниматор объекта в руке, если такой есть
        if (context.started && !_isAnimation && _staminaController.TryAction(_weaponConfig.StaminaCost) && _animator && !UIManager.Instance.IsOpened)
        {
            _staminaController.RemoveStamina(_weaponConfig.StaminaCost);

            StartCoroutine(OnAnimation());

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Center);

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "Enemy")
            {
                var health = hit.collider.gameObject.GetComponent<HealthController>();
                health.RemoveHealth(_weaponConfig.Damage);
                Debug.Log($"Нанесено {_weaponConfig.Damage} урона");
            }
        }
    }

    private IEnumerator OnAnimation()
    {
        _isAnimation = true;

        int anim = _random.Next(0, 2);
        //Debug.Log(anim);
        string animationName = null;
        switch (anim)
        {
            case 0:
                _animator.SetTrigger("Attack");

                AttackAnimationStateHandler.AnimationEnd += (name) =>
                {
                    animationName = name;
                    //Debug.Log(name);
                };

                yield return new WaitUntil(() => animationName == "Attack");

                _isAnimation = false;
                break;

            case 1:
                _animator.SetTrigger("Attack2");

                AttackAnimationStateHandler.AnimationEnd += (name) =>
                {
                    animationName = name;
                    //Debug.Log(name);
                };

                yield return new WaitUntil(() => animationName == "Attack2");

                _isAnimation = false;
                break;
        }
    }
}