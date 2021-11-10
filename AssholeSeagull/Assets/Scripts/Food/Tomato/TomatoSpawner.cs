using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoSpawner : MonoBehaviour
{
    [SerializeField] private Tomato tomato;
    [SerializeField] private int tomatoPoolSize = 5;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform tomatoParent;

    private List<Tomato> tomatoPool = new List<Tomato>();

    private Tomato activeTomato;

    void Start()
    {
        CreateTomatoPool();
        HandleSpawnTomato();
    }

    private void CreateTomatoPool()
    {
        Tomato tmp;
        for (int i = 0; i < tomatoPoolSize; i++)
        {
            tmp = Instantiate(tomato, tomatoParent);
            tmp.gameObject.SetActive(false);
            tomatoPool.Add(tmp);
        }
    }

    public void SpawnNewTomato(Tomato tomato)
    {
        if (tomato == activeTomato)
        {
            HandleSpawnTomato();
        }
    }

    private void HandleSpawnTomato()
    {
        foreach (var tomato in tomatoPool)
        {
            if (tomato.gameObject.activeSelf)
            {
                continue;
            }
            else
            {
                SpawnTomato(tomato);
                return;
            }
        }
        Tomato tmp = Instantiate(tomato);
        SpawnTomato(tmp);
        tomatoPool.Add(tmp);
    }

    private void SpawnTomato(Tomato tomato)
    {
        activeTomato = tomato;
        tomato.transform.position = spawnPoint.position;
        tomato.transform.rotation = spawnPoint.rotation;
        tomato.KinematicToggle(true);
        tomato.gameObject.SetActive(true);
    }
}
