namespace SynthesizerLibrary.Core.Audio.Interface 
{
 
    public interface IAudioProvider
    {
        bool NeedTraverse { get; }
        int Channels { get; }
        int SampleRate { get; }

        int TotalWriteTime { get; }

    }
    
}