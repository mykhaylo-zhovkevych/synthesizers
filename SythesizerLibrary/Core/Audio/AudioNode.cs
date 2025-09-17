using SynthesizerLibrary.Core.Audio.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SythesizerLibrary.Core.Audio
{
    public class AudioNode : IAudioNode
    {
        public IList<IChannel> Inputs { get; }
        public IList<IChannel> Outputs { get; }


        protected readonly IAudioProvider AudioProvider;

        public List<IAudioNode> InputPassThroughNodes { get; }
        public List<IAudioNode> OutputPassThroughNodes { get; set; }
        public bool IsAggregate { get; }

        protected bool IsVirtual { get; init; }




        protected AudioNode(IAudioProvider provider, int numberOfInputs, int numberOfOutputs, bool isAggregate = false)
        {
            AudioProvider = provider;

            for (var i = 0; i < numberOfInputs; i++)
            {
                Inputs.Add(new InputChannel(this, i));
            }

            Outputs = new List<IChannel>();
            for (var i = 0; i < numberOfOutputs; i++)
            {
                Inputs.Add(new OutputChannel(this, i));
            }
            InputPassThroughNodes = new List<IAudioNode>();
            OutputPassThroughNodes = new List<IAudioNode>();

            IsAggregate = isAggregate;

            if (!IsAggregate) return;

            // If we are an aggregate we need to create some virtual nodes 
            // for the pass through nodes
            for (var i = 0; i < numberOfInputs; i++)
            {
                InputPassThroughNodes.Add(new AudioNode(AudioProvider, 1, 1) { IsVirtual = true });
            }

            for (var i = 0; i < numberOfOutputs; i++)
            {
                OutputPassThroughNodes.Add(new AudioNode(AudioProvider, 1, 1) { IsVirtual = true });
            }

        }

        public void Disconnect(IAudioNode node, int outputIndex = 0, int inputIndex = 0)
        {

            if (IsAggregate)
            {
                OutputPassThroughNodes[outputIndex].Disconnect(node, 0, inputIndex);
                
            }
            else
            {
                var inputPin = node.IsAggregate ? node.InputPassThroughNodes[inputIndex].Inputs[0] : node.Inputs[inputIndex];
                var outputPin = Outputs[outputIndex];

                inputPin.Disconnect(outputPin);
                outputPin.Disconnect(inputPin);
            }

            AudioProvider.NeedTraverse = true;


        }
        public void Connect(IAudioNode node, int outputIndex = 0, int inputIndex = 0)
        {
            if (IsAggregate)
            {
                var psNode = OutputPassThroughNodes[outputIndex];
                psNode.Connect(node, 0, inputIndex);
            }
            else
            {
                var inputPin = node.IsAggregate ? node.InputPassThroughNodes[inputIndex].Inputs[0] : node.Inputs[inputIndex];
                var outputPin = Outputs[outputIndex];

                outputPin.Connect(inputPin);
                inputPin.Connect(outputPin);
            }

            AudioProvider.NeedTraverse = true;

        }

        public void Remove()
        {
            throw new NotImplementedException();
        }

        public void Tick()
        {
            MigrateInputSamples();
            MigrateOutputSamples();
        }

        public List<IAudioNode> Traverse(IList<IAudioNode> nodes)
        {
            throw new NotImplementedException();
        }
    
        protected virtual void GenerateMix()
        {

        }

    }
}