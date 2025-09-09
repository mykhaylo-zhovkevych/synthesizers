using System;

namespace SynthesizerLibrary.Core.Audio.Interface
{
    public interface IAudioNode
	{
		IList<IChannel> Inputs { get; }
		IList<IChannel> Outputs { get; }
		void Disconnect(IAudioNode node, int output, int input);
		void Tick();
		List<IAudioNode> Traverse(IList<IAudioNode> nodes);
	
	}
}
