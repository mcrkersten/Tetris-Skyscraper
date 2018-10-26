﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maskirovka
{

	public class Russia : MonoBehaviour 
	{
		public List<Neighbour> neighbours;
		private Color color;

				
		private void Start () {
			color = GetComponent<SpriteRenderer>().color;
		}


		public bool InvadeCountry(Country country) {
			bool isNeighbour = neighbours.FindAll(x => x.neighbour ==  country).Count > 0;
			
			if(isNeighbour == true) {
				country.Invaded(this);
			}
			return isNeighbour;
		}


		public bool IsNeighbour(Country country) {
			return neighbours.FindAll(x => x.neighbour == country).Count > 0;
		}


		public void AddNeighbours(Neighbour[] newNeighbours) {
			foreach(Neighbour n in newNeighbours) {
				if(neighbours.FindIndex(x => x.neighbour == n.neighbour) == -1) {
					neighbours.Add(n);
				}
			}
		}


        public Color GetColor() {
            return color;
        }
    }
}