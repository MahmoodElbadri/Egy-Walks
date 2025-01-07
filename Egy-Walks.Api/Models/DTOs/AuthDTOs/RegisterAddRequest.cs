using System.ComponentModel.DataAnnotations;

namespace Egy_Walks.Api.Models.DTOs.AuthDTOs;

public class RegisterAddRequest
{
    public string Username { get; set; }
    [DataType(DataType.Password)]
    public string Password { get; set; }
    public string[] Roles { get; set; }
}