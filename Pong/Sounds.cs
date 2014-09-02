using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pong
{
    public enum Sound
    {
        Hit,
        Score,
        GameOver,
    }

    public class Sounds
    {
        private SoundObjects soundObjects;

        public Sounds(SoundObjects soundObjects)
        {
            this.soundObjects = soundObjects;
        }

        public void Play(Sound sound)
        {
            switch (sound)
            {
                case (Sound.Hit):
                    soundObjects.Hit.Play();
                        break;

                case (Sound.Score):
                        soundObjects.Score.Play();
                        break;
                case (Sound.GameOver):
                    soundObjects.GameOver.Play();
                    break;


            }
        }

    }
}
