using UnityEngine;

public class Pers_40 : MonoBehaviour
{
    private AnyPerson pers;
    private const float ANGRY = 50f;
    private const float SOLONG = 40f;
    private const float WAITING = 25f;
    private readonly float bonusPayLimit = 10;
    private readonly int procentOfGoodPeople = 92;
    private readonly float minSpeed = 2.7f;
    private readonly float maxSpeed = 3.1f;
    private readonly float runSpeed = 6.3f;
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
            pers.RandomWish(Random.Range(1, 6), 4);
            pers.RandomWish(Random.Range(3, 6), 2);
            pers.RandomWish(Random.Range(3, 6), 2);
        }
    }
}
