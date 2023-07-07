namespace PetShop.Models
{
    public class ErrorModel
    {
        public string? ErrorMsg { get; set; } = "";

        public DateTime? ErrorDate { get; set; }

        public ErrorModel(string? errorMsg)
        {
            ErrorMsg = errorMsg;

            ErrorDate = DateTime.Now;
        }
    }
}
