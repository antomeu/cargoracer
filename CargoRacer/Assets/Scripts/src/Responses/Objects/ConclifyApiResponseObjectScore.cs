// 
// ConclifyApiResponseObjectScore.cs
//  
// Contains the class to represent a score object within an API response
// 
// Author:		Jon Massey
// Copyright:   Conclify 2017
// Company:     Conclify
// Since:		29/04/2017
// ================================================================================================

using Conclify.Game;
using TinyJSON;

namespace Conclify.Responses.Objects
{
	/// <summary>
	/// Class to represent a score object within an API response
	/// </summary>
	public class ConclifyApiResponseObjectScore
	{
		//Constants
		//---------------------------------------------------------------------

		/// <summary>Name of the 'rank' json property</summary>
		private const string Property_Rank = "rank";

		/// <summary>Name of the 'name' json property</summary>
		private const string Property_Name = "name";

		/// <summary>Name of the 'score' json property</summary>
		private const string Property_Score = "score";

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
		/// Creates a new <code>ConclifyApiResponseObjectScore</code> instance
		/// </summary>
		/// <param name="jsonObject">Json object to retrieve fields from</param>
		/// <param name="gameType">Game-type to use to determine if optional properties exist or not</param>
		internal ConclifyApiResponseObjectScore(Variant jsonObject, ConclifyApiGameTypes gameType)
		{
			//Retrieve Fields
			rank = jsonObject[Property_Rank];
			name = jsonObject[Property_Name];
			score = jsonObject[Property_Score];
		}
	}
}
// ================================================================================================