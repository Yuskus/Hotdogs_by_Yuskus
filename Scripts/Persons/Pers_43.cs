using UnityEngine;

public class Pers_43 : MonoBehaviour
{
    private AnyPerson pers;
    private const float ANGRY = 45f;
    private const float SOLONG = 35f;
    private const float WAITING = 20f;
    private readonly float bonusPayLimit = 11;
    private readonly int procentOfGoodPeople = 90;
    private readonly float minSpeed = 2.8f;
    private readonly float maxSpeed = 3.1f;
    private readonly float runSpeed = 6.4f;
    private void Awake()
    {
        pers = GetComponent<AnyPerson>();
    }
    private void OnEnable()
    {
        pers.AlwaysAtStart(Random.Range(minSpeed, maxSpeed), runSpeed, bonusPayLimit, procentOfGoodPeople);
        Wish();
        pers.TheClientStartedWalking();
    }
    private void Update()
    {
        pers.Homecoming();
        if (pers.RandomIndex == 6) { return; }
        pers.StoppedOrNot();
        AngryOrNot();
    }
    private void FixedUpdate()
    {
        pers.Walking();
    }
    private void AngryOrNot()
    {
        switch (pers.Timer)
        {
            case > ANGRY: pers.ForAngryClient(); break;
            case > SOLONG: pers.ForWaitingClient(3,2, 2); break;
            case > WAITING: pers.ForWaitingClient(5,4, 1); break;
        }
    }
    private void Wish()
    {
        if (pers.RandomIndex != 6)
        {
            pers.RandomWish(Random.Range(1, 2), 8);
            pers.RandomWish(Random.Range(1, 3), 8);
            pers.RandomWish(Random.Range(1, 6), 8);
            pers.RandomWish(Random.Range(3, 6), 2);
            pers.RandomWish(Random.Range(9, 11), 2);
        }
    }
}
