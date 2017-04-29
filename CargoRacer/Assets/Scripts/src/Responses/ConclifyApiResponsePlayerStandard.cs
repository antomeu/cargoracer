// 
// ConclifyApiResponsePlayerStandard.cs
//  
// Contains the class to represent a player-standard API response
// 
// Author:		Jon Massey
// Copyright:   Conclify 2017
// Company:     Conclify
// Since:		29/04/2017
// ================================================================================================

using System;
using Conclify.Game;
using TinyJSON;

namespace Conclify.Responses
{
	/// <summary>
	/// Class to represent a player-standard API response
	/// </summary>
	public class ConclifyApiResponsePlayerStandard : ConclifyApiResponse
	{
		//Constants
		//---------------------------------------------------------------------

		/// <summary>Name of the 'playerId' json property</summary>
		private const string Property_PlayerId = "playerId";

		/// <summary>Name of the 'tokens' json property</summary>
		private const string Property_Tokens = "tokens";

		/// <summary>Name of the 'tokensToThreshold' json property</summary>
		private const string Property_TokensToThreshold = "tokensToThreshold";

		/// <summary>Name of the 'threshold' json property</summary>
		private const string Property_Threshold = "threshold";

		//Fields
		//---------------------------------------------------------------------

		/// <summary>Identifier of the player</summary>
		private string playerId = null;

		/// <summary>Tokens earned by the player</summary>
		private int tokens = 0;

		/// <summary>Number of tokens the player requires to reach the next threshold</summary>
		private int tokensToThreshold = 0;

		/// <summary>Threshold token value for earning rewards</summary>
		private int threshold = 0;

		//Properties
		//---------------------------------------------------------------------

		/// <summary>Identifier of the player</summary>
		public string PlayerId
		{ get { return playerId; } }

		/// <summary>Tokens earned by the player</summary>
		/// <remarks>Token-Based games only</remarks>
		public int Tokens
		{ get { return tokens; } }

		/// <summary>Number of tokens the player requires to reach the next threshold</summary>
		/// <remarks>Token-Based games only</remarks>
		public int TokensToThreshold
		{ get { return tokensToThreshold; } }

		/// <summary>Threshold token value for earning rewards</summary>
		/// <remarks>Token-Based games only</remarks>
		public int Threshold
		{ get { return threshold; } }

		//Constructor
		//---------------------------------------------------------------------

		/// <summary>
		/// Creates a new <code>ConclifyApiResponsePlayerStandard</code> instance
		/// </summary>
		/// <param name="jsonResponse">Json response to retrieve fields from</param>
		/// <param name="gameType">Game-type to use to determine if optional properties exist or not</param>
		internal ConclifyApiResponsePlayerStandard(Variant jsonResponse, ConclifyApiGameTypes gameType)
			: base(jsonResponse, gameType)
		{
			//Retrieve Player Id
			playerId = jsonResponse[Property_PlayerId];

			//Check Game Type
			switch(gameType)
			{
				case ConclifyApiGameTypes.TokenBased:
				{
					try
					{
						//Retrieve Token-Based Only Fields
						tokens = jsonResponse[Property_Tokens];
						tokensToThreshold = jsonResponse[Property_TokensToThreshold];
						threshold = jsonResponse[Property_Threshold];
					}
					catch(Exception)
					{
						/* Don't Care */
					}
					break;
				}
			}
		}
	}
}
// ================================================================================================