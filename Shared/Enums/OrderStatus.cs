﻿namespace Shared.Enums;

public enum OrderStatus
{
    Init,
    CheckedOut,
    Preparing,
    ReadyToPickup,
    Delivering,
    Arrived,
    Success,
    Failed,
    Canceled,
    Rejected,
}