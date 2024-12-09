using FMOD.Studio;
using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private EventInstance ambienceEvent;
    private EventInstance dangerEvent;
    [SerializeField] GameObject player;
    [SerializeField] float walkSpeed = 0.6f;
    [SerializeField] private CharacterController characterController;
    private bool playedStep = false;

    private enum CURRENT_TERRAIN { Concrete, Metal, Tile };
    private CURRENT_TERRAIN currentTerrain;

    private EventInstance footstep; 
    private EventInstance enemyFootstep;
    private EventInstance enemyDamaged;
    private EventInstance playerDamaged;
    private EventInstance swing;

    float time = 0.0f;

    private bool wasInAir = false;

    public static Action PlayAmbience;
    public static Action PlayDanger;
    
    // Start is called before the first frame update
    void Start()
    {
        ambienceEvent = RuntimeManager.CreateInstance("event:/Ambience");
        dangerEvent = RuntimeManager.CreateInstance("event:/Danger");
        PlayAmbience += OnPlayAmbience;
        PlayDanger += OnPlayDanger;
        ambienceEvent.start();
    }

    // Update is called once per frame
    void Update()
    {
        AirCheck();
        if (characterController.isGrounded)
        {
            if (characterController.velocity.magnitude > 0)
            {
                if (time > walkSpeed)
                {
                    DetermineTerrain();
                    SelectAndPlayFootstep();
                    time = 0;
                }
                time += Time.deltaTime;

            }
            else if (wasInAir)
            {
                DetermineTerrain();
                SelectAndPlayFootstep();
                wasInAir = false;
            }
        }

    }

    public void AirCheck()
    {

        if (characterController.velocity.y != 0)
        {
            wasInAir = true;
        }
    }

   
    public void OnPlayAmbience() 
    {
        ambienceEvent.setPaused(false);
        dangerEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    // ������� ������ ������
    public void OnPlayDanger()
    {
        ambienceEvent.setPaused(true);
        dangerEvent.start();
    }

    // ����������� �����������, �� ������� ����� �����, � ������� ��������, ������������� ����, �� ���� �� ����������� ����
    public void DetermineTerrain()
    {
        RaycastHit[] hit;

        hit = Physics.RaycastAll(player.transform.position, Vector3.down, 10.0f);

        foreach (RaycastHit rayhit in hit)
        {
            if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Metal"))
            {
                currentTerrain = CURRENT_TERRAIN.Metal;
            }
            else if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Tile"))
            {
                currentTerrain = CURRENT_TERRAIN.Tile;
            }
            else if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Concrete"))
            {
                currentTerrain = CURRENT_TERRAIN.Concrete;
            }
        }
    }

    // ����� �����
    public void SelectAndPlayFootstep()
    {
        switch (currentTerrain)
        {
            case CURRENT_TERRAIN.Tile:
                PlayFootstep(0);
                break;

            case CURRENT_TERRAIN.Concrete:
                PlayFootstep(2);
                break;

            case CURRENT_TERRAIN.Metal:
                PlayFootstep(1);
                break;

            default:
                PlayFootstep(0);
                break;
        }
    }

    // �������� ������� �������� �����������, �� �������� ����� �����, � ����������� ���������� ����
    public void PlayFootstep(int terrain)
    {
        footstep = RuntimeManager.CreateInstance("event:/Footsteps");
        footstep.setParameterByName("Terrain", terrain);
        footstep.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        footstep.start();
        footstep.release();
    }

    public void PlayEnemyFootstep()
    {
        enemyFootstep = RuntimeManager.CreateInstance("event:/Enemy Walking");
        enemyFootstep.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        enemyFootstep.start();
        enemyFootstep.release();
    }

    public void PlayEnemyDamaged()
    {
        enemyDamaged = RuntimeManager.CreateInstance("event:/Enemy Damaged");
        enemyDamaged.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        enemyDamaged.start();
        enemyDamaged.release();
    }

    public void PlayDamaged()
    {
        playerDamaged = RuntimeManager.CreateInstance("event:/Player Damaged");
        playerDamaged.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        playerDamaged.start();
        playerDamaged.release();
    }

    public void PlaySwing()
    {
        swing = RuntimeManager.CreateInstance("event:/Melee Swing");
        swing.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        swing.start();
        swing.release();
    }

   
    


}
