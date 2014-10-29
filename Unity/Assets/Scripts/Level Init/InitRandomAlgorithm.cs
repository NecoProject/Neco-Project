using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InitRandomAlgorithm : MonoBehaviour
{
		public Image AlgorithmIcon;

		public Algorithm Algorithm;

		void Start()
		{
				// TODO: more algos!
				Algorithm = new Algorithm();
				Algorithm.FittestAlgo = Algorithm.Fittest.MOST_THREE_USED;
				Algorithm.GeneticAlgo = Algorithm.Genetic.UNICELLULAR;
		}
}
