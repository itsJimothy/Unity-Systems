namespace Jimothy.Systems.Audio.Music.Ovani
{
    public interface IOvaniMusicController : IMusicController
    {
        int CurrentIntensity { get; }
        void SetIntensity(int intensity);
        void IncreaseIntensity();
        void DecreaseIntensity();
        void Play(int songIndex, int intensity);
    }
}