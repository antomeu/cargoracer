using System;
using System.Collections.Generic;
using System.Text;

namespace Conclify.Responses
{
	public enum ConclifyApiResponseTypes
	{
		/// <summary>Identifies the basic api response</summary>
		Basic,
		/// <summary>Identifies the api error response</summary>
		Error,
		/// <summary>Identifies the player-standard response</summary>
		PlayerStandard,
		/// <summary>Identifies the player-extended response</summary>
		PlayerExtended,
		/// <summary>Identifies the player-reward response</summary>
		PlayerReward,
		/// <summary>Identifies the player-reward-list response</summary>
		PlayerRewardList,
		/// <summary>Identifies the game-reward-list response</summary>
		GameRewardList,
		/// <summary>Identifies the game-rewards-count response</summary>
		GameRewardsCount,
		/// <summary>Identifies the game-scores list response</summary>
		GameScoresList
	}
}
