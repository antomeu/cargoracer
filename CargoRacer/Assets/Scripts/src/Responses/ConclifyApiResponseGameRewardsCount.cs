using Conclify.Game;
using TinyJSON;

namespace Conclify.Responses
{
	public class ConclifyApiResponseGameRewardsCount : ConclifyApiResponse
	{
		//Fields
		//---------------------------------------------------------------------

		//Properties
		//---------------------------------------------------------------------

		//Constructor
		//---------------------------------------------------------------------

		/// <summary>
		/// Creates a new <code>ConclifyApiResponseGameRewardsCount</code> instance
		/// </summary>
		/// <param name="jsonResponse">Json response to retrieve fields from</param>
		/// <param name="gameType">Game-type to use to determine if optional properties exist or not</param>
		internal ConclifyApiResponseGameRewardsCount(Variant jsonResponse, ConclifyApiGameTypes gameType)
			: base(jsonResponse, gameType)
		{
			//TODO :: Game Reward List Response Properties
		}
	}
}
