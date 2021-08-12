using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    public class ScoreOnDeath : Part
    {
        [SerializeField] private int score;

        public override string Name => "Score on Death";

        public int Score
        {
            get => score;
            set => score = value;
        }

        public override bool HandleEvent<T>(T e)
        {
            if (e.Id == DestroyObjectEvent.id)
                ScoreHandler.ModifyScore(Score);
            return base.HandleEvent(e);
        }
    }
}
