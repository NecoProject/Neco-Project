using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class Algorithm
{
		public enum Fittest { MOST_USED };
		public enum Genetic { RANDOM };

		public Fittest FittestAlgo;
		public Genetic GeneticAlgo;
}