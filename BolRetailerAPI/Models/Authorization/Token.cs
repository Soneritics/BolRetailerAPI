﻿using System;

namespace BolRetailerApi.Models.Authorization;

public class Token
{
    private int _expiresIn;
    public string access_token { get; set; }
    public string token_type { get; set; }
    public string scope { get; set; }

    public int expires_in
    {
        get => _expiresIn;
        set
        {
            _expiresIn = value;
            ExpiresAt = Created.AddSeconds(_expiresIn);
        }
    }

    public DateTime Created { get; } = DateTime.Now;
    public DateTime ExpiresAt { get; private set; } = DateTime.Now;
}