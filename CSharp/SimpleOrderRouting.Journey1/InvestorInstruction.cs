// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InvestorInstruction.cs" company="">
//    Copyright 2014 The Lunch-Box mob: Ozgur DEVELIOGLU (@Zgurrr), Cyrille  DUPUYDAUBY 
//    (@Cyrdup), Tomasz JASKULA (@tjaskula), Thomas PIERRAIN (@tpierrain)
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//        http://www.apache.org/licenses/LICENSE-2.0
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------
namespace SimpleOrderRouting.Journey1
{
    using System;

    /// <summary>
    /// Trading instruction given to the SOR on the investor-side.
    /// </summary>
    public class InvestorInstruction
    {
        public InvestorInstruction(Way way, int quantity, decimal price)
        {
            this.Way = way;
            this.Quantity = quantity;
            this.Price = price;
        }

        /// <summary>
        /// Occurs when the <see cref="InvestorInstruction"/> is fully executed.
        /// </summary>
        public event EventHandler<OrderExecutedEventArgs> Executed;

        /// <summary>
        /// Gets the way to be used for the Instruction (Buy/Sell).
        /// </summary>
        /// <value>
        /// The way to be used for the Instruction (Buy/Sell).
        /// </value>
        public Way Way { get; private set; }

        /// <summary>
        /// Gets the quantity to be bought or sell.
        /// </summary>
        /// <value>
        /// The quantity to be bought or sell.
        /// </value>
        public int Quantity { get; private set; }

        public int ExecutedQuantity { get; private set; }

        /// <summary>
        /// Gets the price we are looking for the execution.
        /// </summary>
        /// <value>
        /// The price we are looking for the execution.
        /// </value>
        public decimal Price { get; private set; }

        /// <summary>
        /// Just a naive implementation to make the test pass. 
        /// Code smell here: with the Executed event raised from outside the InvestorInstruction.
        /// </summary>
        /// <param name="executedQuantity">The executed Quantity.</param>
        /// <param name="executedPrice">The executed Price.</param>
        internal virtual void NotifyOrderExecution(int executedQuantity, decimal executedPrice)
        {
            if (executedPrice == this.Price)
            {
                this.ExecutedQuantity += executedQuantity;

                // instruction fully executed, I notify
                if (this.Executed != null && this.ExecutedQuantity >= this.Quantity)
                {
                    this.Executed(this, new OrderExecutedEventArgs(this.Way, this.ExecutedQuantity, this.Price));
                }
            }
        }
    }
}