// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InvestorInstructionDtoCallBacks.cs" company="LunchBox corp">
//     Copyright 2014 The Lunch-Box mob: 
//           Ozgur DEVELIOGLU (@Zgurrr)
//           Cyrille  DUPUYDAUBY (@Cyrdup)
//           Tomasz JASKULA (@tjaskula)
//           Mendel MONTEIRO-BECKERMAN (@MendelMonteiro)
//           Thomas PIERRAIN (@tpierrain)
//     
//     Licensed under the Apache License, Version 2.0 (the "License");
//     you may not use this file except in compliance with the License.
//     You may obtain a copy of the License at
//         http://www.apache.org/licenses/LICENSE-2.0
//     Unless required by applicable law or agreed to in writing, software
//     distributed under the License is distributed on an "AS IS" BASIS,
//     WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//     See the License for the specific language governing permissions and
//     limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SimpleOrderRouting.Infra
{
    using System;

    using SimpleOrderRouting.Markets.Orders;

    public class InvestorInstructionDtoCallBacks
    {
        private readonly Action<InstructionExecutedEventArgs> instructionExecutedCallback;

        private readonly Action<InstructionFailedEventArgs> instructionFailedCallback;

        public InvestorInstructionDtoCallBacks(Action<InstructionExecutedEventArgs> instructionExecutedCallback, Action<InstructionFailedEventArgs> instructionFailedCallback)
        {
            this.instructionExecutedCallback = instructionExecutedCallback;
            this.instructionFailedCallback = instructionFailedCallback;
        }

        public void ExecutedCallback(OrderExecutedEventArgs args)
        {
            // Adapts to the inside (domain) model to the outside one (investor)
            var investorWay = (args.Way == Way.Buy) ? InvestorWay.Buy : InvestorWay.Sell;
            var instructionExecutedArgs = new InstructionExecutedEventArgs(investorWay, args.Price, args.Quantity);
            this.instructionExecutedCallback(instructionExecutedArgs);
        }

        public void FailedCallbacks(string reason)
        {
            // Adapts to the inside (domain) model to the outside one
            var instructionFailedArgs = new InstructionFailedEventArgs(reason);
            this.instructionFailedCallback(instructionFailedArgs);
        }
    }
}