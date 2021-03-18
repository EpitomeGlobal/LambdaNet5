using System;
using Amazon.Lambda.Core;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Sample
{
    /// <summary>
    ///     Lambda function entry.
    /// </summary>
    [PublicAPI]
    public class Function
    {
        /// <summary>
        ///     A simple function that takes a string and returns both the upper and lower case version of the string.
        /// </summary>
        /// <param name="context">Lambda context.</param>
        public void FunctionHandler(ILambdaContext context)
        {
            Program.SetUp();         
        }
    }
}
