namespace QuickEngine.Unity
{
    using UnityEngine;

    public class WaitForAnimation : CustomYieldInstruction
    {
        private Animation animation;

        public WaitForAnimation(Animation animation)
        {
            this.animation = animation;
        }

        public override bool keepWaiting
        {
            get
            {
                if (animation == null)
                    return false;

                return animation.isPlaying;
            }
        }
    }
}
