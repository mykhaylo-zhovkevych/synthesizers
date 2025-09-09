using SynthesizerLibrary.Core.Audio.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SythesizerLibrary.Core.Audio;

namespace SythesizerLibrary.DSP
{
    public class UpMixer : Node
    {
        public UpMixer(IAudioProvider provider, int numberOfInputs, int numberOfOutputs, Action? generate = null) : base(provider, numberOfInputs, numberOfOutputs, generate)
        {
        }

        public override void GenerateMix()
        {
            throw new NotImplementedException();
        }
    }
}
