using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    [Obsolete]
    public class Tiling3x3 : Tiling
    {
        [Header("Sprites")]
        [SerializeField] private Sprite upperLeft;
        [SerializeField] private Sprite upperMiddle;
        [SerializeField] private Sprite upperRight;
        [SerializeField] private Sprite centerLeft;
        [SerializeField] private Sprite center;
        [SerializeField] private Sprite centerRight;
        [SerializeField] private Sprite lowerLeft;
        [SerializeField] private Sprite lowerMiddle;
        [SerializeField] private Sprite lowerRight;
        [SerializeField] private Sprite up;
        [SerializeField] private Sprite down;
        [SerializeField] private Sprite left;
        [SerializeField] private Sprite right;
        [SerializeField] private Sprite horizontal;
        [SerializeField] private Sprite vertical;
        [SerializeField] private Sprite single;

        [Header("Sprite Names")]
        [SerializeField] private string upperLeftName;
        [SerializeField] private string upperMiddleName;
        [SerializeField] private string upperRightName;
        [SerializeField] private string centerLeftName;
        [SerializeField] private string centerName;
        [SerializeField] private string centerRightName;
        [SerializeField] private string lowerLeftName;
        [SerializeField] private string lowerMiddleName;
        [SerializeField] private string lowerRightName;
        [SerializeField] private string upName;
        [SerializeField] private string downName;
        [SerializeField] private string leftName;
        [SerializeField] private string rightName;
        [SerializeField] private string horizontalName;
        [SerializeField] private string verticalName;
        [SerializeField] private string singleName;

        public string UpperLeft
        {
            get => upperLeftName;
            set => upperLeftName = value;
        }

        public string UpperMiddle
        {
            get => upperMiddleName;
            set => upperMiddleName = value;
        }

        public string UpperRight
        {
            get => upperRightName;
            set => upperRightName = value;
        }

        public string CenterLeft
        {
            get => centerLeftName;
            set => centerLeftName = value;
        }

        public string Center
        {
            get => centerName;
            set => centerName = value;
        }

        public string CenterRight
        {
            get => centerRightName;
            set => centerRightName = value;
        }

        public string LowerLeft
        {
            get => lowerLeftName;
            set => lowerLeftName = value;
        }

        public string LowerMiddle
        {
            get => lowerMiddleName;
            set => lowerMiddleName = value;
        }

        public string LowerRight
        {
            get => lowerRightName;
            set => lowerRightName = value;
        }

        public string Up
        {
            get => upName;
            set => upName = value;
        }

        public string Down
        {
            get => downName;
            set => downName = value;
        }

        public string Left
        {
            get => leftName;
            set => leftName = value;
        }

        public string Right
        {
            get => rightName;
            set => rightName = value;
        }

        public string Horizontal
        {
            get => horizontalName;
            set => horizontalName = value;
        }

        public string Vertical
        {
            get => verticalName;
            set => verticalName = value;
        }

        public override string Single
        {
            get => singleName;
            set => singleName = value;
        }

        public override string Name => "Tiling 3x3";

        protected override void Start()
        {
            base.Start();
            InitializeSprites();
            UpdateSprite();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            isDisabled = false;
            UpdateSurroundingSprites();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            isDisabled = true;
            UpdateSurroundingSprites();
        }

        private void InitializeSprites()
        {
            upperLeft = Assets.Get<Sprite>(UpperLeft);
            upperMiddle = Assets.Get<Sprite>(UpperMiddle);
            upperRight = Assets.Get<Sprite>(UpperRight);
            centerLeft = Assets.Get<Sprite>(CenterLeft);
            center = Assets.Get<Sprite>(Center);
            centerRight = Assets.Get<Sprite>(CenterRight);
            lowerLeft = Assets.Get<Sprite>(LowerLeft);
            lowerMiddle = Assets.Get<Sprite>(LowerMiddle);
            lowerRight = Assets.Get<Sprite>(LowerRight);
            up = Assets.Get<Sprite>(Up);
            down = Assets.Get<Sprite>(Down);
            left = Assets.Get<Sprite>(Left);
            right = Assets.Get<Sprite>(Right);
            horizontal = Assets.Get<Sprite>(Horizontal);
            vertical = Assets.Get<Sprite>(Vertical);
            single = Assets.Get<Sprite>(Single);
        }

        protected override void UpdateSurroundingSprites()
        {
            UpdateTilingEvent e = LocalEvent.Get<UpdateTilingEvent>();
            e.tiling = this;
            UpdateOtherSprite(X, Y + 1, e);
            UpdateOtherSprite(X, Y - 1, e);
            UpdateOtherSprite(X - 1, Y, e);
            UpdateOtherSprite(X + 1, Y, e);    
        }

        private void UpdateOtherSprite(int x, int y, UpdateTilingEvent e)
        {
            GameGrid grid = CurrentZoneInfo.grid;
            if (!grid.IsWithinBounds(x, y))
                return;
            Cell cell = grid[x, y];
            FireEvent(cell, e);
        }

        public override bool HandleEvent<T>(T e)
        {
            if (e.Id == UpdateTilingEvent.id)
                UpdateSprite();
            return base.HandleEvent(e);
        }

        private void UpdateSprite()
        {
            bool above = HasTilingObject(X, Y + 1);
            bool below = HasTilingObject(X, Y - 1);
            bool toLeft = HasTilingObject(X - 1, Y);
            bool toRight = HasTilingObject(X + 1, Y);
            Render render = parent.parts.Get<Render>();

            if (above && below && toLeft && toRight)
                render.SetSprite(center);
            else if (above && below && toLeft)
                render.SetSprite(centerRight);
            else if (above && below && toRight)
                render.SetSprite(centerLeft);
            else if (above && toLeft && toRight)
                render.SetSprite(lowerMiddle);
            else if (below && toLeft && toRight)
                render.SetSprite(upperMiddle);
            else if (above && toLeft)
                render.SetSprite(lowerRight);
            else if (above && toRight)
                render.SetSprite(lowerLeft);
            else if (below && toLeft)
                render.SetSprite(upperRight);
            else if (below && toRight)
                render.SetSprite(upperLeft);
            else if (above && below)
                render.SetSprite(vertical);
            else if (toLeft && toRight)
                render.SetSprite(horizontal);
            else if (above)
                render.SetSprite(down);
            else if (below)
                render.SetSprite(up);
            else if (toLeft)
                render.SetSprite(right);
            else if (toRight)
                render.SetSprite(left);
            else
                render.SetSprite(single);
        }

        private bool HasTilingObject(int x, int y)
        {
            GameGrid grid = CurrentZoneInfo.grid;
            if (!grid.IsWithinBounds(x, y))
                return false;
            Cell cell = grid[x, y];
            return (cell.Contains(out Tiling3x3 tiling) && !tiling.isDisabled && Single == tiling.Single);
        }
    }
}
