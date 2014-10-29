using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class Algorithm
{
		public enum Fittest { MOST_USED, MOST_THREE_USED };
		public enum Genetic { RANDOM, UNICELLULAR };

		public Fittest FittestAlgo;
		public Genetic GeneticAlgo;
}