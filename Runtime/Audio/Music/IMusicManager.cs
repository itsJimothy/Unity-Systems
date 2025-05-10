namespace Jimothy.Systems.Audio.Music
{
    public interface IMusicManager
    {
        int CurrentSongIndex { get; }
        int SongCount { get; }
        void Play(int songIndex);
        void Stop();
    }
}