namespace RFIDClient.Desktop.Core
{
    public interface IPayment
    {
        /// <summary>
        /// ID of the payment
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// Internal code of the payment
        /// </summary>
        string Code { get; set; }

        /// <summary>
        /// Friendly name of the payment
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Amount of the payment
        /// </summary>
        decimal Amount { get; set; }
    }
}
