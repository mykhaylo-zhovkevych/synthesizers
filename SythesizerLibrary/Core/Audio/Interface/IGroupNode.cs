using System.Collections;

namespace SynthesizerLibrary.Core.Audio.Interface
{

    public interface IGroupNode
    {
        IList<IAudioNode> InputPassthroughNodes { get; }
        IList<IAudioNode> OutputPassthroughNodes { get; }

    }

}

