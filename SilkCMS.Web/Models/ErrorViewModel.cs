namespace SilkCMS.Web.Models;
#pragma warning disable CS8632 


public class ErrorViewModel
{

    public string? RequestId { get; set; }


    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
#pragma warning restore CS8632 