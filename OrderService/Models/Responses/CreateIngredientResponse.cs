﻿using Shared.Responses;

namespace OrderService.Models.Responses;

public class CreateIngredientResponse : BaseResponse
{
    public int Data { get; set; }
}