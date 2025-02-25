﻿using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.UserService.Dtos;

public record UserCreateRequest(string UserName, string Email, string Password, string PhoneNumber, string Address, string City, string County);
