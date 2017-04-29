using Conclify.Game;
using TinyJSON;
using Conclify.Responses.Objects;

namespace Conclify.Responses
{
	public class ConclifyApiResponsePlayerReward : ConclifyApiResponsePlayerStandard
	{
		//Fields
		//---------------------------------------------------------------------

		/// <summary>Reward that the player received</summary>
		private ConclifyApiResponseObjectReward reward = null;

		//Properties
		//---------------------------------------------------------------------

		/// <summary>Reward that the player received</summary>
		public ConclifyApiResponseObjectReward Reward
		{ get { return reward; } }

		//Constructor
		//---------------------------------------------------------------------

		/// <summary>
		/// Creates a new <code>ConclifyApiResponsePlayerReward</code> instance
		/// </summary>
		/// <param name="jsonResponse">Json response to retrieve fields from</param>
		/// <param name="gameType">Game-type to use to determine if optional properties exist or not</param>
		internal ConclifyApiResponsePlayerReward(Variant jsonResponse, ConclifyApiGameTypes gameType)
			: base(jsonResponse, gameType)
		{
			//TODO :: Player Reward List Response Properties
		}
	}
}
