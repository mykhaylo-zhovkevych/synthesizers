using SynthesizerLibrary.Core.Audio.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SythesizerLibrary.Core.Audio
{
    public abstract class Node : IAudioNode
    {
        public IList<IChannel> Inputs { get; }
        public IList<IChannel> Outputs { get; }

        protected readonly IAudioProvider AudioProvider;
        private readonly Action _generate;
        protected Node(IAudioProvider provider, int numberOfInputs, int numberOfOutputs, Action? generate = null)
        {
            AudioProvider = provider;
            for(var i = 0; i < numberOfInputs; i++)
            {
                Inputs.Add(new InputChannel(this, i));
            }

            Outputs = new List<IChannel>();
            for (var i = 0; i < numberOfOutputs; i++)
            {
                Inputs.Add(new OutputChannel(this, i));
            }

            _generate = GenerateMix;
            if (generate != null)
            {
                _generate = generate;
            }
        }

        public void Disconnect(IAudioNode node, int output, int input)
        {
            throw new NotImplementedException();
        }

        public void Tick()
        {
            throw new NotImplementedException();
        }

        public List<IAudioNode> Traverse(IList<IAudioNode> nodes)
        {
            throw new NotImplementedException();
        }
    }

    public abstract void GenerateMix();
}
