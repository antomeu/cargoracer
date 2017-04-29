using Conclify.Game;
using TinyJSON;

namespace Conclify.Responses
{
	public class ConclifyApiResponsePlayerRewardList : ConclifyApiResponse
	{
		//Fields
		//---------------------------------------------------------------------

		//Properties
		//---------------------------------------------------------------------

		//Constructor
		//---------------------------------------------------------------------

		/// <summary>
		/// Creates a new <code>ConclifyApiResponsePlayerRewardList</code> instance
		/// </summary>
		/// <param name="jsonResponse">Json response to retrieve fields from</param>
		/// <param name="gameType">Game-type to use to determine if optional properties exist or not</param>
		internal ConclifyApiResponsePlayerRewardList(Variant jsonResponse, ConclifyApiGameTypes gameType)
			: base(jsonResponse, gameType)
		{
			//TODO :: Player Reward List Response Properties
		}
	}
}
