using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Midori.TextureLoading;
using Midori.Interfaces;
using Midori.GameObjects.Units;
using Midori.Core;

namespace Midori.GameObjects.Projectiles
{
    public class MidoriSmallProjectile : Projectile, IHaveAFinalAnimation
    {
        private const int MidoriSmallProjectileTextureWidth = 100;
        private const int MidoriSmallProjectileTextureHeight = 36;
		private const float MidoriSmallProjectileDefaultMovementSpeed = 22;
		// Basic Animation
		private const int MidoriSmallProjectileDelay = 100;
        private const int MidoriSmallProjectileBasicAnimationFrameCount = 1;
		// Final Animation
		private const int MidoriSmallProjectileFinalAnimationDelay = 100;
		private const int MidoriSmallProjectileFinalAnimationFrameCount = 3;

		public MidoriSmallProjectile(Vector2 position, bool moveLeft, Unit owner)
            : base()
        {
            this.Position = position;

            this.SpriteSheet = TextureLoader.MidoriSmallProjectileSheet;
            this.TextureWidth = MidoriSmallProjectileTextureWidth;
            this.TextureHeight = MidoriSmallProjectileTextureHeight;

            this.IsFacingLeft = moveLeft;
            if (this.IsFacingLeft)
            { 
                this.SourceRect = new Rectangle(MidoriSmallProjectileTextureWidth * 0, MidoriSmallProjectileTextureHeight * 1, MidoriSmallProjectileTextureWidth, MidoriSmallProjectileTextureHeight);
				this.FinalAnimationSourceRect = new Rectangle(MidoriSmallProjectileTextureWidth * 1, MidoriSmallProjectileTextureHeight * 1, MidoriSmallProjectileTextureWidth, MidoriSmallProjectileTextureHeight);
            }
            else
            {
                this.SourceRect = new Rectangle(MidoriSmallProjectileTextureWidth * 0, MidoriSmallProjectileTextureHeight * 0, MidoriSmallProjectileTextureWidth, MidoriSmallProjectileTextureHeight);
				this.FinalAnimationSourceRect = new Rectangle(MidoriSmallProjectileTextureWidth * 1, MidoriSmallProjectileTextureHeight * 0, MidoriSmallProjectileTextureWidth, MidoriSmallProjectileTextureHeight);
			}

            this.DefaultMovementSpeed = MidoriSmallProjectileDefaultMovementSpeed;
            this.MovementSpeed = this.DefaultMovementSpeed;

            this.BoundingBox = new Rectangle(
                (int)this.X,
                (int)this.Y,
                this.TextureWidth,
                this.TextureHeight);

            this.Owner = owner;

			// Final Animation
			this.ShouldStartFinalAnimation = false;
			this.FinalAnimationDelay = MidoriSmallProjectileFinalAnimationDelay;
			this.FinalAnimationFrameCount = MidoriSmallProjectileFinalAnimationFrameCount;
        }

		public bool ShouldStartFinalAnimation { get; protected set; }

		public int FinalAnimationCurrentFrame { get; protected set; }

		public int FinalAnimationFrameCount { get; protected set; }

		public double FinalAnimationTimer { get; protected set; }

		public int FinalAnimationDelay { get; protected set; }

		public Rectangle FinalAnimationSourceRect { get; protected set; }

		public void StartFinalAnimation()
		{
			this.ShouldStartFinalAnimation = true;
			this.AbleToDoDamage = false;
		}

		public override void Update(GameTime gameTime)
		{
			if (Collision.CheckForCollisionWithWorldBounds(this) || Collision.CheckForCollisionWithAnyTiles(this.BoundingBox))
			{
				// If projectile has a final animation => play it and then nullify
				if (this is IHaveAFinalAnimation)
				{
					IHaveAFinalAnimation projectileWithFinalAnimation = this as IHaveAFinalAnimation;
					// If final animation was already started
					if (projectileWithFinalAnimation.FinalAnimationCurrentFrame == projectileWithFinalAnimation.FinalAnimationFrameCount)
					{
						// Nullify
						this.Nullify();
					}
					else
					{
						// If not => start the final animation
						(this as IHaveAFinalAnimation).StartFinalAnimation();
					}
				}
				else
				{
					// If it does not have a final animation => nullify
					this.Nullify();
				}
			}
			else
			{
				// Stop moving while playing the final animation
				if (!this.ShouldStartFinalAnimation)
				{
					this.ManageMovement(gameTime);
				}

				this.BoundingBoxX = (int)this.X;
				this.BoundingBoxY = (int)this.Y;
			}
		}

		public override void Draw(SpriteBatch spriteBatch)
        {
			if (this.ShouldStartFinalAnimation)
			{
				spriteBatch.Draw(this.SpriteSheet, this.Position, this.FinalAnimationSourceRect, Color.White);
				this.FinalAnimationCurrentFrame++;

				// Nullify when the last frame of the final animation has been drawn
				if (this.FinalAnimationCurrentFrame > this.FinalAnimationFrameCount)
				{
					this.Nullify();
				}

			}
			else {
				spriteBatch.Draw(this.SpriteSheet, this.Position, this.SourceRect, Color.White);
			}
        }

        
    }
}
