using Microsoft.Xna.Framework;
using Midori.Interfaces;
using System;

namespace Midori.GameObjects
{
	public abstract class AnimatedGameObject : GameObject, IAnimatable
	{
		private int currentFrame;
		private int basicAnimationFrameCount;
		private double timer;
		private int delay;
		private Rectangle sourceRect;

		protected AnimatedGameObject()
			: base()
		{
			this.Timer = 0.0;
			this.CurrentFrame = 0;
			this.SourceRect = new Rectangle();
		}

		#region Properties
		// IAnimatable
		public int CurrentFrame
		{
			get
			{
				return this.currentFrame;
			}
			protected set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("Current Frame should not be negative");
				}

				this.currentFrame = value;
			}
		}

		public int BasicAnimationFrameCount
		{
			get
			{
				return this.basicAnimationFrameCount;
			}
			protected set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("Basic animation frame count Frame should not be negative");
				}

				this.basicAnimationFrameCount = value;
			}
		}

		public double Timer
		{
			get
			{
				return this.timer;
			}
			protected set
			{
				if (value < 0.0)
				{
					throw new ArgumentOutOfRangeException("Timer should not be negative");
				}

				this.timer = value;
			}
		}

		public int Delay
		{
			get
			{
				return this.delay;
			}
			protected set
			{
				if (value < 0.0)
				{
					throw new ArgumentOutOfRangeException("Delay should not be negative");
				}

				this.delay = value;
			}
		}

		public Rectangle SourceRect
		{
			get { return this.sourceRect; }
			protected set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Source rectangle shouldn't be null");
				}

				this.sourceRect = value;
			}
		}

		#endregion

		#region Non-abstract methods

		protected void BasicAnimationLogic(GameTime gameTime, int delay, int basicAnimationFrameCount)
		{
			this.Timer += gameTime.ElapsedGameTime.TotalMilliseconds;

			if (this.Timer >= delay)
			{
				this.CurrentFrame++;

				if (this.CurrentFrame >= basicAnimationFrameCount)
				{
					this.CurrentFrame = 0;
				}

				this.Timer = 0.0;
			}
		}

		#endregion
	}
}
