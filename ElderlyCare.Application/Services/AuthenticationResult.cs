﻿using ElderlyCare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElderlyCare.Application.Services;

public record AuthenticationResult(
    User User,
    string Token
    );
