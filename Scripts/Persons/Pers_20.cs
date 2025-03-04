using UnityEngine;

public class Pers_20 : MonoBehaviour
{
    private AnyPerson pers;
    private const float ANGRY = 55f;
    private const float SOLONG = 45f;
    private const float WAITING = 30f;
    private readonly float bonusPayLimit = 6;
    private readonly int procentOfGoodPeople = 96;
    private readonly float minSpeed = 2.5f;
    private readonly float maxSpeed = 2.9f;
    private readonly float runSpeed = 6.1f;
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
            pers.RandomWish(Random.Range(1, 3), 2);
            pers.RandomWish(Random.Range(0, 3), 2);
            pers.RandomWish(Random.Range(5, 6), 2);
            pers.RandomWish(Random.Range(3, 6), 2);
        }
    }
}
