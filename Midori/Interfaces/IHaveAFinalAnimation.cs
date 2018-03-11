using Microsoft.Xna.Framework;

namespace Midori.Interfaces
{
	public interface IHaveAFinalAnimation
	{
		bool ShouldStartFinalAnimation { get; }

		int FinalAnimationCurrentFrame { get; }

		int FinalAnimationFrameCount { get; }

		double FinalAnimationTimer { get; }

		int FinalAnimationDelay { get; }

		Rectangle FinalAnimationSourceRect { get; }

		void StartFinalAnimation();
	}
}
