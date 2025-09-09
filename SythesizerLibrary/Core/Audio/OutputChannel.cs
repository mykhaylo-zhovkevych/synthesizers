using SynthesizerLibrary.Core.Audio.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SythesizerLibrary.Core.Audio
{
    public class OutputChannel : IChannel
    {

        private IChannel? _linkedInput;
        private int _numberChannels;

        public IAudioNode Node { get; }

        public int Index { get; }

        public List<IChannel> Connected { get; }

        public int Channels { get => GetNumberOfChannels(); set => _numberChannels = value; }
        public List<double> Samples { get; set; }


        public OutputChannel(IAudioNode node, int index)
        {
            Node = node;
            Index = index;
            Connected = new List<IChannel>();
            Samples = new List<double>();

            _linkedInput = null;
            _numberChannels = 1;
        }

        public void Connect(IChannel from)
        {
            Connected.Add(from);
        }

        public void Disconnect(IChannel from)
        {
            Connected.Remove(from);
        }

        public void LinkNumberOfChannels(IChannel input)
        {
            _linkedInput = input;
        }

        private int GetNumberOfChannels()
        {
            var sampleCount = _linkedInput?.Samples.Count ?? 0;
            return _linkedInput?.Connected.Count > 0 ? sampleCount : _numberChannels;
        }
    }
}
