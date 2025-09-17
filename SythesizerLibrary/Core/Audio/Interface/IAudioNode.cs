using System;

namespace SynthesizerLibrary.Core.Audio.Interface
{
    public interface IAudioNode
	{
		IList<IChannel> Inputs { get; }
		IList<IChannel> Outputs { get; }

		void Connect(IAudioNode node, int outputIndex, int inputIndex);
		void Disconnect(IAudioNode node, int output, int input);
		void Tick();
		List<IAudioNode> Traverse(IList<IAudioNode> nodes);
	
		List<IAudioNode> InputPassThroughNodes { get; } 
		List<IAudioNode> OutputPassThroughNodes { get; set; }

		void Remove();
		bool IsAggregate { get; }
	}
}
