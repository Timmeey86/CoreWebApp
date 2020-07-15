using System;

namespace LogicLayer.Dtos
{
    /// <summary>
    /// This class defines data presented to the API user for errors.
    /// </summary>
    public class ErrorDto
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
