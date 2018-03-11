using Microsoft.Xna.Framework;
using Midori.Core;
using Midori.GameObjects.Units;
using Midori.Interfaces;

namespace Midori.GameObjects.Projectiles
{
    public abstract class Projectile : AnimatedGameObject, IProjectile
    {
        protected Projectile()
            : base()
        {
            this.Timer = 0.0;
            this.CurrentFrame = 0;
            this.SourceRect = new Rectangle();

			this.AbleToDoDamage = true;
        }

        #region Properties

        // IMovable
        public float MovementSpeed { get; protected set; }

        public float DefaultMovementSpeed { get; protected set; }

        // INeedToKnowEhereImFacing
        public bool IsFacingLeft { get; protected set; }

        // IOwned
        public Unit Owner { get; protected set; }

		public bool AbleToDoDamage { get; protected set; }

		#endregion

		#region Methods

		// Non-abstract Methods
		protected void ManageMovement(GameTime gameTime)
        {
            // Left & Right Movement
            if (this.IsFacingLeft)
            {
                this.X -= this.MovementSpeed;
            }
            else
            {
                this.X += this.MovementSpeed;
            }
        }

        public virtual void Update(GameTime gameTime)
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
                this.ManageMovement(gameTime);

                this.BoundingBoxX = (int)this.X;
                this.BoundingBoxY = (int)this.Y;
            }            
        }

        #endregion
        
    }
}
