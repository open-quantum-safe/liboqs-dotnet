using System;

namespace OpenQuantumSafe
{
    /// <summary>
    /// Thrown when a requested mechanism is supported but not enabled by the OQS runtime.
    /// </summary>
    public class MechanismNotEnabledException: MechanismNotSupportedException
    {
        /// <summary>
        /// Constructs a new <see cref="MechanismNotEnabledException"/>.
        /// </summary>
        /// <param name="mechanism"></param>
        public MechanismNotEnabledException(string mechanism) : base(mechanism) { }
    }
}
