using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    [Serializable]
    public class Butcherable : Part
    {
        [SerializeField] private string firstProduct;
        [SerializeField] private string firstProductCount;
        [SerializeField] private string secondProduct;
        [SerializeField] private string secondProductCount;
        [SerializeField] private string thirdProduct;
        [SerializeField] private string thirdProductCount;

        public string FirstProduct
        {
            get => firstProduct;
            set => firstProduct = value;
        }

        public string FirstProductCount
        {
            get => firstProductCount;
            set => firstProductCount = value;
        }

        public string SecondProduct
        {
            get => secondProduct;
            set => secondProduct = value;
        }

        public string SecondProductCount
        {
            get => secondProductCount;
            set => secondProductCount = value;
        }

        public string ThirdProduct
        {
            get => thirdProduct;
            set => thirdProduct = value;
        }

        public string ThirdProductCount
        {
            get => thirdProductCount;
            set => thirdProductCount = value;
        }

        public override string Name => "Butcherable";

        protected override void GetInteractions(GetInteractionsEvent e)
        {
            base.GetInteractions(e);
            e.interactions.Add(new Interaction("Butcher", StartButchering));
        }

        private void StartButchering()
        {
            StopAllCoroutines();
            StartCoroutine(StartButcheringRoutine());
        }

        private IEnumerator StartButcheringRoutine()
        {
            yield return null;
        }
    }
}
