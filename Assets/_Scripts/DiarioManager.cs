using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiarioManager : MonoBehaviour
{
    public GameObject diario;
    public HashSet<Pista> pistas;
    public DialogueManager dm;
    public GameObject slot;
    public GameObject parent;
    public HashSet<GameObject> slots;
    void Start()
    {
        slots = new HashSet<GameObject>();
        //pistas = new HashSet<Pista>();
        //Pista pista = ScriptableObject.CreateInstance<Pista>();

        //Pista pista2 = ScriptableObject.CreateInstance<Pista>();

        //Pista pista3 = ScriptableObject.CreateInstance<Pista>();
        //pistas.Add(pista);
        //pistas.Add(pista2);
        //pistas.Add(pista3);
        //pistas.Add(pista);
    }

    // Update is called once per frame
    void Update()
    {
        pistas = dm.pistas;
        if (diario.activeSelf)
        {
            if (Input.GetKeyDown("e"))
            {
                diario.SetActive(false);
                DeletePistas();
            }
                
        }
        else
        {
            if (Input.GetKeyDown("e"))
            {
                UpdatePistas();
                diario.SetActive(true);
            }
                
        }
    }

    void UpdatePistas()
    {
        foreach (var pista in pistas)
        {
            GameObject slotPista = Instantiate(slot, parent.transform);

            slotPista.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = pista.sprite;
            slotPista.transform.GetChild(2).gameObject.GetComponent<Text>().text = pista.titulo;
            slotPista.transform.GetChild(3).gameObject.GetComponent<Text>().text = pista.tipo;
            slotPista.GetComponent<PistaScript>().pista = pista;

            slots.Add(slotPista);
        }

        int i = 0;
        foreach (var slot in slots)
        {
            if(slot != null)
            {
                slot.SetActive(true);
                slot.transform.position = new Vector3(slot.transform.position.x, slot.transform.position.y + i, 0);
                i -= 60;
            }
        }
    }

    void DeletePistas()
    {
        foreach (var slot in slots)
        {
            if(slot != null)
                Destroy(slot);
        }
    }
}
