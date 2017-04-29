// 
// ConclifyApiGameScore.cs
//  
// Contains the class to represent a score for a game
// 
// Author:		Jon Massey
// Copyright:   Conclify 2017
// Company:     Conclify
// Since:		29/04/2017
// ================================================================================================

using System;

namespace Conclify.Game
{
	/// <summary>
	/// Class to represent a score for a game
	/// </summary>
	[Serializable]
	public class ConclifyApiGameScore
	{
		//Fields
		//---------------------------------------------------------------------

		/// <summary>Rank to display for the score</summary>
		private string rank = null;

		/// <summary>Name to display for the score</summary>
		private string name = null;

		/// <summary>Score display value</summary>
		private string score = null;

		//Properties
		//---------------------------------------------------------------------

		/// <summary>Rank to display for the score</summary>
		public string Rank
		{ get { return rank; } }

		/// <summary>Name to display for the score</summary>
		public string Name
		{ get { return name; } }

		/// <summary>Score display value</summary>
		public string Score
		{ get { return score; } }

		//Constructor
		//---------------------------------------------------------------------

		/// <summary>
		/// Creates a new <code>ConclifyApiGameScore</code> instance
		/// </summary>
		/// <param name="scoreRank">Rank to display for the score</param>
		/// <param name="scoreName">Name to display for the score</param>
		/// <param name="scoreScore">Score display value</param>
		internal ConclifyApiGameScore(string scoreRank, string scoreName, string scoreScore)
		{
			//Store Fields
			rank = scoreRank;
			name = scoreName;
			score = scoreScore;
		}
	}
}
// ================================================================================================