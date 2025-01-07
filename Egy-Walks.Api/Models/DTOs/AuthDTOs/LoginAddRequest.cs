using System.ComponentModel.DataAnnotations;

namespace Egy_Walks.Api.Models.DTOs.AuthDTOs;

public class LoginAddRequest
{
    public string UserName { get; set; }
    [DataType(DataType.Password)]
    public string Password { get; set; }
}