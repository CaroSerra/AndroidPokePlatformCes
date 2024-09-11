using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MongoPokePlatform;

public class GetData : MonoBehaviour
{
    public TMP_Text currentScore;
    public TMP_Text dbScore;

    // Start is called before the first frame update
    void Start()
    {
        currentScore.text = StaticData.score.ToString();
        var records = Db.GetHighestScores(1);
        dbScore.text = records[0].Score.ToString();
        Debug.Log(records[0].Score);
    }
}
