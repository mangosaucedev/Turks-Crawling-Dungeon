using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public static class GameplayStateFactory
    {
        public static GameplayState Loading() => new Loading();

        public static GameplayState InView() => new InView();

        public static GameplayState InCinematic() => new InCinematic();

        public static GameplayState Gameplay() => new Gameplay();

        public static GameplayState InAction() => new InAction();
    }
}
