using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Dice : MonoBehaviour
{
    #region PRIVATE FIELDS

    private Rigidbody rigidbody;

    private Vector3 initialPos;

    #endregion

    #region SERIALIZEFIELDS

    [SerializeField] float speed=5;

    [SerializeField] Button rollButton;

    #endregion

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();

        rigidbody.useGravity = false;

        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RollDice()
    {
        rollButton.interactable = false;

        StartCoroutine(Roll());

        rigidbody.useGravity = true;
    }

    IEnumerator Roll()
    {
        float duration = 2f;

        float time = 0;

        Vector3 rot = new Vector3(Random.Range(75, 360), Random.Range(75, 360), Random.Range(75, 360));

        while(time < duration)
        {
            time += Time.deltaTime;

            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, rot, time* speed);

            yield return null;
        }

        yield return new WaitForSeconds(2);

        StartCoroutine(BackToPosition());
    }

    IEnumerator BackToPosition()
    {

        rigidbody.useGravity = false;

        transform.eulerAngles = Vector3.zero;

        float duration = 0.5f;

        float time = 0;

        while(time < duration)
        {
            time += Time.deltaTime;

            transform.position = Vector3.Lerp(transform.position, initialPos, time * speed);

            yield return null;
        }

        rollButton.interactable = true;
        
    }

    [PunRPC]
    void SendScore()
    {

    }
}
